using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Project Tests")]
public class ProjectUITest : BaseTest
{
    [Test]
    [AllureName("Создание новго проекта")]
    [AllureDescription("Тест на создание сущности")]
    public void CreateProjectTest()
    {
        string projectName = $"Project {DateTime.Now}";
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        Assert.That(DashboardSteps.CreateProject(projectName));
        
        Assert.That(LoginSteps.DashboardPage.DashbordProjectName.Text, Is.EqualTo(projectName));
        AllureApi.Step($"Проект \"{projectName}\" успешно создан");
    }
}