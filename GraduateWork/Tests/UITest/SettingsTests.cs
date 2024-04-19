using System.Diagnostics;
using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Settings Tests")]
public class SettingsTests : BaseTest
{
    [Test]
    [AllureName("Обновление аватара профеля")]
    [AllureDescription("Тест на загрузку файла")]
    public void UploadAvatarTest()
    {
        Debug.Assert(Configurator.AppSettings.Username != null && Configurator.AppSettings.Password != null);
        
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);

        DashboardSteps.NavigateToSettingsPage();
        
        Assert.That(SettingsSteps.UploadAvatar("avatar-icon.jpg"));
        AllureApi.Step("Успешно загружен файл");
    }
}