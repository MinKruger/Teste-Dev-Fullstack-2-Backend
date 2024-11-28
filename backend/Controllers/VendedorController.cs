using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;

        public VendedorController(IVendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var vendedores = await _vendedorService.ObterTodosAsync();
            return Ok(vendedores);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var vendedor = await _vendedorService.ObterPorIdAsync(id);
            if (vendedor == null)
                return NotFound("Vendedor não encontrado.");
            return Ok(vendedor);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CreateVendedorDto vendedorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _vendedorService.AdicionarAsync(vendedorDto);
            return CreatedAtAction(nameof(ObterPorId), new { id = vendedorDto }, vendedorDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateVendedorDto vendedorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _vendedorService.AtualizarAsync(id, vendedorDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            try
            {
                await _vendedorService.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
