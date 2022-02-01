using System.ComponentModel.DataAnnotations;


namespace project_service_refwebsoftware.Models
{
    public class ProjectType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}