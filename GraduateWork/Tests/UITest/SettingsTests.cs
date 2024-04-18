using Allure.Net.Commons;
using GraduateWork.Helpers;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests.UITest;

[AllureSuite("UI Settings Tests")]
public class SettingsTests : BaseTest
{
    [Test]
    [Order(1)]
    [AllureName("Обновление аватара профеля")]
    [AllureDescription("Тест на загрузку файла")]
    public void UploadAvatarTest()
    {
        LoginSteps.NavigateToLoginPage();
        LoginSteps.SuccessfulLogin(Configurator.AppSettings.Username, Configurator.AppSettings.Password);
        
        Assert.That(SettingsSteps.UploadAvatar("avatar-icon.jpg"));
        AllureApi.Step("Успешно загружен файл");
    }
}