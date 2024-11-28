using Application.DTOs;
using Domain.Entities;
using Domain.Repository;
using Bogus;
using Application.Interfaces;

namespace Application.Services
{
    public class DatabaseSeederService : IDatabaseSeederService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public DatabaseSeederService(
            IVendedorRepository vendedorRepository,
            IClienteRepository clienteRepository,
            IPedidoRepository pedidoRepository)
        {
            _vendedorRepository = vendedorRepository;
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
        }

        private async Task GerarClientesAsync()
        {
            var faker = new Faker<Cliente>()
                .RuleFor(c => c.RazaoSocial, f => f.Company.CompanyName())
                .RuleFor(c => c.NomeFantasia, f => f.Company.CompanySuffix())
                .RuleFor(c => c.CNPJ, f => f.Random.ReplaceNumbers("########0001##"))
                .RuleFor(c => c.Logradouro, f => f.Address.StreetAddress())
                .RuleFor(c => c.Bairro, f => f.Address.County())
                .RuleFor(c => c.Cidade, f => f.Address.City())
                .RuleFor(c => c.Estado, f => f.Address.StateAbbr())
                .RuleFor(c => c.Ativo, f => true);

            var clientes = faker.Generate(10);

            await _clienteRepository.AdicionarEmLoteAsync(clientes);
        }

        private async Task GerarVendedoresAsync()
        {
            var faker = new Faker<Vendedor>()
                .RuleFor(v => v.Nome, f => f.Name.FullName())
                .RuleFor(v => v.CodigoVendedor, f => f.Random.Replace("V####"))
                .RuleFor(v => v.Apelido, f => f.Internet.UserName())
                .RuleFor(v => v.Ativo, f => true);

            var vendedores = faker.Generate(5);

            await _vendedorRepository.AdicionarEmLoteAsync(vendedores);
        }

        private async Task GerarPedidosAsync()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            var vendedores = await _vendedorRepository.ObterTodosAsync();

            var faker = new Faker<Pedido>()
                .RuleFor(p => p.DescricaoPedido, f => f.Commerce.ProductName())
                .RuleFor(p => p.ValorTotal, f => f.Finance.Amount(100, 1000))
                .RuleFor(p => p.DataCriacao, f => f.Date.Past(1))
                .RuleFor(p => p.Observacao, f => f.Lorem.Sentence())
                .RuleFor(p => p.Autorizado, f => f.Random.Bool())
                .RuleFor(p => p.ClienteId, f => f.PickRandom(clientes).Id)
                .RuleFor(p => p.VendedorId, f => f.PickRandom(vendedores).Id);

            var pedidos = faker.Generate(30);

            await _pedidoRepository.AdicionarEmLoteAsync(pedidos);
        }

        public async Task PopularBancoDeDadosAsync()
        {
            await GerarClientesAsync();
            await GerarVendedoresAsync();
            await GerarPedidosAsync();
        }
    }
}
