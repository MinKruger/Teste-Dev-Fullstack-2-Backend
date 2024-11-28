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
            var clientes = await _clienteRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto?> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            return _mapper.Map<ClienteDto?>(cliente);
        }

        public async Task AdicionarPorCnpjAsync(string cnpj)
        {
            // Lógica para consumir API externa para buscar dados pelo CNPJ
            var dadosCliente = await _externalApiService.ObterDadosPorCnpjAsync(cnpj);
            var cliente = _mapper.Map<Cliente>(dadosCliente);
            await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task AtualizarAsync(Guid id, UpdateClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            _mapper.Map(clienteDto, cliente);
            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task DesativarAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            cliente.Desativar();
            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task<decimal> ObterTotalComprasNoPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _clienteRepository.ObterTotalComprasNoPeriodoAsync(inicio, fim);
        }
    }
}
