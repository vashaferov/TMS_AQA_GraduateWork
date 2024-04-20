using Allure.Net.Commons;
using GraduateWork.Clients;
using GraduateWork.Core;
using GraduateWork.Helpers;
using GraduateWork.Steps;
using NUnit.Allure.Core;
using OpenQA.Selenium;

namespace GraduateWork.Tests.UITest;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
public class BaseTest
{
    protected IWebDriver Driver { get; private set; }
    protected WaitsHelper WaitsHelper { get; private set; }
    protected CreatTD CreatTD { get; private set; }

    protected LoginSteps LoginSteps;
    protected ProjectSteps ProjectSteps;
    protected SettingsSteps SettingsSteps;
    protected DashboardSteps DashboardSteps;

    [SetUp]
    public void Setup()
    {
        var restClient = new RestClientExtended();
        Driver = new Browser().Driver;
        WaitsHelper = new WaitsHelper(Driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        CreatTD = new CreatTD(restClient);

        LoginSteps = new LoginSteps(Driver);
        ProjectSteps = new ProjectSteps(Driver);
        SettingsSteps = new SettingsSteps(Driver);
        DashboardSteps = new DashboardSteps(Driver);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                byte[] screenshotBytes = screenshot.AsByteArray;

                AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png", screenshotBytes);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Driver.Quit();
    }
}