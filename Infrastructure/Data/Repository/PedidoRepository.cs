using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Vendedor)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedido.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedido.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var pedido = await ObterPorIdAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> ObterTotalVendasPorVendedoresNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Pedido
                .Where(p => p.DataCriacao >= inicio && p.DataCriacao <= fim)
                .SumAsync(p => p.ValorTotal);
        }

        public async Task<Cliente?> ObterMelhorClienteAsync()
        {
            var clienteId = await _context.Pedido
                .GroupBy(p => p.ClienteId)
                .OrderByDescending(g => g.Sum(p => p.ValorTotal))
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            if (clienteId == null)
                return null;

            return await _context.Cliente.FindAsync(clienteId);
        }
    }
}
