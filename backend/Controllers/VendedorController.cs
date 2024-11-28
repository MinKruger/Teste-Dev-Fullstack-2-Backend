using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/vendedores")]
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UpdateVendedorDto vendedorDto)
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desativar(int id)
    {
        try
        {
            await _vendedorService.DesativarAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("VendasNoPeriodo")]
    public async Task<IActionResult> VendasNoPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var totalVendas = await _vendedorService.ObterTotalVendasNoPeriodoAsync(inicio, fim);
        return Ok(totalVendas);
    }

    [HttpGet("MelhorCliente")]
    public async Task<IActionResult> MelhorCliente()
    {
        var cliente = await _vendedorService.ObterMelhorClienteAsync();
        return Ok(cliente);
    }
}
