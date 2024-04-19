using System.Collections.ObjectModel;
using System.Drawing;
using GraduateWork.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GraduateWork.Elements;

public class UIElement : IWebElement
{
    private IWebDriver _webDriver;
    private WaitsHelper _waitsHelper;
    private IWebElement _webElement;
    private Actions _actions;

    private UIElement(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _waitsHelper = new WaitsHelper(webDriver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        _actions = new Actions(webDriver);
    }

    public UIElement(IWebDriver webDriver, By by) : this(webDriver)
    {
        _webElement = _waitsHelper.WaitForExists(by);
    }

    public UIElement(IWebDriver webDriver, IWebElement webElement) : this(webDriver)
    {
        _webElement = webElement;
    }

    public IWebElement FindElement(By by)
    {
        return _webElement.FindElement(by);
    }

    public UIElement FindUIElement(By by)
    {
        return new UIElement(_webDriver, FindElement(by));
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        return _webElement.FindElements(by);
    }

    public List<UIElement> FindUIElements(By by)
    {
        var result = new List<UIElement>();

        foreach (var webElement in FindElements(by))
        {
            result.Add(new UIElement(_webDriver, webElement));
        }

        return result;
    }

    public void Clear()
    {
        _webElement.Clear();
    }

    public void SendKeys(string text)
    {
        _webElement.SendKeys(text);
    }

    public void Submit()
    {
        _webElement.Submit();
    }

    public void Click()
    {
        try
        {
            _webElement.Click();
        }
        catch (ElementNotInteractableException)
        {
            try
            {
                MoveToElement();
                ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].click();", _webElement);
            }
            catch (Exception)
            {
                _actions
                    .MoveToElement(_webElement)
                    .Click()
                    .Build()
                    .Perform();
            }
        }
    }

    public string GetAttribute(string attributeName)
    {
        return _webElement.GetAttribute(attributeName);
    }

    public string GetDomAttribute(string attributeName)
    {
        return _webElement.GetDomAttribute(attributeName);
    }

    public string GetDomProperty(string propertyName)
    {
        return _webElement.GetDomAttribute(propertyName);
    }

    public string GetCssValue(string propertyName)
    {
        return _webElement.GetCssValue(propertyName);
    }

    public ISearchContext GetShadowRoot()
    {
        return _webElement.GetShadowRoot();
    }

    public void MoveToElement()
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", _webElement);
    }

    public void Hover()
    {
        _actions.MoveToElement(_webElement).Build().Perform();
    }

    public string TagName => _webElement.TagName;

    public string Text => _webElement.Text.Equals("") ? GetAttribute("innerText") : _webElement.Text;

    public bool Enabled => _webElement.Enabled;
    public bool Selected => _webElement.Selected;
    public Point Location => _webElement.Location;
    public Size Size => _webElement.Size;
    public bool Displayed => _webElement.Displayed;

    public bool Until => _waitsHelper.WaitForElementInvisible(_webElement);
}