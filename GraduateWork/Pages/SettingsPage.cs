using GraduateWork.Elements;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public class SettingsPage : BasePage
{
    private static string Endpoint = "settings";

    private static readonly By BackToDashboardButtonBy = By.XPath("//a[@data-testid='link-back-to-app']");
    private static readonly By LeftMenuButtonProjectBy = By.XPath("//li[@data-testid='item-projects']");
    private static readonly By LeftMenuButtonАccountBy = By.XPath("//li[@data-testid='item-profile']");

    private static readonly By DeleteButtonBy =
        By.XPath(
            "//div[@data-testid='section-project_edit']/descendant::button[@data-testid='button-more_single:delete']");

    private static readonly By CloseButtonBy =
        By.XPath(
            "//div[@data-testid='section-project_edit']/descendant::button[@data-testid='button-entity-close']");

    private static readonly By СonfirmDeleteButtonBy = By.XPath("//button[@data-testid='button-affirm']");

    private static readonly By ProjectTableBy = By.XPath("//table[@data-testid='table-projects']");
    private static readonly By ProjectTableBodyBy = By.XPath("//table[@data-testid='table-projects']/tbody/tr[1]");
    private static readonly By FilterInputBy = By.XPath("//input[@data-testid='textbox-filter']");
    private static readonly By FilterResultBy = By.XPath("//div[@data-testid='toolbar-table']/following-sibling::div");

    private static readonly By DragImageBy = By.XPath("//div[@data-testid='section-drop-area']/input");
    private static readonly By UploadAvatarButtonBy = By.XPath("//div[@data-testid='section-drop-area']/div/button");

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
    public Button LeftMenuButtonАccount => new Button(_driver, LeftMenuButtonАccountBy);

    public Button DeleteButton => new Button(_driver, DeleteButtonBy);
    public Button CloseButton => new Button(_driver, CloseButtonBy);
    public Button СonfirmDeleteButton => new Button(_driver, СonfirmDeleteButtonBy);

    public Table ProjectTable => new Table(_driver, ProjectTableBy);
    public UIElement ProjectTableBody => new UIElement(_driver, ProjectTableBodyBy);
    public UIElement FilterResult => new UIElement(_driver, FilterResultBy);
    public Input FilterInput => new Input(_driver, FilterInputBy);

    public UIElement DragImage => new UIElement(_driver, DragImageBy);
    public Button UploadAvatarButton => new Button(_driver, UploadAvatarButtonBy);
}