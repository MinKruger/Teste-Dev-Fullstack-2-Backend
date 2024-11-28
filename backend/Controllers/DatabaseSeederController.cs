using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("PopularBanco")]
        public async Task<IActionResult> PopularBancoDeDados()
        {
            await _seederService.PopularBancoDeDadosAsync();
            return Ok("Banco de dados populado com sucesso!");
        }
    }
}
