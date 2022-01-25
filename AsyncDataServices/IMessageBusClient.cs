using project_service_refwebsoftware.Dtos;

namespace project_service_refwebsoftware.AsyncDataService
{
    public interface IMessageBusClient
    {
        void UpdatedProject(ProjectUpdateAsyncDto projectUpdatedDto);
    }
}