using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProjectService.Models
{
    public class Project
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }        

        [Required]
        public DateTime EndtDate { get; set; }

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();

        // public ProjectType

        // public Client
    }
}