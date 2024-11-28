using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> ObterTodosAsync();
        Task<PedidoDto?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(CreatePedidoDto pedidoDto);
        Task AtualizarAsync(Guid id, UpdatePedidoDto pedidoDto);
        Task RemoverAsync(Guid id);
    }
}
