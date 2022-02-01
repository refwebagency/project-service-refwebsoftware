using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace project_service_refwebsoftware.Dtos
{
    public class ProjectCreatedDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }   

        [Required]
        public DateTime EndtDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProjectTypeId { get; set; }

        public string Event { get; set; }
    }
}