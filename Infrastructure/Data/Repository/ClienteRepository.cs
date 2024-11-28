using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            return await _context.Cliente.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Cliente.ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DesativarAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente != null)
            {
                cliente.Desativar();
                await AtualizarAsync(cliente);
            }
        }

        public async Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Pedido
                .Where(p => p.ClienteId != null && p.DataCriacao >= inicio && p.DataCriacao <= fim)
                .SumAsync(p => p.ValorTotal);
        }
    }
}
