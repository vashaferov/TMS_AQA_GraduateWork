using GraduateWork.Clients;
using GraduateWork.Service;
using NLog;
using NUnit.Allure.Core;

namespace GraduateWork.Tests.APITest;

[AllureNUnit]
public class BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    protected ProjectServices? ProjectService;
    protected TestPlaneServices? TestPlaneServices;
    protected AuthenticationServices? AuthenticationServices;

    [OneTimeSetUp]
    public void SetUpApi()
    {
        var restClient = new RestClientExtended();
        ProjectService = new ProjectServices(restClient);
        TestPlaneServices = new TestPlaneServices(restClient);
        AuthenticationServices = new AuthenticationServices(restClient);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        ProjectService.Dispose();
        TestPlaneServices.Dispose();
    }
}