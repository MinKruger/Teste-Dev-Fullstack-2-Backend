using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure services for Infra and Application layers
builder.Services.AddInfra(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
builder.Services.AddApplication(); // Adiciona Application

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Application.Mappings.ClienteMappingProfile));

// Configure FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Application.Validators.CreateClienteDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
