using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClienteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _dbContext.Clientes.Update(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente != null)
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}