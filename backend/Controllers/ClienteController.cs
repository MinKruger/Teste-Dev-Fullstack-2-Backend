using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/clientes")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound("Cliente não encontrado.");
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
            return BadRequest("CNPJ é obrigatório.");

        try
        {
            await _clienteService.AdicionarPorCnpjAsync(cnpj);
            return Created("Cliente criado com sucesso.", null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UpdateClienteDto clienteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _clienteService.AtualizarAsync(id, clienteDto);
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
            await _clienteService.DesativarAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ComprasNoPeriodo")]
    public async Task<IActionResult> ComprasNoPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var totalCompras = await _clienteService.ObterTotalComprasNoPeriodoAsync(inicio, fim);
        return Ok(totalCompras);
    }
}
