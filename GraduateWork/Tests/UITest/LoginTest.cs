using System.Diagnostics;
using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Login Tests")]
public class LoginTest : BaseTest
{
    [Test]
    [AllureName("Вход в систему с передачей верных параметров")]
    [Category("Smoke")]
    public void CorrectLoginTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);
        
        Assert.That(LoginSteps.DashboardPage.IsPageOpened);
        
        AllureApi.Step($"Вход выполнен успешно.");
    }

    [Test]
    [AllureName("Вход в систему с передачей неверных параметров")]
    [AllureDescription("Тест на использование некорректных данных")]
    [Category("Regression")]
    public void IncorrectLoginTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null);
        
        string errorMessage = "Either your email address or your password is wrong. Please try again or";

        LoginSteps.NavigateToLoginPage();
        LoginSteps.IncorrectLogin(Configurator.AppSettings.Username, "1");
        
        Assert.That(LoginSteps.GetErrorText().Contains(errorMessage));
        
        AllureApi.Step($"Получена нужная ошибка:\n{errorMessage}");
    }
}