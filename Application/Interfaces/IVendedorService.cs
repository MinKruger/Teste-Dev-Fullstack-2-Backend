﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDto>> ObterTodosAsync();
        Task<VendedorDto?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(CreateVendedorDto vendedorDto);
        Task AtualizarAsync(Guid id, UpdateVendedorDto vendedorDto);
        Task RemoverAsync(Guid id);
    }
}
