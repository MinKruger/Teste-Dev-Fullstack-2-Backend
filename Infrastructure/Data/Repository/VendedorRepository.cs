using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly ApplicationDbContext _context;

        public VendedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vendedor?> ObterPorIdAsync(Guid id)
        {
            return await _context.Vendedor.FindAsync(id);
        }

        public async Task<IEnumerable<Vendedor>> ObterTodosAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task AdicionarAsync(Vendedor vendedor)
        {
            await _context.Vendedor.AddAsync(vendedor);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Vendedor vendedor)
        {
            _context.Vendedor.Update(vendedor);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var vendedor = await ObterPorIdAsync(id);
            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado.");

            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();
        }
    }
}
