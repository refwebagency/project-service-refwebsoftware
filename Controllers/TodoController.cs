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

    }
}