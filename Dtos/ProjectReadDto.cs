using System;
using project_service_refwebsoftware.Models;

namespace project_service_refwebsoftware.Dtos
{
    public class ProjectReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }   

        public DateTime EndtDate { get; set; }
        
        public int ProjectTypeId { get; set; }
        
        public int ClientId { get; set; }

        public Client client { get; set; }

        public ProjectType projectType { get; set; }

    }
}