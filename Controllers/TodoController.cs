using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Models;
using ProjectService.Data;
using ProjectService.Dtos;

namespace ProjectService.Controllers
{
    [Route("project/[controller]")]
    [ApiController]

    public class TodoController : ControllerBase
    {
        private readonly IProjectRepo _repository;
        private readonly IMapper _mapper;


        public TodoController(IProjectRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost("{id}", Name = "AddTodoInProject")]
        public ActionResult<TodoReadDto> AddTodoInProject(int id, TodoCreateDto todoCreateDto){

            // On initialise une variable ou l'on stock le model de creation de la tache
            var todoModel = _mapper.Map<Todo>(todoCreateDto);
            // Ici on recupere la méthode du repo CreateTodo.
            _repository.AddTodoInProject(id, todoModel);
            // On recupere la methode SaveChanges du repo.
            _repository.SaveChanges();
            // On stock dans une variable le schema pour lire la nouvelle tache enregistré précedemment.
            var TodoReadDto = _mapper.Map<TodoReadDto>(todoModel);
            _repository.SaveChanges();
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return Ok(TodoReadDto); 
        }
    }
}