using Domain.Entities;
using Domain.Repository;

namespace Infrastructure.Data.Repositories
{
    public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(ApplicationDbContext context) : base(context) { }

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
