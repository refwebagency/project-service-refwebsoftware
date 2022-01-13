using Microsoft.EntityFrameworkCore;
using project_service_refwebsoftware.Models;

namespace project_service_refwebsoftware.Data
{
    public class AppDbContext : DbContext
    {
        // Pont entre notre model et notre BDD
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<Project> project {get; set;}
        public DbSet<Client> client {get; set;}
        public DbSet<ProjectType> projectType {get; set;}
    }
}