using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IVendedorRepository
    {
        Task<Vendedor?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Vendedor>> ObterTodosAsync();
        Task AdicionarAsync(Vendedor vendedor);
        Task AtualizarAsync(Vendedor vendedor);
        Task DesativarAsync(Guid id);
    }
}
