using System.Collections.Generic;
using project_service_refwebsoftware.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;

// Sur ce fichier j'implémente le contenu des méthodes

namespace project_service_refwebsoftware.Data
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

            // je recupere le client qui vient d'être crée
            _context.project.Add(project);        
            }
            else
            {
                throw new System.ArgumentNullException(nameof(project));
            }
            
        }

        /**
        * Je passe un project à mon IEnumerable
        * je veux qu'il me retourne suivant le context les projects sous forme de liste
        */
        public IEnumerable<Project> GetAllProjects()
        {
            _context.projectType.ToList();
            _context.client.ToList();
            return _context.project.ToList();
        }
        
        // je veux qu'il me retourne suivant le context les client sous forme de liste
        public IEnumerable<Client> GetAllClientInProjects()
        {
            return _context.client.ToList();
        }

        // je veux qu'il me retourne suivant le context les types de projects sous forme de liste
        public IEnumerable<ProjectType> GetAllProjectTypeInProject()
        {
            return _context.projectType.ToList();
        }

        /**
        * je veux qu'il me retourne suivant le context un project par l'id de type int
        * ça va me récuperé le premier ou par default
        * c'est à dire le project.id qui est == à l'id qui est en paramètres
        */
        public Project GetProjectById(int id)
        {
            _context.projectType.ToList();
            _context.client.ToList();
            return _context.project.FirstOrDefault(p => p.Id == id);
        }

        public Client GetClientById(int id)
        {
            return _context.client.FirstOrDefault(c => c.Id == id);
        }

        public ProjectType GetProjectTypeById(int id)
        {
            return _context.projectType.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Project> GetProjectsByProjectTypeId(int id)
        {
            _context.projectType.ToList();
            _context.client.ToList();
            return _context.project.Where(pt => pt.ProjectTypeId == id).ToList();
        }

        public IEnumerable<Project> GetProjectsByClientId(int id)
        {
            _context.projectType.ToList();
            _context.client.ToList();
            return _context.project.Where(cp => cp.ClientId == id).ToList();
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
