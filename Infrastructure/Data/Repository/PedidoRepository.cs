using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context) { }

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

        public async Task<List<PedidoDetalhado>> ObterPedidosDetalhados()
        {
            return await _context.ViewPedidosDetalhados.ToListAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var pedido = await ObterPorIdAsync(id);
            if (pedido != null && pedido.Autorizado == false)
            {
                await RemoverAsync(pedido);
            }
        }
    }
}
