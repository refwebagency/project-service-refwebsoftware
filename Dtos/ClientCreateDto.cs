using System.ComponentModel.DataAnnotations;

namespace project_service_refwebsoftware.Dtos
{
    public class ClientCreateDto
    {
        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}