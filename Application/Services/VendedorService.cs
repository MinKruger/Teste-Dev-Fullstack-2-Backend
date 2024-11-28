using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;

namespace Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;

        public VendedorService(IVendedorRepository vendedorRepository, IMapper mapper)
        {
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendedorDto>> ObterTodosAsync()
        {
            var vendedores = await _vendedorRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<VendedorDto>>(vendedores);
        }

        public async Task<VendedorDto?> ObterPorIdAsync(Guid id)
        {
            var vendedor = await _vendedorRepository.ObterPorIdAsync(id);
            return _mapper.Map<VendedorDto?>(vendedor);
        }

        public async Task AdicionarAsync(CreateVendedorDto vendedorDto)
        {
            var vendedor = _mapper.Map<Vendedor>(vendedorDto);
            await _vendedorRepository.AdicionarAsync(vendedor);
        }

        public async Task AtualizarAsync(Guid id, UpdateVendedorDto vendedorDto)
        {
            var vendedor = await _vendedorRepository.ObterPorIdAsync(id);
            if (vendedor == null)
            {
                throw new Exception("Vendedor não encontrado.");
            }

            _mapper.Map(vendedorDto, vendedor);
            await _vendedorRepository.AtualizarAsync(vendedor);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _vendedorRepository.RemoverAsync(id);
        }
    }
}
