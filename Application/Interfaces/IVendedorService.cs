using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDto>> ObterTodosAsync();
        Task<VendedorDto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(CreateVendedorDto vendedorDto);
        Task AtualizarAsync(int id, UpdateVendedorDto vendedorDto);
        Task DesativarAsync(int id);
        Task<decimal> ObterTotalVendasNoPeriodoAsync(DateTime inicio, DateTime fim);
        Task<ClienteDto?> ObterMelhorClienteAsync();
        Task<List<PedidoPorVendedorDto>> ObterTotalVendasPorCodigoVendedorAsync(string codigoVendedor);
    }
}
