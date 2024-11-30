using Dapper;
using Domain.Entities;
using Domain.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Data.Repositories
{
    public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<PedidoPorVendedor>> ObterTotalVendasPorCodigoVendedorAsync(string codigoVendedor)
        {
            try
            {
                // Obtenha a string de conexão do contexto
                var connectionString = _context.Database.GetConnectionString();

                // Crie uma nova conexão usando a string de conexão
                using (var connection = new SqlConnection(connectionString))
                {
                    // Abra a conexão
                    await connection.OpenAsync();

                    // Defina os parâmetros para o procedimento armazenado
                    var parameters = new DynamicParameters();
                    parameters.Add("@CodigoVendedor", codigoVendedor);

                    // Execute o procedimento armazenado usando Dapper
                    var result = await connection.QueryAsync<PedidoPorVendedor>(
                        "BuscarVendasPorVendedor",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    // Retorne os resultados como uma lista
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento adicional
                throw new Exception("Erro ao obter total de vendas por código de vendedor.", ex);
            }
        }

        public async Task DesativarAsync(int id)
        {
            var vendedor = await ObterPorIdAsync(id);
            if (vendedor != null)
            {
                vendedor.Desativar();
                await AtualizarAsync(vendedor);
            }
        }
    }
}
