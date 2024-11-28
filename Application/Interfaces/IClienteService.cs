using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto?> ObterPorIdAsync(int id);
        Task AdicionarPorCnpjAsync(string cnpj);
        Task AtualizarAsync(int id, UpdateClienteDto clienteDto);
        Task DesativarAsync(int id);
        Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim);
    }
}
