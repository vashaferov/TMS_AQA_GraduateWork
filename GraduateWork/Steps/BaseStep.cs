using GraduateWork.Pages;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class BaseStep
{
    protected IWebDriver _driver;

    public LoginPage LoginPage => new LoginPage(_driver);
    public DashboardPage DashboardPage => new DashboardPage(_driver);

    public BaseStep(IWebDriver driver)
    {
        _driver = driver;
    }
}