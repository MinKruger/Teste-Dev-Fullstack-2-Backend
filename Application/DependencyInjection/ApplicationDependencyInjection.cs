using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVendedorService, VendedorService>();
            services.AddScoped<IPedidoService, PedidoService>();

            return services;
        }
    }
}
