using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/pedidos")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    /// <summary>
    /// Obtém todos os pedidos cadastrados.
    /// </summary>
    /// <returns>Lista de pedidos.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os pedidos", Description = "Retorna uma lista com todos os pedidos cadastrados.")]
    [ProducesResponseType(typeof(IEnumerable<PedidoDto>), 200)]
    public async Task<IActionResult> ObterTodos()
    {
        var pedidos = await _pedidoService.ObterTodosAsync();
        return Ok(pedidos);
    }

    /// <summary>
    /// Obtém os detalhes de um pedido específico pelo ID.
    /// </summary>
    /// <param name="id">ID do pedido.</param>
    /// <returns>Detalhes do pedido.</returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Obter pedido por ID", Description = "Retorna os detalhes de um pedido específico pelo ID.")]
    [ProducesResponseType(typeof(PedidoDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido == null)
            return NotFound("Pedido não encontrado.");
        return Ok(pedido);
    }

    /// <summary>
    /// Cria um novo pedido (somente se vendedor e cliente estiverem ativos).
    /// </summary>
    /// <param name="pedidoDto">Dados do novo pedido.</param>
    /// <returns>Confirmação da criação.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar pedido", Description = "Cria um novo pedido (somente se vendedor e cliente estiverem ativos).")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Atualiza os dados de um pedido existente.
    /// </summary>
    /// <param name="id">ID do pedido.</param>
    /// <param name="pedidoDto">Dados atualizados do pedido.</param>
    /// <returns>Status da atualização.</returns>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar pedido", Description = "Atualiza os dados de um pedido específico pelo ID.")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Exclui um pedido específico pelo ID (somente se não autorizado).
    /// </summary>
    /// <param name="id">ID do pedido.</param>
    /// <returns>Status da exclusão.</returns>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Excluir pedido", Description = "Exclui um pedido específico pelo ID (somente se não autorizado).")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
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
