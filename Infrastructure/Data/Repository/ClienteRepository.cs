using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context) { }

        public async Task DesativarAsync(int id)
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
