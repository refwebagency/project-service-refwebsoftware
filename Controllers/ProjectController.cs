using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using project_service_refwebsoftware.Data;
using project_service_refwebsoftware.Dtos;
using project_service_refwebsoftware.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace project_service_refwebsoftware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepo _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
    

        public ProjectController(IProjectRepo repository, IMapper mapper, HttpClient httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
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

        //Pour retourner un objet par L'id
        [HttpGet("projecttype/{id}", Name = "GetProjectByProjectTypeId")]
        public ActionResult<ProjectReadDto> GetProjectByProjectTypeId(int id)
        {
            var ProjectItem = _repository.GetProjectsByProjectTypeId(id);

            if(ProjectItem != null)
            {   
                var test = _mapper.Map<IEnumerable<ProjectReadDto>>(ProjectItem);
                Console.WriteLine(test);
            return Ok(_mapper.Map<IEnumerable<ProjectReadDto>>(ProjectItem));
            }
            else
            {
                return NotFound();
            }
        }

        //Pour retourner un objet par L'id
        [HttpGet("client/{id}", Name = "GetProjectByClientId")]
        public ActionResult<ProjectReadDto> GetProjectByClientId(int id)
        {
            var ProjectItem = _repository.GetProjectsByClientId(id);

            if(ProjectItem != null)
            {
            return Ok(_mapper.Map<IEnumerable<ProjectReadDto>>(ProjectItem));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectReadDto>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            var projectModel = _mapper.Map<Project>(projectCreateDto);

            // requete http en async pour recuperer sur clientService un client par son id stock dans une variable
            var getClient = await _httpClient.GetAsync("https://localhost:1001/Client/" + projectModel.ClientId); 

            // requete http en async pour recuperer sur projectTypeService un type de projet par son id et stocke dans une variable
            var getProjectType = await _httpClient.GetAsync("https://localhost:4001/projecttype/" + projectModel.ProjectTypeId); 

            // deserialisation de l'objet client
            var client = JsonConvert.DeserializeObject<ClientReadDto>(
                await getClient.Content.ReadAsStringAsync()
                );

            // deserialisation de l'objet projecttype
            var projectType = JsonConvert.DeserializeObject<ProjectTypeReadDto>(
                await getProjectType.Content.ReadAsStringAsync()
                );

            // mapping des données deserialisés 
            var clientMap = _mapper.Map<Client>(client);
            var projectTypeMap = _mapper.Map<ProjectType>(projectType);

            var clientExternalId = _repository.GetClientById(clientMap.Id);
            var projectTypeExternalId = _repository.GetProjectTypeById(projectTypeMap.Id);

            /**
            * condition qui suivant à la valeur de clienExternalID
            * et projectExternalId attache l'objet ou une valeur de l'objet
            */
            if (clientExternalId != null && projectTypeExternalId != null)
            {
                projectModel.ClientId = clientMap.Id; 
                projectModel.ProjectTypeId = projectTypeMap.Id;
            }
            else if (clientExternalId != null && projectTypeExternalId == null)
            {
                projectModel.ClientId = clientMap.Id;
                projectModel.projectType = projectTypeMap; // j'attache le nouveau type de projet au projet
            }
            else if (clientExternalId == null && projectTypeExternalId != null)
            {
                projectModel.client = clientMap; // j'attache le nouveau client au projet
                projectModel.ProjectTypeId = projectTypeMap.Id; 
            }
            else
            {
                projectModel.projectType = projectTypeMap; // j'attache le nouveau type de projet au projet
                projectModel.client = clientMap; // j'attache le nouveau client au projet
            }
       
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
