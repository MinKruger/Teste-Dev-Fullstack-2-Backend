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
            // URL da API ReceitaWS para consulta de CNPJ
            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

            // Envia a requisição GET para a API externa
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao obter dados da empresa. Status Code: {response.StatusCode}");
            }

            // Lê o conteúdo da resposta
            var responseContent = await response.Content.ReadAsStringAsync();

            // Converte o conteúdo para um objeto JSON
            var json = JObject.Parse(responseContent);

            // Verifica se há erro na resposta
            if (json["status"]?.ToString() != "OK")
            {
                throw new HttpRequestException($"Erro na consulta: {json["message"]?.ToString()}");
            }

            // Mapeia os dados para o ClienteDto
            var clienteDto = new ClienteDto
            {
                RazaoSocial = json["nome"]?.ToString(),
                NomeFantasia = json["fantasia"]?.ToString(),
                CNPJ = json["cnpj"]?.ToString(),
                Logradouro = json["logradouro"]?.ToString(),
                Bairro = json["bairro"]?.ToString(),
                Cidade = json["municipio"]?.ToString(),
                Estado = json["uf"]?.ToString(),
                Ativo = true // Presume que empresas consultadas estão ativas
            };

            return clienteDto;
        }
    }
}
