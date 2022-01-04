using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectService.Dtos
{
    public class ProjectUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }   

        [Required]
        public DateTime EndtDate { get; set; }
    }
}