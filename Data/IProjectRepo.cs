using System.Collections.Generic;
using ProjectService.Models;

namespace ProjectService.Data
{
    //Je definie l'interface IProjectRepo avec ses méthodes, proprietés, indexeurs et évenements à implémenter
    public interface IProjectRepo
    {
        //SaveChanges() permet de sauvegarder les changements dans la base de données
        bool SaveChanges();

        //IEnumerable retourne une liste
        IEnumerable<Project> GetAllProjects(); //récupere une liste de projets
        Project GetProjectById(int projectId); //récupere un projet par l'id 

        void CreateProject(Project project); //pour créer un projet

        void UpdateProject(int projectId); //pour mettre à jour un projet

        void DeleteProjectById(int projectId); //pour supprimer un projet
    }
}