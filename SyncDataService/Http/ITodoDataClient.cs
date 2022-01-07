using ProjectService.Dtos;
using ProjectService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectService.SyncDataServices.Http
{
    public interface ITodoDataClient
    {
        Task<Todo> GetTodoByProjectId(int id);
    }
}