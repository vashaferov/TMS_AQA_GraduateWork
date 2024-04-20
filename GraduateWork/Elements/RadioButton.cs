using GraduateWork.Helpers;
using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class RadioButton
{
    private List<UIElement> _uiElements;
    private List<string> _values;
    private List<string> _texts;
    
    /// <summary>
    /// Данный элемент должен использовать атрибут 'name' для локатора
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="by"></param>
    public RadioButton(IWebDriver driver, By by)
    {
        _uiElements = new List<UIElement>();
        _values = new List<string>();
        _texts = new List<string>();
        
        WaitsHelper _waitsHelper = new WaitsHelper(driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        foreach (var webElement in _waitsHelper.WaitForAllVisibleElementsLocatedBy(by))
        {
            UIElement uiElement = new UIElement(driver, webElement);
            _uiElements.Add(uiElement);
            _values.Add(uiElement.GetAttribute("value"));
            _texts.Add(uiElement.FindUIElement(By.XPath("parent::*/strong")).Text.Trim());
        }
    }

    public void SelectByIndex(int index)
    {
        try
        {
            _uiElements[index].Click();
        }
        catch (Exception e)
        {
            throw new AssertionException("Привышен индекс");
        }
    }
    
    public void SelectByValue(string value) => _uiElements[_values.IndexOf(value)].Click();
    public void SelectByText(string text) => _uiElements[_texts.IndexOf(text)].Click();
    public List<string> GetOptions() => _texts;
}