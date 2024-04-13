
using System.Net;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;

namespace GraduateWork.Tests;
[Category("CRUD")]
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

        Project projectNew = new Project()
        {
            Name = $"Test {DateTime.Now}",
            Description = "Test Description",
            Etag = "test"
        };

        var actualProject = ProjectService!.AddProject(projectNew);
        
        Assert.That(actualProject.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        _project = JsonConvert.DeserializeObject<Project>(actualProject.Result.Content);
        
        Assert.Multiple(() =>
        {
            Assert.That(_project.Name, Is.EqualTo(projectNew.Name));
            _logger.Info($"Name равен: {_project.Name}");

            Assert.That(_project.Description, Is.EqualTo(projectNew.Description));
            _logger.Info($"Description равен: {projectNew.Description}");

            Assert.That(_project.Id, !Is.EqualTo(null));
            _logger.Info($"Id равен: {_project.Id}");
        });

        _logger.Info("CreateProjectApiTest выполнен.");
    }
    
    [Test]
    [Order(2), Category("NFE")]
    public void GetProjectApiTest()
    {
        _logger.Info("GetProjectApiTest запущен.");
        
        var getProject = ProjectService!.GetProject(_project.Id);
        
        Assert.Multiple(() =>
        {
            Assert.That(getProject.Result.Name, Is.EqualTo(_project.Name));
            _logger.Info($"Name равен: {getProject.Result.Name}");
    
            Assert.That(getProject.Result.Description, Is.EqualTo(_project.Description));
            _logger.Info($"Description равен: {getProject.Result.Description}");
    
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
        _logger.Info($"Name равен: {_project.Name}");
        
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
    
    [Test]
    [Category("AFE")]
    public void GetProjectInvalidIdApiTest()
    {
        _logger.Info("GetProjectApiTest запущен.");
        
        var getProject = ProjectService!.GetProject(-1);
        
        Assert.That(getProject.Result.Message, Is.EqualTo("The entity with id -1 was not found."));

        _logger.Info("GetProjectApiTest выполнен.");
    }
}