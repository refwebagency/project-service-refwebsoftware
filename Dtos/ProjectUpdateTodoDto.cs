using System.ComponentModel.DataAnnotations;
using System;
using ProjectService.Models;
using System.Collections.Generic;

namespace ProjectService.Dtos
{
    public class ProjectUpdateTodoDto
    {
        [Required]
        public int Id { get; set; }
 
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}