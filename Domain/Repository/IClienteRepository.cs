using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task DesativarAsync(Guid id);
        Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim);
    }
}
