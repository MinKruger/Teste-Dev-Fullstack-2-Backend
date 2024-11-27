using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.ObterTodosAsync();
            return Ok(clientes);
        }

        // GET: api/Cliente/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clienteService.AdicionarAsync(clienteDto);
            return CreatedAtAction(nameof(GetById), new { id = clienteDto }, clienteDto);
        }

        // PUT: api/Cliente/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _clienteService.AtualizarAsync(id, clienteDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Cliente/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _clienteService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
