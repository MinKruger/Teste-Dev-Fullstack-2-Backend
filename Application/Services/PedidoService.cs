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
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDto>> ObterTodosAsync()
        {
            var pedidos = await _pedidoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }

        public async Task<PedidoDto?> ObterPorIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(id);
            return _mapper.Map<PedidoDto?>(pedido);
        }

        public async Task AdicionarAsync(CreatePedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _pedidoRepository.AdicionarAsync(pedido);
        }

        public async Task AtualizarAsync(Guid id, UpdatePedidoDto pedidoDto)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(id);
            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado.");
            }

            _mapper.Map(pedidoDto, pedido);
            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _pedidoRepository.RemoverAsync(id);
        }
    }
}
