using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class ProjectSteps : BaseStep
{
    public ProjectSteps(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Создание нового проекта")]
    public bool CreateProject(string name)
    {
        DashboardPage.ProjectNameInput.SendKeys(name);
        DashboardPage.CreateButton.Click();

        return DashboardPage.CreateButton.Until;
    }

    [AllureStep("Удаление проекта")]
    public bool DeleteProject(string name)
    {
        if (SettingsPage.CloseButton.Displayed)
            SettingsPage.CloseButton.Click();

        SettingsPage.IsPageOpened();
        SettingsPage.ProjectTable
            .GetCell("name", name, "description")
            .Click();

        SettingsPage.DeleteButton.Click();
        SettingsPage.СonfirmDeleteButton.Click();

        SettingsPage.FilterInput.SendKeys(name);
        return SettingsPage.ProjectTable.Until && SettingsPage.FilterResult.Text.Trim().Contains("Filter does not match anything.");
    }
}