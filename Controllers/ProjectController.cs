using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Data;
using ProjectService.Dtos;
using ProjectService.Models;


namespace ProjectService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepo _repository;
        private readonly IMapper _mapper;
    

        public ProjectController(IProjectRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /**Pour mettre en forme des résulat de GetAllProjects, 
        pour avoir tous les projets sous forme de liste
        de type Dto
        */
        [HttpGet]
        public ActionResult<IEnumerable<ProjectReadDto>> GetAllProjects()
        {
            //Console.WriteLine est équivalent à console.log
            Console.WriteLine("GetProjects");

            var ProjectItem = _repository.GetAllProjects();

            //Retourne un statut 200, qui affiche le resultat
            return Ok(_mapper.Map<IEnumerable<ProjectReadDto>>(ProjectItem));
        }

        //Pour retourner un objet par L'id
        [HttpGet("{id}", Name = "GetProjectById")]
        public ActionResult<ProjectReadDto> GetProjectById(int id)
        {
            var ProjectItem = _repository.GetProjectById(id);

            if(ProjectItem != null)
            {
            return Ok(_mapper.Map<ProjectReadDto>(ProjectItem));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<ProjectReadDto> CreateProject(ProjectCreateDto projectCreateDto)
        {
            var projectModel = _mapper.Map<Project>(projectCreateDto);

            _repository.CreateProject(projectModel);
            //sauvegarde les changements au niveau des données
            _repository.SaveChanges();

            var projectReadDto = _mapper.Map<ProjectReadDto>(projectModel);

            //reourne une route qui renvoie avec un project specifique
            return CreatedAtRoute(nameof(GetProjectById), new { Id = projectReadDto.Id }, projectReadDto);
        }

        //Pour modifier un projet par son id
        //declaration d'une variable qui récupere comme valeur 
        //le resulat de la méthode GetPrjectById, qui elle même
        // se trouve dans le repository ProjectRepos.cs
        [HttpPut("{id}", Name = "UpdateProject")]
        public ActionResult<ProjectReadDto> UpdateProject(int id, ProjectUpdateDto projectUpdateDto)
        {
            var projectModelFromRepo = _repository.GetProjectById(id);
            _mapper.Map(projectUpdateDto, projectModelFromRepo);
            if (projectModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.UpdateProject(id);
            _repository.SaveChanges();
            
            return CreatedAtRoute(nameof(GetProjectById), new { Id = projectUpdateDto.Id }, projectUpdateDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProjectById(int id)
        {
            var projectId = _repository.GetProjectById(id);
            if (projectId != null)
            {
                _repository.DeleteProjectById(projectId.Id);
                _repository.SaveChanges();
                return Ok();
            }else{
                return NotFound();
            }
        }


        
    }
}
