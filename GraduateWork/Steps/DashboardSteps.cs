using GraduateWork.Helpers;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class DashboardSteps : BaseStep
{
    public DashboardSteps(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Успешное создание нового проекта")]
    public bool CreateProject(string name)
    {
        WaitsHelper waitsHelper = new WaitsHelper(_driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        
        DashboardPage.ProjectDropdownButtonMain.Click();
        DashboardPage.ProjectDropdownMenu.SelectText("Create a new project");
        
        DashboardPage.ProjectNameInput.SendKeys(name);
        DashboardPage.CreateButton.Click();

        return DashboardPage.CreateButton.Until;
    }
}