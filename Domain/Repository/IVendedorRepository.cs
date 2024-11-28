using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IVendedorRepository : IBaseRepository<Vendedor>
    {
        Task DesativarAsync(int id);
    }
}
