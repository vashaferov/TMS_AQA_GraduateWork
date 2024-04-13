using System.Net;
using Allure.Net.Commons;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests;

[AllureSuite("API Project Tests")]
public class ProjectTests : BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private Project _project;
    private string _etag;

    [Test]
    [Order(1)]
    public void CreateProjectApiTest()
    {
        AllureApi.Step("CreateProjectApiTest запущен.");

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
            AllureApi.Step($"Name равен: {_project.Name}");

            Assert.That(_project.Description, Is.EqualTo(projectNew.Description));
            AllureApi.Step($"Description равен: {projectNew.Description}");

            Assert.That(_project.Id, !Is.EqualTo(null));
            AllureApi.Step($"Id равен: {_project.Id}");
        });

        AllureApi.Step("CreateProjectApiTest выполнен.");
    }
    
    [Test]
    [Order(2), Category("NFE")]
    public void GetProjectApiTest()
    {
        AllureApi.Step("GetProjectApiTest запущен.");
        
        var getProject = ProjectService!.GetProject(_project.Id);
        
        Assert.Multiple(() =>
        {
            Assert.That(getProject.Result.Name, Is.EqualTo(_project.Name));
            AllureApi.Step($"Name равен: {getProject.Result.Name}");
    
            Assert.That(getProject.Result.Description, Is.EqualTo(_project.Description));
            AllureApi.Step($"Description равен: {getProject.Result.Description}");
    
            Assert.That(getProject.Result.Id, Is.EqualTo(_project.Id));
            AllureApi.Step($"Id равен: {getProject.Result.Id}");
        });

        _etag = getProject.Result.Etag;
        
        AllureApi.Step("GetProjectApiTest выполнен.");
    }
    
    [Test]
    [Order(3)]
    public void UpdateProjectApiTest()
    {
        AllureApi.Step("UpdateProjectApiTest запущен.");

        string name = $"New Name {DateTime.Now}";

        _project.Name = name;
        _project.Etag = _etag;

        var updateProject = ProjectService!.UpdateProject(_project);
        
        Assert.That(updateProject.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        _project = JsonConvert.DeserializeObject<Project>(updateProject.Result.Content);
        
        Assert.That(_project.Name, Is.EqualTo(name));
        AllureApi.Step($"Name равен: {_project.Name}");
        
        AllureApi.Step("UpdateProjectApiTest выполнен.");
    }

    [Test]
    [Order(4)]
    public void DeleteProjectApiTest()
    {
        AllureApi.Step("DeleteProjectApiTest запущен.");

        var deleteProject = ProjectService!.DeleteProject(_project.Id);
        
        Assert.That(deleteProject, Is.EqualTo(HttpStatusCode.OK));
        
        AllureApi.Step("DeleteProjectApiTest выполнен.");
    }
    
    [Test]
    [Category("AFE")]
    public void GetProjectInvalidIdApiTest()
    {
        AllureApi.Step("GetProjectApiTest запущен.");
        
        var getProject = ProjectService!.GetProject(-1);
        
        Assert.That(getProject.Result.Message, Is.EqualTo("The entity with id -1 was not found."));

        AllureApi.Step("GetProjectApiTest выполнен.");
    }
}