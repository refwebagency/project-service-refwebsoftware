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
            CreateMap<ProjectType, ProjectTypeReadDto>();
        }
    }
}
