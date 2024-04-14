using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class Input
{
    private readonly UIElement _uiElement;
        
    public Input(IWebDriver? driver, By by)
    {
        _uiElement = new UIElement(driver, by);
    }

    public void Click() => _uiElement.Click();
        
    public void SendKeys(string text) => _uiElement.SendKeys(text);
        
    public string Text => _uiElement.Text;
        
    public bool Displayed => _uiElement.Displayed;
}