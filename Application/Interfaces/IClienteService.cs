using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(CreateClienteDto clienteDto);
        Task AtualizarAsync(Guid id, UpdateClienteDto clienteDto);
        Task RemoverAsync(Guid id);
    }
}
