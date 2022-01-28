using System;
using System.Text.Json;
using AutoMapper;
using project_service_refwebsoftware.Data;
using project_service_refwebsoftware.Dtos;
using project_service_refwebsoftware.Models;
using project_service_refwebsoftware.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;

namespace project_service_refwebsoftware.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IMapper _mapper;

        private readonly HttpClient _httpClient;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper,HttpClient httpClient)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                // On souscrit à la méthode UpdateClient() si la valeur retournée est bien EventType
                case EventType.ClientUpdated:
                    UpdateClient(message);
                    break;
                case EventType.ProjectTypeUpdated:
                    UpdateProjectType(message);
                    break;
                case EventType.ProjectCreated:
                    CreateProject(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            // On déserialise les données pour retourner un objet (texte vers objet json)
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            Console.WriteLine($"--> Event Type: {eventType.Event}");

            switch(eventType.Event)
            {
                /* "Client_Updated" est la valeur attribuée dans le controller de ClientService
                lors de l'envoi de données 
                Dans le cas ou la valeur de notre attribue Event est bien "Client_Updated",
                nous retournons notre objet */
                case "Client_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.ClientUpdated;
                case "ProjectType_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.ProjectTypeUpdated;
                case "Project_Published":
                    Console.WriteLine("--> Platform Created Event Detected");
                    return EventType.ProjectCreated;
                // Sinon nous retournons que l'objet est indeterminé
                default:
                    Console.WriteLine("-> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void UpdateClient(string clientUpdatedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                // Recuperation du scope de IprojectRepo
                var repo = scope.ServiceProvider.GetRequiredService<IProjectRepo>();

                //On deserialize le clientUpdatedMessage
                var clientUpdatedDto = JsonSerializer.Deserialize<ClientUpdatedDto>(clientUpdatedMessage);
                Console.WriteLine($"--> Client Updated: {clientUpdatedDto}");

                try
                {

                    //Console.WriteLine(clientUpdatedDto.Name);

                    var clientRepo = repo.GetClientById(clientUpdatedDto.Id);
                    _mapper.Map(clientUpdatedDto, clientRepo);
                    
                    // SI le client existe bien on l'update sinon rien
                    if(clientRepo != null)
                    {
                        //Console.WriteLine(clientRepo.Name);
                        repo.UpdateClientById(clientRepo.Id);
                        //Console.WriteLine(clientRepo.Name);
                        repo.SaveChanges();
                        Console.WriteLine("--> Client mis à jour");
                    }
                    else{
                        Console.WriteLine("--> Client non existant");
                    }
                }
                // Si une erreur survient, on affiche un message
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update Client to DB {ex.Message}");
                }
            }
        }

        private void UpdateProjectType(string projectTypeDto)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProjectRepo>();

                var projectTypeUpdateDto = JsonSerializer.Deserialize<ProjectTypeUpdateDto>(projectTypeDto);
                Console.WriteLine($"--> ProjectType Updated: {projectTypeUpdateDto}");

                try
                {

                    var projectTypeRepo = repo.GetProjectTypeById(projectTypeUpdateDto.Id);
                    _mapper.Map(projectTypeUpdateDto, projectTypeRepo);
                    
                    // SI le client existe bien on l'update sinon rien
                    if(projectTypeRepo != null)
                    {
                        //Console.WriteLine(clientRepo.Name);
                        repo.UpdateProjectTypeById(projectTypeRepo.Id);
                        //Console.WriteLine(clientRepo.Name);
                        repo.SaveChanges();
                        Console.WriteLine("--> ProjectType mis à jour");
                    }
                    else{
                        Console.WriteLine("--> ProjectType non existant");
                    }
                }
                // Si une erreur survient, on affiche un message
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update ProjectType to DB {ex.Message}");
                }
            }
        }

        private void CreateProject(string projectCreatedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                // Recuperation du scope de IProjectRepo
                var repo = scope.ServiceProvider.GetRequiredService<IProjectRepo>();
                var controller = scope.ServiceProvider.GetRequiredService<ProjectController>();

                //On deserialize le clientUpdatedMessage
                var projectCreatedDto = JsonSerializer.Deserialize<ProjectCreatedDto>(projectCreatedMessage);
                Console.WriteLine($"--> Project Created: {projectCreatedDto}");

                try
                {

                    //Console.WriteLine(projectCreatedDto.Name);

                    var projectRepo = repo.GetProjectById(projectCreatedDto.Id);
                    
                    // SI le project existe ou non
                    if(projectRepo != null)
                    {                  
                        Console.WriteLine("--> Project déjà existant");
                    }
                    else{
                        var httpContent = new StringContent(JsonSerializer.Serialize(projectCreatedMessage), 
                        Encoding.UTF8,
                        "application/json"
                        );
                        var newProject = _mapper.Map<ProjectCreateDto>(projectCreatedDto);
                        controller.CreateProject(newProject);
                        
                        Console.WriteLine("--> Project crée");
                    }
                }
                // Si une erreur survient, on affiche un message
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not create project to DB {ex.Message}");
                }
            }
        }
    }

    //Type d'event
    enum EventType
    {
        ClientUpdated, //clientService
        ProjectTypeUpdated, //projectTypeService
        ProjectCreated, //quotePDFService
        Undetermined
    }
}