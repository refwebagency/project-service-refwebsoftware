using System.ComponentModel.DataAnnotations;

namespace project_service_refwebsoftware.Models
{
    public class Client
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}