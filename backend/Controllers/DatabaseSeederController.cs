using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseSeederController : ControllerBase
    {
        private readonly IDatabaseSeederService _seederService;

        public DatabaseSeederController(IDatabaseSeederService seederService)
        {
            _seederService = seederService;
        }

        /// <summary>
        /// Método Seeder para popular o banco com dados fictícios
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Populador de banco", Description = "Faz a geração de dados fictícios para manipulação e teste de rotas. " +
            "É gerado 10 entradas para cliente, 5 para vendedor e 30 para pedidos sempre que a rota for chamada.")]
        [HttpPost("PopularBanco")]
        public async Task<IActionResult> PopularBancoDeDados()
        {
            await _seederService.PopularBancoDeDadosAsync();
            return Ok("Banco de dados populado com sucesso!");
        }
    }
}
