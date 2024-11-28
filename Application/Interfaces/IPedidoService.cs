using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> ObterTodosAsync();
        Task<PedidoDto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(CreatePedidoDto pedidoDto);
        Task AtualizarAsync(int id, UpdatePedidoDto pedidoDto);
        Task ExcluirAsync(int id);
    }
}
