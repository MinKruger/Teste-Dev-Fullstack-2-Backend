using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(Guid id);
        Task<decimal> ObterTotalVendasPorVendedoresNoPeriodoAsync(DateTime inicio, DateTime fim);
        Task<Cliente?> ObterMelhorClienteAsync();
    }
}
