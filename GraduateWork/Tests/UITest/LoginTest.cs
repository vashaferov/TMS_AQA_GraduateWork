using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Login Tests")]
public class LoginTest : BaseTest
{
    [Test]
    [AllureName("Вход в систему с передачей верных параметров")]
    public void CorrectLoginTest()
    {
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);
        
        Assert.That(LoginSteps.DashboardPage.IsPageOpened);
        
        AllureApi.Step($"Вход выполнен успешно.");
    }

    [Test]
    [AllureName("Вход в систему с передачей неверных параметров")]
    [AllureDescription("Тест на использование некорректных данных")]
    public void IncorrectLoginTest()
    {
        string errorMessage = "Either your email address or your password is wrong. Please try again or";

        LoginSteps.NavigateToLoginPage();
        LoginSteps.IncorrectLogin(Configurator.AppSettings.Username, "1");
        
        Assert.That(LoginSteps.GetErrorText(), Is.EqualTo(errorMessage));
        
        AllureApi.Step($"Получена нужная ошибка:\n{errorMessage}");
    }
}