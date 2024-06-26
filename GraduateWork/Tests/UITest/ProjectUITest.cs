using System.Diagnostics;
using Allure.Net.Commons;
using GraduateWork.Helpers;
using GraduateWork.Models;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Project Tests")]
public class ProjectUITest : BaseTest
{
    [Test]
    [Order(1)]
    [AllureName("Создание новго проекта")]
    [AllureDescription("Тест на создание сущности")]
    [Category("Smoke")]
    [Category("Regression")]
    public void CreateProjectTest()
    {
        string projectName = $"Test {DateTime.Now}";
        
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        DashboardSteps.NavigateToCreateNewProject();

        Assert.That(ProjectSteps.CreateProject(projectName));

        Assert.That(LoginSteps.DashboardPage.DashbordProjectName.Text, Is.EqualTo(projectName));
        AllureApi.Step($"Проект \"{projectName}\" успешно создан");
    }

    [Test]
    [Order(2)]
    [AllureName("Удаление проекта")]
    [AllureDescription("Тест на удаление сущности\nТест на отображения диалогового окна")]
    [Category("Smoke")]
    [Category("Regression")]
    public void DeletProjectTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        var jsonTD = JsonConvert.DeserializeObject<Project>(File.ReadAllText(CreatTD.CreatProject()));
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        DashboardSteps.NavigateToSettingsPage();

        Assert.That(ProjectSteps.DeleteProject(jsonTD.Name));
        AllureApi.Step($"Проект \"{jsonTD.Name}\" отсутствует в списке");
    }

    [Test]
    [Order(3)]
    [AllureName("Граничные значения в имени проекта")]
    [AllureDescription("Тест на проверку поля для ввода на граничные значения")]
    [Category("Regression")]
    public void LimitValuesTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        DashboardSteps
            .NavigateToCreateNewProject()
            .ClearAndSend("aa");
        
        Assert.Multiple(() =>
        {
            Assert.That(DashboardSteps.DashboardPage.LimitValuesErrorMessage.Text.Trim(),
                Is.EqualTo("Must have at least 3 characters (leading/trailing white spaces not counted)."));
            Assert.That(DashboardSteps.DashboardPage.CreateButton.GetAttribute("disabled") != null);
        });
        AllureApi.Step("При вводе 2 знаков ожидаемо получена ошибка\nКнопка \"Create\" неактивна");

        DashboardSteps.DashboardPage.ClearAndSend("aaa");
        
        Assert.Multiple(() =>
        {
            Assert.That(DashboardSteps.DashboardPage.LimitValuesErrorMessage.Until);
            Assert.That(DashboardSteps.DashboardPage.CreateButton.GetAttribute("disabled") == null);
        });
        AllureApi.Step("При вводе 3 знаков ожидаемо не получена ошибка\nКнопка \"Create\" активна");
        
        DashboardSteps.DashboardPage.ClearAndSend("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        
        Assert.Multiple(() =>
        {
            Assert.That(DashboardSteps.DashboardPage.LimitValuesErrorMessage.Text.Trim().Contains("Must not have more than 40 characters."));
            Assert.That(DashboardSteps.DashboardPage.CreateButton.GetAttribute("disabled") != null);
        });
        AllureApi.Step("При вводе 41 знаков ожидаемо получена ошибка\nКнопка \"Create\" неактивна");
        
        DashboardSteps.DashboardPage.ClearAndSend("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        
        Assert.Multiple(() =>
        {
            Assert.That(DashboardSteps.DashboardPage.LimitValuesErrorMessage.Until);
            Assert.That(DashboardSteps.DashboardPage.CreateButton.GetAttribute("disabled") == null);
        });
        AllureApi.Step("При вводе 39 знаков ожидаемо не получена ошибка\nКнопка \"Create\" активна");
    }
    
    [Test]
    [Order(4)]
    [AllureName("Недопустимые данные в ключе проекта")]
    [AllureDescription("Тест на ввод данных превышающих допустимые")]
    [Category("Regression")]
    public void InvalidDataTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        DashboardSteps
            .NavigateToCreateNewProject()
            .ProjectKeyInput.SendKeys("тест");
        
        Assert.Multiple(() =>
        {
            Assert.That(DashboardSteps.DashboardPage.LimitValuesErrorMessage.Text.Trim(),
                Is.EqualTo("The input value is not valid (must contain only letters and numbers)."));
            Assert.That(DashboardSteps.DashboardPage.CreateButton.GetAttribute("disabled") != null);
        });
        AllureApi.Step("При вводе кириллицы ожидаемо получена ошибка\nКнопка \"Create\" неактивна");
    }

    [TearDown]
    public void TearDown()
    {
        CreatTD.ClearTD();
    }
}