using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class Checkbox
{
    private UIElement _uiElement;

    /// <summary>
    /// Данный элемент должен использовать атрибут 'use' для локатора
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="by"></param>
    public Checkbox(IWebDriver driver, By by)
    {
        _uiElement = new UIElement(driver, by);
    }

    public Checkbox(IWebDriver driver, IWebElement webElement)
    {
        _uiElement = new UIElement(driver, webElement);
    }

    public void Set(bool flag)
    {
        bool trigger = _uiElement.GetAttribute("href") != "#icon-checkbox-unchecked";
        
        if ((_uiElement.GetAttribute("href") != "#icon-checkbox-unchecked") == flag)
            _uiElement.Click();
    }

    public bool Displayed => _uiElement.Displayed;
}