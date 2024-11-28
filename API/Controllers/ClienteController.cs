using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/clientes")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Obtém todos os clientes cadastrados.
    /// </summary>
    /// <returns>Lista de clientes.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os clientes", Description = "Retorna uma lista com todos os clientes cadastrados.")]
    [ProducesResponseType(typeof(IEnumerable<ClienteDto>), 200)]
    public async Task<IActionResult> ObterTodos()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Obtém um cliente específico pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <returns>Dados do cliente.</returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Obter cliente por ID", Description = "Retorna os dados de um cliente específico pelo seu ID.")]
    [ProducesResponseType(typeof(ClienteDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound("Cliente não encontrado.");
        return Ok(cliente);
    }

    /// <summary>
    /// Cria um cliente a partir de um CNPJ utilizando uma API externa.
    /// </summary>
    /// <param name="cnpj">CNPJ do cliente.</param>
    /// <returns>Confirmação da criação.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cliente", Description = "Cria um novo cliente utilizando uma API externa para buscar dados a partir do CNPJ.")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <param name="clienteDto">Dados atualizados do cliente.</param>
    /// <returns>Status da atualização.</returns>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar cliente", Description = "Atualiza os dados de um cliente específico pelo ID.")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Desativa um cliente específico pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <returns>Status da desativação.</returns>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Desativar cliente", Description = "Desativa um cliente específico pelo ID.")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Retorna o valor total das compras realizadas em um período.
    /// </summary>
    /// <param name="inicio">Data de início do período.</param>
    /// <param name="fim">Data de fim do período.</param>
    /// <returns>Valor total das compras.</returns>
    [HttpGet("ComprasNoPeriodo")]
    [SwaggerOperation(Summary = "Total de compras no período", Description = "Retorna o valor total das compras realizadas em um período específico.")]
    [ProducesResponseType(typeof(decimal), 200)]
    public async Task<IActionResult> ComprasNoPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var totalCompras = await _clienteService.ObterTotalComprasNoPeriodoAsync(inicio, fim);
        return Ok(totalCompras);
    }
}
