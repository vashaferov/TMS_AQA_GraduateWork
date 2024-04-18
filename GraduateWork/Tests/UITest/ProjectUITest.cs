using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Project Tests")]
public class ProjectUITest : BaseTest
{
    string projectName = $"Project 1";
    // string projectName = $"Project {DateTime.Now}";
    
    [Test]
    [Order(1)]
    [AllureName("Создание новго проекта")]
    [AllureDescription("Тест на создание сущности")]
    public void CreateProjectTest()
    {
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        Assert.That(ProjectSteps.CreateProject(projectName));

        Assert.That(LoginSteps.DashboardPage.DashbordProjectName.Text, Is.EqualTo(projectName));
        AllureApi.Step($"Проект \"{projectName}\" успешно создан");
    }

    [Test]
    [Order(2)]
    [AllureName("Удаление проекта")]
    [AllureDescription("Тест на удаление сущности\nТест на отображения диалогового окна")]
    public void DeletProjectTest()
    {
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        Assert.That(ProjectSteps.DeleteProject(projectName));
        AllureApi.Step($"Проект \"{projectName}\" отсутствует в списке");
    }
}