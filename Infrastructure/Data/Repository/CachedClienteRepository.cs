using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class CachedClienteRepository : IClienteRepository
    {
        private readonly IClienteRepository _innerRepository;
        private readonly IMemoryCache _cache;

        public CachedClienteRepository(IClienteRepository innerRepository, IMemoryCache cache)
        {
            _innerRepository = innerRepository ?? throw new ArgumentNullException(nameof(innerRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            var cacheKey = $"Cliente-{id}";
            if (!_cache.TryGetValue(cacheKey, out Cliente? cliente))
            {
                cliente = await _innerRepository.ObterPorIdAsync(id);
                if (cliente != null)
                {
                    _cache.Set(cacheKey, cliente, TimeSpan.FromMinutes(5)); // Cache por 5 minutos
                }
            }
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            const string cacheKey = "Clientes-Todos";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Cliente>? clientes))
            {
                clientes = await _innerRepository.ObterTodosAsync();
                _cache.Set(cacheKey, clientes, TimeSpan.FromMinutes(5)); // Cache por 5 minutos
            }
            return clientes;
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _innerRepository.AdicionarAsync(cliente);
            _cache.Remove("Clientes-Todos");
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            await _innerRepository.AtualizarAsync(cliente);
            _cache.Remove($"Cliente-{cliente.Id}");
            _cache.Remove("Clientes-Todos");
        }

        public async Task RemoverAsync(Guid id)
        {
            await _innerRepository.RemoverAsync(id);
            _cache.Remove($"Cliente-{id}");
            _cache.Remove("Clientes-Todos");
        }
    }
}
