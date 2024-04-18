using GraduateWork.Elements;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public class SettingsPage : BasePage
{
    private static string Endpoint = "settings";

    private static readonly By BackToDashboardButtonBy = By.XPath("//a[@data-testid='link-back-to-app']");

    private static readonly By DeleteButtonBy =
        By.XPath(
            "//div[@data-testid='section-project_edit']/descendant::button[@data-testid='button-more_single:delete']");

    private static readonly By СonfirmDeleteButtonBy = By.XPath("//button[@data-testid='button-affirm']");
    private static readonly By LeftMenuButtonProjectBy = By.XPath("//li[@data-testid='item-projects']");
    private static readonly By ProjectTableBy = By.XPath("//table[@data-testid='table-projects']");
    private static readonly By ProjectTableBodyBy = By.XPath("//table[@data-testid='table-projects']/tbody/tr[1]");
    private static readonly By FilterInputBy = By.XPath("//input[@data-testid='textbox-filter']");
    private static readonly By FilterResultBy = By.XPath("//div[@class='sc-jTQCzO iSbSxx sc-jSUdEz fJAhYQ']");

    public SettingsPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl)
    {
    }

    public SettingsPage(IWebDriver driver) : base(driver)
    {
    }

    public override bool IsPageOpened()
    {
        return ProjectTableBody.Displayed;
    }

    protected override string GetEndpoint()
    {
        return Endpoint;
    }

    public Button BackToDashboardButton => new Button(_driver, BackToDashboardButtonBy);
    public Button LeftMenuButtonProject => new Button(_driver, LeftMenuButtonProjectBy);
    public Button DeleteButton => new Button(_driver, DeleteButtonBy);
    public Button СonfirmDeleteButton => new Button(_driver, СonfirmDeleteButtonBy);
    public Table ProjectTable => new Table(_driver, ProjectTableBy);
    public UIElement ProjectTableBody => new UIElement(_driver, ProjectTableBodyBy);
    public UIElement FilterResult => new UIElement(_driver, FilterResultBy);
    public Input FilterInput => new Input(_driver, FilterInputBy);
}