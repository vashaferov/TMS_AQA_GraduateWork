using System.Collections.ObjectModel;
using GraduateWork.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace GraduateWork.Helpers;

public class WaitsHelper(IWebDriver driver, TimeSpan timeout)
{
    private readonly WebDriverWait _wait = new(driver, timeout);

    public ReadOnlyCollection<IWebElement> WaitForAllVisibleElementsLocatedBy(By locator)
    {
        return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
    }

    public ReadOnlyCollection<IWebElement> WaitForPresenceOfAllElementsLocatedBy(By locator)
    {
        return _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
    }

    public IWebElement WaitForExists(By locator)
    {
        return _wait.Until(ExpectedConditions.ElementExists(locator));
    }

    public bool WaitForElementInvisible(IWebElement webElement)
    {
        try
        {
            return _wait.Until(d => !webElement.Displayed);
        }
        catch (NoSuchElementException)
        {
            return true;
        }
        catch (StaleElementReferenceException)
        {
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            throw new WebDriverTimeoutException("Элемент не стал невидимым в течение заданного времени");
        }
    }

    public IWebElement FluentWaitForElement(By locator)
    {
        WebDriverWait fluentWait = new WebDriverWait(driver, TimeSpan.FromSeconds(12))
        {
            PollingInterval = TimeSpan.FromMilliseconds(50)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        return fluentWait.Until(_ => driver.FindElement(locator));
    }
}