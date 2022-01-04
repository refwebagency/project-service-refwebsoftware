using System.Collections.Generic;
using ProjectService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectService.Data;

// Sur ce fichier j'implémente le contenu des méthodes

namespace ProjectService.Data
{
    public class ProjectRepo : IProjectRepo
    {
        
        private readonly AppDbContext _context;

        public ProjectRepo(AppDbContext context)
        {

            _context = context;
        }

        /**
        * Creation de projet 
        * si projet n'est pas null alors add pour creer avec project en parametre
        * sauvegarde les changements
        * sinon erreur
        */
        public void CreateProject(Project project)
        {
            if(project != null)
            {
            //_context.Projects equivaut a bdd.Projects
            _context.project.Add(project);
            _context.SaveChanges();
            }
            
        }

        /**
        * Je passe un project à mon IEnumerable
        * je veux qu'il me retourne suivant le context les projects sous forme de liste
        */
        public IEnumerable<Project> GetAllProjects()
        {
            return _context.project.ToList();
        }
        
        /**
        * je veux qu'il me retourne suivant le context un project par l'id de type int
        * ça va me récuperé le premier ou par default
        * c'est à dire le project.id qui est == à l'id qui est en paramètres
        */
        public Project GetProjectById(int id)
        {
            return _context.project.FirstOrDefault(p => p.Id == id);
        }

        /**
        *Pour update un projet je doit en premier le récupere par son id 
        *puis je le modifie par son état
        */
        public void UpdateProject(int id)
        {
            var projectId = _context.project.Find(id);
            _context.Entry(projectId).State = EntityState.Modified;
        }

        public void DeleteProjectById(int id)
        {
            var projectItem = _context.project.Find(id);
            if(projectItem != null)
            {
                _context.project.Remove(projectItem);
            }
            
        }

        /**
        * Pour sauvegarder les changements si dans le context
        * les changements sont >= à 0
        */

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }
    }
}
