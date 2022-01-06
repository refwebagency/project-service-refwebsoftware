using ProjectService.Dtos;
using System.Threading.Tasks;

namespace ProjectService.SyncDataService.Http
{
    public interface ITodoDataClient
    {
        Task SendProjectToTodo(ProjectReadDto project);
    }
}