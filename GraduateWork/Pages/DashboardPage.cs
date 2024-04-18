using GraduateWork.Elements;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public class DashboardPage : BasePage
{
    private static string Endpoint = "dashboard";
    
    private static readonly By ProjectDropdownMenuButtonBy = By.ClassName("button-dropdown");
    private static readonly By ProjectDropdownMenuBy = By.XPath("//*[@id='portal-root']/descendant::ul");
    private static readonly By ProjectNameInputBy = By.XPath("//*[@data-testid='textbox-name']");
    private static readonly By ProjectKeyInputBy = By.Id(":ra:");
    private static readonly By ProjectDescriptionInputBy = By.Id(":rc:");
    private static readonly By CreateButtonBy = By.XPath("//*[@data-testid='button-save-entity']");
    private static readonly By CloseButtonBy = By.XPath("//*[@data-testid='button-close-entity']");
    private static readonly By DashbordProjectNameBy = By.XPath("//*[@data-testid='button-projects']/child::div/span");
    private static readonly By LeftMenuButtonDashboardBy = By.XPath("//li[@data-testid='item-dashboard']");
    private static readonly By DashbordPopupElementBy = By.XPath("//*[@data-testid='popup-tooltip']");
    
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
    public Input ProjectNameInput => new Input(_driver, ProjectNameInputBy);
    public Input ProjectKeyInput => new Input(_driver, ProjectKeyInputBy);
    public Input ProjectDescriptionInput => new Input(_driver, ProjectDescriptionInputBy);
    public Button CreateButton => new Button(_driver, CreateButtonBy);
    public Button CloseButton => new Button(_driver, CloseButtonBy);
    public UIElement DashbordProjectName => new UIElement(_driver, DashbordProjectNameBy);
    public UIElement LeftMenuButtonDashboard => new UIElement(_driver, LeftMenuButtonDashboardBy);
    public UIElement DashbordPopupElement => new UIElement(_driver, DashbordPopupElementBy);

}