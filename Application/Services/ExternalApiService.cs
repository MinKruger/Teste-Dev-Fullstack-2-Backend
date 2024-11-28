using System.Net.Http;
using System.Threading.Tasks;
using Application.DTOs;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClienteDto> ObterDadosPorCnpjAsync(string cnpj)
        {
            // URL da API de mockagem que retorna dados de empresa com base no CNPJ
            var url = $"https://www.4devs.com.br/ferramentas_online.php";

            // Parâmetros necessários para a requisição
            var parameters = new Dictionary<string, string>
            {
                { "acao", "gerar_empresa" },
                { "txt_cnpj", cnpj },
                { "pontuacao", "N" }
            };

            var content = new FormUrlEncodedContent(parameters);

            // Envia a requisição POST para a API externa
            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erro ao obter dados da empresa.");
            }

            // Lê o conteúdo da resposta
            var responseContent = await response.Content.ReadAsStringAsync();

            // Converte o conteúdo para um objeto JSON
            var json = JObject.Parse(responseContent);

            // Mapeia os dados para o DTO de Empresa
            var empresaDto = new ClienteDto
            {
                RazaoSocial = json["nome"]?.ToString(),
                NomeFantasia = json["fantasia"]?.ToString(),
                CNPJ = json["cnpj"]?.ToString(),
                Logradouro = json["endereco"]?.ToString(),
                Bairro = json["bairro"]?.ToString(),
                Cidade = json["cidade"]?.ToString(),
                Estado = json["estado"]?.ToString(),
                Ativo = true // Considera que a empresa está ativa
            };

            return empresaDto;
        }
    }
}
