using System;

namespace project_service_refwebsoftware.Dtos
{
    public class ProjectUpdateAsyncDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }   

        public DateTime EndtDate { get; set; }

        public int ClientId { get; set; }

        public string Event { get; set; }
    }

}