using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description = "API para gerenciamento de clientes, vendedores e pedidos."
                });

                // Define as tags para categorizar os endpoints
                c.TagActionsBy(apiDesc =>
                {
                    var actionRoute = apiDesc.RelativePath?.ToLowerInvariant();

                    if (actionRoute != null)
                    {
                        // Categoriza a rota de Seeder com a tag "Seeder"
                        if (actionRoute.Contains("seeder"))
                            return new[] { "Seeder" };

                        // Categoriza por método HTTP
                        if (apiDesc.HttpMethod?.Equals("GET", StringComparison.OrdinalIgnoreCase) == true)
                            return new[] { "Get Endpoints" };

                        if (apiDesc.HttpMethod?.Equals("POST", StringComparison.OrdinalIgnoreCase) == true)
                            return new[] { "Set Endpoints" };

                        if (apiDesc.HttpMethod?.Equals("PUT", StringComparison.OrdinalIgnoreCase) == true)
                            return new[] { "Put Endpoints" };

                        if (apiDesc.HttpMethod?.Equals("DELETE", StringComparison.OrdinalIgnoreCase) == true)
                            return new[] { "Delete Endpoints" };
                    }

                    return new[] { "Others" }; // Default tag
                });

                // Ordena as ações pelo nome das tags, garantindo que Seeder fique no final
                c.OrderActionsBy(apiDesc =>
                {
                    var tags = apiDesc.ActionDescriptor.EndpointMetadata
                        .OfType<Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription>()
                        .SelectMany(d => d.ParameterDescriptions.Select(p => p.Name));

                    if (tags.Contains("Seeder")) return "z-seeder";
                    return apiDesc.RelativePath; // Ordem padrão
                });
            });

            return services;
        }
    }
}
