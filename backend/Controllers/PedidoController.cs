using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var pedidos = await _pedidoService.ObterTodosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var pedido = await _pedidoService.ObterPorIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido não encontrado.");
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CreatePedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pedidoService.AdicionarAsync(pedidoDto);
            return CreatedAtAction(nameof(ObterPorId), new { id = pedidoDto }, pedidoDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdatePedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoService.AtualizarAsync(id, pedidoDto);
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
                await _pedidoService.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
