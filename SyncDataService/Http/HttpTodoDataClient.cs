using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectService.Dtos;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProjectService.SyncDataService.Http
{
    public class HttpTodoDataClient : ITodoDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpTodoDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendProjectToTodo(ProjectReadDto project)
        {

            var httpContent = new StringContent(
                JsonSerializer.Serialize(project),
                Encoding.UTF8,
                "application/json");

            Console.WriteLine("NTM 1 ");
            var response = await _httpClient.PostAsync($"{_configuration["TodoService"]}", httpContent);
            Console.WriteLine("NTM 2 ");
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(" Request POST send to Todo Service");
            }
            else 
            {
                Console.WriteLine("ERROR: Request POST not send to Todo Service");
            }
        }
    }
}