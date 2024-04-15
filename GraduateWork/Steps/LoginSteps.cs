using GraduateWork.Pages;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class LoginSteps : BaseStep
{
    public LoginSteps(IWebDriver driver) : base(driver)
    {
    }

    public LoginPage NavigateToLoginPage()
    {
        return new LoginPage(_driver, true);
    }
    
    [AllureStep("Успешный вход в систему.")]
    public DashboardPage SuccessfulLogin(string username, string psw)
    {
        Login(username, psw);
        return DashboardPage;
    }
    [AllureStep("Неудачный вход в систему.")]
    public LoginPage IncorrectLogin(string username, string psw)
    {
        Login(username, psw);
        return LoginPage;
    }
    
    private void Login(string username, string psw)
    {
        LoginPage.EmailInput.SendKeys(username);
        LoginPage.PasswordInput.SendKeys(psw);
        LoginPage.LogInButton.Click();
    }

    public string GetErrorText()
    {
        return LoginPage.ErrorMessage.Text.Trim();
    }
}