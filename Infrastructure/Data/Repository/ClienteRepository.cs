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
            return await _dbContext.Cliente.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _dbContext.Cliente.ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _dbContext.Cliente.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _dbContext.Cliente.Update(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente != null)
            {
                cliente.Desativar(); // Desativa o cliente
                _dbContext.Cliente.Update(cliente); // Atualiza o cliente no banco
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}