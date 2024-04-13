using System.Net;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;

namespace GraduateWork.Tests;

public class TestPlanTests : BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private Project _project;
    private TestPlan _testPlan;

    [OneTimeSetUp]
    public void CreateProject()
    {
        _project = new Project()
        {
            Name = $"Project {DateTime.Now}",
            Description = "Test Project"
        };

        var actualProject = ProjectService!.AddProject(_project);

        Assert.That(actualProject.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        _project = JsonConvert.DeserializeObject<Project>(actualProject.Result.Content);
    }

    [Test]
    [Order(1)]
    public void CreateTestPlaneApiTest()
    {
        _logger.Info("CreateTestPlaneApiTest запущен.");

        _testPlan = new TestPlan()
        {
            Title = $"TestPlan {DateTime.Now}",
            Description = "Test TestPlan",
            ProjectId = _project.Id
        };

        var createTestPlan = TestPlaneServices!.AddTestPlan(_testPlan);

        Assert.Multiple(() =>
        {
            Assert.That(createTestPlan.Result.Title, Is.EqualTo(_testPlan.Title));
            _logger.Info($"Title равен: {createTestPlan.Result.Title}");

            Assert.That(createTestPlan.Result.ProjectId, Is.EqualTo(_testPlan.ProjectId));
            _logger.Info($"ID проекта равен: {createTestPlan.Result.ProjectId}");

            Assert.That(createTestPlan.Result.Id, !Is.EqualTo(null));
            _logger.Info($"Id равен: {createTestPlan.Result.Id}");
        });

        _testPlan = createTestPlan.Result;

        _logger.Info("CreateTestPlaneApiTest выполнен.");
    }

    [Test]
    [Order(2), Category("NFE")]
    public void GetTestPlaneApiTest()
    {
        _logger.Info("GetTestPlaneApiTest запущен.");

        var getTesstPlan = TestPlaneServices!.GetTestPlan(_testPlan.Id);

        Assert.Multiple(() =>
        {
            Assert.That(getTesstPlan.Result.Title, Is.EqualTo(_testPlan.Title));
            _logger.Info($"Title равен: {getTesstPlan.Result.Title}");

            Assert.That(getTesstPlan.Result.Id, !Is.EqualTo(null));
            _logger.Info($"Id равен: {getTesstPlan.Result.Id}");
        });

        _logger.Info("GetTestPlaneApiTest выполнен.");
    }

    [Test]
    [Order(3)]
    public void DeleteTestPlaneApiTest()
    {
        _logger.Info("DeleteTestPlaneApiTest запущен.");

        var deleteTestPlan = TestPlaneServices!.DeleteTestPlan(_testPlan.Id);
        Assert.That(deleteTestPlan, Is.EqualTo(HttpStatusCode.OK));

        _logger.Info("DeleteTestPlaneApiTest выполнен.");
    }

    [Test]
    [Order(4), Category("AFE")]
    public void GetTestPlaneInvalidIdApiTest()
    {
        _logger.Info("GetTestPlaneInvalidIdApiTest запущен.");

        var getTestPlan = TestPlaneServices!.GetTestPlan(-1);

        Assert.That(getTestPlan.Result.Message, Is.EqualTo("The entity with id -1 was not found."));

        _logger.Info("GetTestPlaneInvalidIdApiTest выполнен.");
    }

    [OneTimeTearDown]
    public void DeleteProject()
    {
        Assert.That(ProjectService!.DeleteProject(_project.Id), Is.EqualTo(HttpStatusCode.OK));
    }
}