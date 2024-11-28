using Application.DependencyInjection;
using Application.Validators;
using Infrastructure.DependencyInjection;
using FluentValidation;
using Application.Mappings;
using Application.Services;
using API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure services for Infra and Application layers
builder.Services.AddInfra(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
builder.Services.AddApplication(); // Adiciona Application
builder.Services.AddHttpClient<ExternalApiService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(ClienteMappingProfile), typeof(VendedorMappingProfile), typeof(PedidoMappingProfile));

// Configure FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ClienteDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PedidoDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VendedorDtoValidator>();

builder.Services.AddCustomSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
