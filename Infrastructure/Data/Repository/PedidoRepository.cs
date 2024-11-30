using Domain.Entities;
using Domain.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<decimal> ObterTotalVendasPorVendedoresNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                return await _context.Pedido
                    .Where(p => p.DataCriacao >= inicio && p.DataCriacao <= fim)
                    .SumAsync(p => p.ValorTotal);
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception("Erro ao obter total de vendas por vendedores no período.", ex);
            }
        }

        public async Task<Cliente?> ObterMelhorClienteAsync()
        {
            try
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
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception("Erro ao obter o melhor cliente.", ex);
            }
        }

        public async Task<List<PedidoDetalhado>> ObterPedidosDetalhados()
        {
            try
            {
                return await _context.ViewPedidosDetalhados.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception("Erro ao obter pedidos detalhados.", ex);
            }
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var pedido = await ObterPorIdAsync(id);
                if (pedido != null && pedido.Autorizado == false)
                {
                    await RemoverAsync(pedido);
                }
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception($"Erro ao remover o pedido com ID {id}.", ex);
            }
        }
    }
}
