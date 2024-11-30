using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;

namespace Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public VendedorService(IVendedorRepository vendedorRepository, IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _vendedorRepository = vendedorRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendedorDto>> ObterTodosAsync()
        {
            var vendedores = await _vendedorRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<VendedorDto>>(vendedores);
        }

        public async Task<VendedorDto?> ObterPorIdAsync(int id)
        {
            var vendedor = await _vendedorRepository.ObterPorIdAsync(id);
            return _mapper.Map<VendedorDto?>(vendedor);
        }

        public async Task AdicionarAsync(CreateVendedorDto vendedorDto)
        {
            var vendedor = _mapper.Map<Vendedor>(vendedorDto);
            await _vendedorRepository.AdicionarAsync(vendedor);
        }

        public async Task AtualizarAsync(int id, UpdateVendedorDto vendedorDto)
        {
            var vendedor = await _vendedorRepository.ObterPorIdAsync(id);
            if (vendedor == null)
            {
                throw new Exception("Vendedor não encontrado.");
            }

            _mapper.Map(vendedorDto, vendedor);
            await _vendedorRepository.AtualizarAsync(vendedor);
        }

        public async Task DesativarAsync(int id)
        {
            var vendedor = await _vendedorRepository.ObterPorIdAsync(id);
            if (vendedor == null)
            {
                throw new Exception("Vendedor não encontrado.");
            }

            vendedor.Desativar();
            await _vendedorRepository.AtualizarAsync(vendedor);
        }

        public async Task<List<PedidoPorVendedorDto>> ObterTotalVendasPorCodigoVendedorAsync(string codigoVendedor)
        {
            var vendas = await _vendedorRepository.ObterTotalVendasPorCodigoVendedorAsync(codigoVendedor);
            return _mapper.Map<List<PedidoPorVendedorDto>>(vendas);
        }

        public async Task<decimal> ObterTotalVendasNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _pedidoRepository.ObterTotalVendasPorVendedoresNoPeriodoAsync(inicio, fim);
        }

        public async Task<ClienteDto?> ObterMelhorClienteAsync()
        {
            var melhorCliente = await _pedidoRepository.ObterMelhorClienteAsync();
            return _mapper.Map<ClienteDto?>(melhorCliente);
        }
    }
}
