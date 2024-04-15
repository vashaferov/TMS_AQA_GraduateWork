using GraduateWork.Helpers;
using OpenQA.Selenium;

namespace GraduateWork.Pages;

public abstract class BasePage
{
    protected IWebDriver _driver;
    protected WaitsHelper _waitsHelper;

    public BasePage(IWebDriver driver, bool openPageByUrl)
    {
        _driver = driver;
        _waitsHelper = new WaitsHelper(_driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));

        if (openPageByUrl)
            OpenPageByUrl();
    }

    public BasePage(IWebDriver driver) : this(driver, false)
    {
    }

    public abstract bool IsPageOpened();
    protected abstract string GetEndpoint();

    private void OpenPageByUrl()
    {
        _driver.Navigate().GoToUrl(Configurator.AppSettings.URL + GetEndpoint());
    }
}