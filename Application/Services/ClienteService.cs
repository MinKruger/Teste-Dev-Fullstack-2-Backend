using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ExternalApiService _externalApiService;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ExternalApiService externalApiService)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _externalApiService = externalApiService;
        }

        public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
        {
            try
            {
                var clientes = await _clienteRepository.ObterTodosAsync();
                return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDto?> ObterPorIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                return _mapper.Map<ClienteDto?>(cliente);
            }
            catch
            {
                throw;
            }
        }

        public async Task AdicionarPorCnpjAsync(string cnpj)
        {
            try
            {
                var dadosCliente = await _externalApiService.ObterDadosPorCnpjAsync(cnpj);
                var cliente = _mapper.Map<Cliente>(dadosCliente);
                await _clienteRepository.AdicionarAsync(cliente);
            }
            catch
            {
                throw;
            }
        }

        public async Task AtualizarAsync(int id, UpdateClienteDto clienteDto)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }

                _mapper.Map(clienteDto, cliente);
                await _clienteRepository.AtualizarAsync(cliente);
            }
            catch
            {
                throw;
            }
        }

        public async Task DesativarAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }

                cliente.Desativar();
                await _clienteRepository.AtualizarAsync(cliente);
            }
            catch
            {
                throw;
            }
        }

        public async Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                return await _clienteRepository.ObterTotalComprasNoPeriodoAsync(inicio, fim);
            }
            catch
            {
                throw;
            }
        }
    }
}
