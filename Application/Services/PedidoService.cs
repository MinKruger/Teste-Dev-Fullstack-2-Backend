using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IVendedorRepository vendedorRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDto>> ObterTodosAsync()
        {
            try
            {
                var pedidos = await _pedidoRepository.ObterTodosAsync();
                return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PedidoDto?> ObterPorIdAsync(int id)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorIdAsync(id);
                return _mapper.Map<PedidoDto?>(pedido);
            }
            catch
            {
                throw;
            }
        }

        public async Task AdicionarAsync(CreatePedidoDto pedidoDto)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(pedidoDto.ClienteId);
                var vendedor = await _vendedorRepository.ObterPorIdAsync(pedidoDto.VendedorId);

                if (cliente == null || !cliente.Ativo)
                    throw new Exception("Cliente inativo ou não encontrado.");

                if (vendedor == null || !vendedor.Ativo)
                    throw new Exception("Vendedor inativo ou não encontrado.");

                var pedido = _mapper.Map<Pedido>(pedidoDto);
                await _pedidoRepository.AdicionarAsync(pedido);
            }
            catch
            {
                throw;
            }
        }

        public async Task AtualizarAsync(int id, UpdatePedidoDto pedidoDto)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorIdAsync(id);
                if (pedido == null)
                {
                    throw new Exception("Pedido não encontrado.");
                }

                _mapper.Map(pedidoDto, pedido);
                await _pedidoRepository.AtualizarAsync(pedido);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PedidoDetalhadoDto>> ObterPedidosDetalhados()
        {
            try
            {
                var pedidos = await _pedidoRepository.ObterPedidosDetalhados();
                return _mapper.Map<IEnumerable<PedidoDetalhadoDto>>(pedidos);
            }
            catch
            {
                throw;
            }
        }

        public async Task ExcluirAsync(int id)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorIdAsync(id);
                if (pedido == null)
                    throw new Exception("Pedido não encontrado.");

                if (pedido.Autorizado)
                    throw new Exception("Não é possível excluir um pedido autorizado.");

                await _pedidoRepository.RemoverAsync(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
