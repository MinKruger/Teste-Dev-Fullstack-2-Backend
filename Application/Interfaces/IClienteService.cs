using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto?> ObterPorIdAsync(Guid id);
        Task AdicionarPorCnpjAsync(string cnpj);
        Task AtualizarAsync(Guid id, UpdateClienteDto clienteDto);
        Task DesativarAsync(Guid id);
        Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim);
    }
}
