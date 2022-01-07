using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ProjectService.Dtos;
using ProjectService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ProjectService.SyncDataServices.Http
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

        public async Task GetTodoByProjectId(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:6001/Todo/project/id?id={id}");

            if(response.IsSuccessStatusCode)
            {
                // Si la réponse http est un succés on envoie un message 
                Console.WriteLine(" Request GET sent to Todo service");

                var todo = JsonConvert.DeserializeObject<IEnumerable<Todo>>(
                        await response.Content.ReadAsStringAsync());
                return (Todo)todo;
            }
            else 
            {
                // Si la réponse http est un échec on envoie un message
                Console.WriteLine("ERROR: Request GET not sent to Todo service");
                return null;
            }
        }
    }
} 