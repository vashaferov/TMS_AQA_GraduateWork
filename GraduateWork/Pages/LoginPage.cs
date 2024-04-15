using GraduateWork.Elements;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public class LoginPage : BasePage
{
    private static string Endpoint = "";

    private static readonly By EmailInputBy = By.Id(":r0:");
    private static readonly By PasswordInputBy = By.Id(":r2:");
    private static readonly By StayLoggedInCheckBoxBy = By.XPath("//*[@class='sc-bbxCgr gvkwA']/following-sibling::*[@role='img']");
    private static readonly By ErrorMessageBy = By.XPath("//*[@class='sc-hiTDLB cAUxfP']/p[1]/span[1]");
    private static readonly By LogInButtonBy = By.ClassName("button-main");
    
    public LoginPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl)
    {
    }

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public override bool IsPageOpened()
    {
        try
        {
            return LogInButton.Displayed;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected override string GetEndpoint()
    {
        return Endpoint;
    }

    public Input EmailInput => new Input(_driver, EmailInputBy);
    public Input PasswordInput => new Input(_driver, PasswordInputBy);
    public Checkbox StayLoggedInCheckBox => new Checkbox(_driver, StayLoggedInCheckBoxBy);
    public UIElement ErrorMessage => new UIElement(_driver, ErrorMessageBy);
    public Button LogInButton => new Button(_driver, LogInButtonBy);
}