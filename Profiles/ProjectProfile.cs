using AutoMapper;
using ProjectService.Models;
using ProjectService.Dtos;

namespace ProjectService.Profiles
{
    public class ProjectProfile : Profile
    {
        //ici je configure le mapping entre mon model et mon dto
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
        }
    }
}
