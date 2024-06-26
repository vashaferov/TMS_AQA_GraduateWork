using System.Diagnostics;
using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Dashboard Tests")]
public class DashboardTest : BaseTest
{
    [Test]
    [AllureName("Проверка Popup элемента")]
    [AllureDescription("Тест на проверку всплывающего сообщения")]
    [Category("Regression")]
    public void PopupTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps
            .SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password)
            .LeftMenuButtonDashboard
            .Hover();
        AllureApi.Step("Выполнено наведение на элемент левого меню");
        
        Assert.That(DashboardSteps.DashboardPage.DashbordPopupElement.Text.Trim(), Is.EqualTo("Dashboard"));
        AllureApi.Step("Получено всплывающее сообщение с текстом \"Dashboard\"");
    }
}