using GraduateWork.Elements;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public class DashboardPage : BasePage
{
    private static string Endpoint = "dashboard";
    
    private static readonly By ProjectDropdownMenuButtonBy = By.ClassName("button-dropdown");
    private static readonly By ProjectDropdownMenuBy = By.XPath("//*[@id='portal-root']/descendant::ul");
    
    public DashboardPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl)
    {
    }

    public DashboardPage(IWebDriver driver) : base(driver)
    {
    }

    public override bool IsPageOpened()
    {
        return ProjectDropdownButtonMain.Displayed;
    }

    protected override string GetEndpoint()
    {
        return Endpoint;
    }
    
    public Button ProjectDropdownButtonMain => new Button(_driver, ProjectDropdownMenuButtonBy);
    public DropdownMenu ProjectDropdownMenu => new DropdownMenu(_driver, ProjectDropdownMenuBy);
}