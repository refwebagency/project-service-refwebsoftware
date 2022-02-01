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
    [Route("project/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IProjectRepo _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
    

        public ClientController(IProjectRepo repository, IMapper mapper, HttpClient httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        //retoruner tous les clients
        [HttpGet]
        public ActionResult<IEnumerable<ProjectReadDto>> GetAllClientInProjects()
        {
            

            var ClientItem = _repository.GetAllClientInProjects();

            //Retourne un statut 200, qui affiche le resultat
            return Ok(_mapper.Map<IEnumerable<ClientReadDto>>(ClientItem));
        }
    }
}