using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace ProjectService.Dtos
{
    public class ProjectReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }   

        public DateTime EndtDate { get; set; }
        

    }
}