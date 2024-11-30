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

    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os vendedores", Description = "Retorna uma lista com todos os vendedores cadastrados.")]
    [ProducesResponseType(typeof(IEnumerable<VendedorDto>), 200)]
    public async Task<IActionResult> ObterTodos()
    {
        try
        {
            var vendedores = await _vendedorService.ObterTodosAsync();
            return Ok(vendedores);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter vendedores: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Obter vendedor por ID", Description = "Retorna os detalhes de um vendedor específico pelo ID.")]
    [ProducesResponseType(typeof(VendedorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        try
        {
            var vendedor = await _vendedorService.ObterPorIdAsync(id);
            if (vendedor == null)
                return NotFound("Vendedor não encontrado.");
            return Ok(vendedor);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter vendedor por ID: {ex.Message}");
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Criar vendedor", Description = "Cria um novo vendedor.")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Adicionar([FromBody] CreateVendedorDto vendedorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _vendedorService.AdicionarAsync(vendedorDto);
            return Created("Vendedor criado com sucesso.", null);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar vendedor: {ex.Message}");
        }
    }

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
            return BadRequest($"Erro ao atualizar vendedor: {ex.Message}");
        }
    }

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
            return BadRequest($"Erro ao desativar vendedor: {ex.Message}");
        }
    }

    [HttpGet("VendasNoPeriodo")]
    [SwaggerOperation(Summary = "Vendas no período", Description = "Retorna o valor total das vendas realizadas em um período específico.")]
    [ProducesResponseType(typeof(decimal), 200)]
    public async Task<IActionResult> VendasNoPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        try
        {
            var totalVendas = await _vendedorService.ObterTotalVendasNoPeriodoAsync(inicio, fim);
            return Ok(totalVendas);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter vendas no período: {ex.Message}");
        }
    }

    [HttpGet("MelhorCliente")]
    [SwaggerOperation(Summary = "Melhor cliente", Description = "Retorna o cliente que mais comprou (valor total de pedidos).")]
    [ProducesResponseType(typeof(ClienteDto), 200)]
    public async Task<IActionResult> MelhorCliente()
    {
        try
        {
            var cliente = await _vendedorService.ObterMelhorClienteAsync();
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter o melhor cliente: {ex.Message}");
        }
    }

    [HttpGet("ObterTotalVendas/{codigoVendedor}")]
    [SwaggerOperation(Summary = "Obter dados via stored procedure", Description = "Retorna as vendas realizadas a partir de uma stored procedure.")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ObterTotalVendasPorCodigoVendedor(string codigoVendedor)
    {
        try
        {
            var vendas = await _vendedorService.ObterTotalVendasPorCodigoVendedorAsync(codigoVendedor);
            return Ok(vendas);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter vendas por código de vendedor: {ex.Message}");
        }
    }
}
