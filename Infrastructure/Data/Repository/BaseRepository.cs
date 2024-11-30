using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T?> ObterPorIdAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error fetching entity by ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error fetching all entities: {ex.Message}", ex);
            }
        }

        public async Task AdicionarAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error adding entity: {ex.Message}", ex);
            }
        }

        public async Task AdicionarEmLoteAsync(IEnumerable<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error adding entities in bulk: {ex.Message}", ex);
            }
        }

        public async Task AtualizarAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error updating entity: {ex.Message}", ex);
            }
        }

        public async Task RemoverAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception($"Error removing entity: {ex.Message}", ex);
            }
        }
    }
}
