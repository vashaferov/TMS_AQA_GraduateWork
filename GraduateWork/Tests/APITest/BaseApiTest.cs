using GraduateWork.Clients;
using GraduateWork.Service;
using NLog;

namespace GraduateWork.Tests;

public class BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    protected ProjectServices? ProjectService;
    protected TestPlaneServices? TestPlaneServices;

    [OneTimeSetUp]
    public void SetUpApi()
    {
        var restClient = new RestClientExtended();
        ProjectService = new ProjectServices(restClient);
        TestPlaneServices = new TestPlaneServices(restClient);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        ProjectService.Dispose();
        TestPlaneServices.Dispose();
    }
}