using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class TableCell
{
    private UIElement _uiElement;
    
    public TableCell(IWebDriver driver, By by)
    {
    }

    public TableCell(UIElement uiElement)
    {
        _uiElement = uiElement;
    }

    public UIElement GetLink() => _uiElement.FindUIElement(By.TagName("a"));
    public string Text => _uiElement.Text;
    public void Click() => _uiElement.Click();
}