using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace ProjectService.Dtos
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }   

        [Required]
        public DateTime EndtDate { get; set; }
    }
}