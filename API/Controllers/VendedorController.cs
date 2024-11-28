using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/vendedores")]
[ApiController]
public class VendedorController : ControllerBase
{
    private readonly IVendedorService _vendedorService;

    public VendedorController(IVendedorService vendedorService)
    {
        _vendedorService = vendedorService;
    }

    /// <summary>
    /// Obtém todos os vendedores cadastrados.
    /// </summary>
    /// <returns>Lista de vendedores.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os vendedores", Description = "Retorna uma lista com todos os vendedores cadastrados.")]
    [ProducesResponseType(typeof(IEnumerable<VendedorDto>), 200)]
    public async Task<IActionResult> ObterTodos()
    {
        var vendedores = await _vendedorService.ObterTodosAsync();
        return Ok(vendedores);
    }

    /// <summary>
    /// Obtém os detalhes de um vendedor específico pelo ID.
    /// </summary>
    /// <param name="id">ID do vendedor.</param>
    /// <returns>Detalhes do vendedor.</returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Obter vendedor por ID", Description = "Retorna os detalhes de um vendedor específico pelo ID.")]
    [ProducesResponseType(typeof(VendedorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var vendedor = await _vendedorService.ObterPorIdAsync(id);
        if (vendedor == null)
            return NotFound("Vendedor não encontrado.");
        return Ok(vendedor);
    }

    /// <summary>
    /// Cria um novo vendedor.
    /// </summary>
    /// <param name="vendedorDto">Dados do novo vendedor.</param>
    /// <returns>Confirmação da criação.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar vendedor", Description = "Cria um novo vendedor.")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Adicionar([FromBody] CreateVendedorDto vendedorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _vendedorService.AdicionarAsync(vendedorDto);
        return CreatedAtAction(nameof(ObterPorId), new { id = vendedorDto }, vendedorDto);
    }

    /// <summary>
    /// Atualiza os dados de um vendedor existente.
    /// </summary>
    /// <param name="id">ID do vendedor.</param>
    /// <param name="vendedorDto">Dados atualizados do vendedor.</param>
    /// <returns>Status da atualização.</returns>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar vendedor", Description = "Atualiza os dados de um vendedor específico pelo ID.")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Desativa um vendedor específico pelo ID.
    /// </summary>
    /// <param name="id">ID do vendedor.</param>
    /// <returns>Status da desativação.</returns>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Desativar vendedor", Description = "Desativa um vendedor específico pelo ID.")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Retorna o valor total das vendas realizadas em um período.
    /// </summary>
    /// <param name="inicio">Data de início do período.</param>
    /// <param name="fim">Data de fim do período.</param>
    /// <returns>Valor total das vendas no período.</returns>
    [HttpGet("VendasNoPeriodo")]
    [SwaggerOperation(Summary = "Vendas no período", Description = "Retorna o valor total das vendas realizadas em um período específico.")]
    [ProducesResponseType(typeof(decimal), 200)]
    public async Task<IActionResult> VendasNoPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var totalVendas = await _vendedorService.ObterTotalVendasNoPeriodoAsync(inicio, fim);
        return Ok(totalVendas);
    }

    /// <summary>
    /// Retorna o cliente que mais comprou (valor total de pedidos).
    /// </summary>
    /// <returns>Cliente que mais comprou.</returns>
    [HttpGet("MelhorCliente")]
    [SwaggerOperation(Summary = "Melhor cliente", Description = "Retorna o cliente que mais comprou (valor total de pedidos).")]
    [ProducesResponseType(typeof(ClienteDto), 200)]
    public async Task<IActionResult> MelhorCliente()
    {
        var cliente = await _vendedorService.ObterMelhorClienteAsync();
        return Ok(cliente);
    }
}
