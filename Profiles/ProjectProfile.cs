using AutoMapper;
using project_service_refwebsoftware.Models;
using project_service_refwebsoftware.Dtos;

namespace project_service_refwebsoftware.Profiles
{
    public class ProjectProfile : Profile
    {
        //ici je configure le mapping entre mon model et mon dto
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
            CreateMap<Client, ClientReadDto>();
            CreateMap<ClientReadDto, Client>();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ProjectType, ProjectTypeReadDto>();
            CreateMap<ProjectTypeReadDto, ProjectType>();

            //RabbitMQ
            CreateMap<ClientUpdatedDto, Client>();
            CreateMap<Client, ClientUpdatedDto>();

            CreateMap<ProjectType, ProjectTypeUpdateDto>();
            CreateMap<ProjectTypeUpdateDto, ProjectType>();
        }
    }
}
