using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IVendedorRepository : IBaseRepository<Vendedor>
    {
        Task<List<PedidoPorVendedor>> ObterTotalVendasPorCodigoVendedorAsync(string codigoVendedor);
        Task DesativarAsync(int id);
    }
}
