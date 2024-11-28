using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task DesativarAsync(int id);
        Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim);
    }
}
