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
            try
            {
                var cliente = await ObterPorIdAsync(id);
                if (cliente != null)
                {
                    cliente.Desativar();
                    await AtualizarAsync(cliente);
                }
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception($"Erro ao desativar o cliente com ID {id}.", ex);
            }
        }

        public async Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                return await _context.Pedido
                    .Where(p => p.ClienteId != null && p.DataCriacao >= inicio && p.DataCriacao <= fim)
                    .SumAsync(p => p.ValorTotal);
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception("Erro ao obter o total de compras no período especificado.", ex);
            }
        }
    }
}
