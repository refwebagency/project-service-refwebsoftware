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
        public void CreateProject(Project project, Client client)
        {
            if(project != null)
            {
            //_context.Projects equivaut a bdd.Projects


            // _context.project.Add(project);
            // Console.WriteLine(project.Name);
            // Console.WriteLine(project.client.LastName);

            //  dynamic data = JObject.Parse(project.client.ToString());
            //  Console.WriteLine(data.Name);
            //JArray array = JArray.Parse(project);
            
            var NewClient = new Client
            {
                ExternalId = client.ExternalId,
                Name = client.Name,
                LastName = client.LastName
            };

            //Console.WriteLine(NewClient.ExternalId);
            _context.client.Attach(NewClient);

            // // je recupere le client qui vient d'être crée

            var clientCreate = _context.client.Find(NewClient.Id);

            Console.WriteLine(clientCreate.ExternalId);
            

            
            // var newProject = new Project
            // {
            //     Name = project.Name,
            //     StartDate = project.StartDate,
            //     EndtDate = project.EndtDate,
            //     ProjectTypeId = project.ProjectTypeId,
            //     projectType = project.projectType,
            //     ClientId = project.ClientId,
            //     client = clientCreate
                
            // };



            Console.WriteLine(project.client.LastName);
            
            // newProject.client = new Client(){
            //     Name = project.client.Name,
            //     LastName = project.client.LastName
            // };

            project.client = clientCreate;

            _context.project.Add(project);

            //CreateClientInProject(newProject.client);
            // _context.client.Add(newClient);
            _context.SaveChanges();
            
            }
            else
            {
                Console.WriteLine("Client pas trouvé");
            }
            
        }

        public void CreateClientInProject(Client client)
        {
            if(client != null)
            {
            
            _context.client.Add(client);
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
        
        public IEnumerable<Client> GetAllClientInProjects()
        {
            return _context.client.ToList();
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

        public IEnumerable<Project> GetProjectsByProjectTypeId(int id)
        {
            return _context.project.Where(pt => pt.ProjectTypeId == id).ToList();
        }

        public IEnumerable<Project> GetProjectsByClientId(int id)
        {
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
