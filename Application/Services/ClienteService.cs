using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto?> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
        }

        public async Task AdicionarAsync(CreateClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task AtualizarAsync(Guid id, UpdateClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente não encontrado.");

            // Atualiza os dados
            cliente.SetRazaoSocial(clienteDto.RazaoSocial ?? "");
            cliente.SetNomeFantasia(clienteDto.NomeFantasia ?? "");
            cliente.SetLogradouro(clienteDto.Logradouro ?? "");
            cliente.SetBairro(clienteDto.Bairro ?? "");
            cliente.SetCidade(clienteDto.Cidade ?? "");
            cliente.SetEstado(clienteDto.Estado ?? "");
            if (clienteDto.Ativo) cliente.Ativar();
            else cliente.Desativar();

            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _clienteRepository.RemoverAsync(id);
        }
    }

}
