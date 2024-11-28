using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<decimal> ObterTotalVendasPorVendedoresNoPeriodoAsync(DateTime inicio, DateTime fim);
        Task<Cliente?> ObterMelhorClienteAsync();
        Task RemoverAsync(int id);
    }
}
