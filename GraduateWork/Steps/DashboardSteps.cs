using GraduateWork.Pages;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class DashboardSteps : BaseStep
{
    public DashboardSteps(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Выполнен переход на страницу \"Settings\"")]
    public SettingsPage NavigateToSettingsPage()
    {
        DashboardPage.ProjectDropdownButtonMain.Click();
        DashboardPage.ProjectDropdownMenu.SelectText("Project settings");
        
        return new SettingsPage(_driver);
    }
    
    [AllureStep("Открыт раздел \"Create a new project\"")]
    public DashboardPage NavigateToCreateNewProject()
    {
        DashboardPage.ProjectDropdownButtonMain.Click();
        DashboardPage.ProjectDropdownMenu.SelectText("Create a new project");
        
        return new DashboardPage(_driver);
    }
}