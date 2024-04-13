
using System.Net;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;

namespace GraduateWork.Tests;

public class ProjectTests : BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private Project _project;
    private string _etag;

    [Test]
    [Order(1)]
    public void CreateProjectApiTest()
    {
        _logger.Info("CreateProjectApiTest запущен.");

        _project = new Project()
        {
            Name = $"Test {DateTime.Now}",
            Description = "Test Description",
            Etag = "test"
        };

        var actualProject = ProjectService!.AddProject(_project);
        
        Assert.That(actualProject.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        _project = JsonConvert.DeserializeObject<Project>(actualProject.Result.Content);
        
        Assert.Multiple(() =>
        {
            Assert.That(_project.Name, Is.EqualTo(_project.Name));
            _logger.Info($"Name совпал с ожидаемым и равен: {_project.Name}");

            Assert.That(_project.Description, Is.EqualTo(_project.Description));
            _logger.Info($"Description совпал с ожидаемым и равен: {_project.Description}");

            Assert.That(_project.Id, !Is.EqualTo(null));
            _logger.Info($"Id равен: {_project.Id}");
        });

        _logger.Info("CreateProjectApiTest выполнен.");
    }
    
    [Test]
    [Order(2)]
    public void GetProjectApiTest()
    {
        _logger.Info("GetProjectApiTest запущен.");
        
        var getProject = ProjectService!.GetProject(_project.Id);
        
        Assert.Multiple(() =>
        {
            Assert.That(getProject.Result.Name, Is.EqualTo(_project.Name));
            _logger.Info($"Name совпал с ожидаемым и равен: {getProject.Result.Name}");
    
            Assert.That(getProject.Result.Description, Is.EqualTo(_project.Description));
            _logger.Info($"Description совпал с ожидаемым и равен: {getProject.Result.Description}");
    
            Assert.That(getProject.Result.Id, Is.EqualTo(_project.Id));
            _logger.Info($"Id равен: {getProject.Result.Id}");
        });

        _etag = getProject.Result.Etag;
        
        _logger.Info("GetProjectApiTest выполнен.");
    }
    
    [Test]
    [Order(3)]
    public void UpdateProjectApiTest()
    {
        _logger.Info("UpdateProjectApiTest запущен.");

        string name = $"New Name {DateTime.Now}";

        _project.Name = name;
        _project.Etag = _etag;

        var updateProject = ProjectService!.UpdateProject(_project);
        
        Assert.That(updateProject.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        _project = JsonConvert.DeserializeObject<Project>(updateProject.Result.Content);
        
        Assert.That(_project.Name, Is.EqualTo(name));
        _logger.Info($"Name совпал с ожидаемым и равен: {_project.Name}");
        
        _logger.Info("UpdateProjectApiTest выполнен.");
    }

    [Test]
    [Order(4)]
    public void DeleteProjectApiTest()
    {
        _logger.Info("DeleteProjectApiTest запущен.");

        var deleteProject = ProjectService!.DeleteProject(_project.Id);
        
        Assert.That(deleteProject, Is.EqualTo(HttpStatusCode.OK));
        
        _logger.Info("DeleteProjectApiTest выполнен.");
    }
}