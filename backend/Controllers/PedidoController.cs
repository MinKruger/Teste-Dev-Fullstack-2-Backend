using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/pedidos")]
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
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

        try
        {
            await _pedidoService.AdicionarAsync(pedidoDto);
            return Created("Pedido criado com sucesso.", null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UpdatePedidoDto pedidoDto)
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            await _pedidoService.ExcluirAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
