using Infrastructure.Data.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository;

namespace Infrastructure.DependencyInjection
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, string connectionString)
        {
            // Configura o DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registra o repositório com o decorator de cache
            services.AddMemoryCache();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.Decorate<IClienteRepository, CachedClienteRepository>();

            return services;
        }
    }
}
