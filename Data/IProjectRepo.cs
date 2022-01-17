using System.Collections.Generic;
using project_service_refwebsoftware.Models;

namespace project_service_refwebsoftware.Data
{
    //Je definie l'interface IProjectRepo avec ses méthodes, proprietés, indexeurs et évenements à implémenter
    public interface IProjectRepo
    {
        //SaveChanges() permet de sauvegarder les changements dans la base de données
        bool SaveChanges();

        //IEnumerable retourne une liste
        IEnumerable<Project> GetAllProjects(); //récupere une liste de projets

        IEnumerable<Client> GetAllClientInProjects(); //récupere une liste de clients

        IEnumerable<Project> GetProjectsByProjectTypeId(int id); //récupere une liste de projets par type de projet

        IEnumerable<Project> GetProjectsByClientId(int id); //récupere une liste de projets par client

        Project GetProjectById(int projectId); //récupere un projet par l'id 

        Client GetClientById(int clientId); //récupere un client par l'id

        void CreateProject(Project project); //pour créer un projet

        IEnumerable<ProjectType> GetAllProjectTypeInProject(); //pour retourner tous les types de projets

        ProjectType GetProjectTypeById(int ProjectTypeId);

        void UpdateProject(int projectId); //pour mettre à jour un projet

        void DeleteProjectById(int projectId); //pour supprimer un projet
    }
}