using GraduateWork.Helpers;
using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class DropdownMenu
{
    private List<UIElement> _uiElements;
    private List<string> _texts;

    public DropdownMenu(IWebDriver driver, By by)
    {
        _uiElements = new List<UIElement>();
        _texts = new List<string>();
        WaitsHelper _waitsHelper = new WaitsHelper(driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        
        foreach (var webElement in _waitsHelper.WaitForAllVisibleElementsLocatedBy(By.XPath("descendant::li")))
        {
            UIElement uiElement = new UIElement(driver, webElement);
            _uiElements.Add(uiElement);
            _texts.Add(uiElement.Text.Trim());
        }
    }

    public void SelectText(string text)
    {
        try
        {
            _uiElements[_texts.IndexOf(text)].Click();
        }
        catch (Exception e)
        {
            throw new AssertionException("По искомому тексту не найден элемент");
        }
    }
    
    public void SelectIndex(int index)
    {
        try
        {
            _uiElements[index].Click();
        }
        catch (Exception e)
        {
            throw new AssertionException("По искомому индексу не найден элемент");
        }
    }
}