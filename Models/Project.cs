using System;
using System.ComponentModel.DataAnnotations;

namespace project_service_refwebsoftware.Models
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

        [Required]
        public int ProjectTypeId { get; set; }

        public ProjectType projectType { get; set;}

        [Required]
        public int ClientId { get; set; }

        public Client client { get; set; }
    }
}