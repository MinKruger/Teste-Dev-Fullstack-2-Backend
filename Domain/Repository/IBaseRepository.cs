using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IBaseRepository<T>
    {
        Task<T?> ObterPorIdAsync(int id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);
        Task AtualizarAsync(T entity);
        Task RemoverAsync(T entity);
        Task AdicionarEmLoteAsync(IEnumerable<T> entities);
    }
}
