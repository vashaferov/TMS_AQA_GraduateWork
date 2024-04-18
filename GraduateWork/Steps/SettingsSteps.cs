using System.Reflection;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace GraduateWork.Steps;

public class SettingsSteps : BaseStep
{
    public SettingsSteps(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Обновление аватара")]
    public bool UploadAvatar(string fileName)
    {
        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string filePath = Path.Combine(assemblyPath, "Resources", fileName);

        DashboardPage.ProjectDropdownButtonMain.Click();
        DashboardPage.ProjectDropdownMenu.SelectText("Project settings");

        SettingsPage.LeftMenuButtonАccount.Click();
        SettingsPage.DragImage.SendKeys(filePath);

        return SettingsPage.UploadAvatarButton.Trxt.Trim().Equals("Remove avatar");
    }
}