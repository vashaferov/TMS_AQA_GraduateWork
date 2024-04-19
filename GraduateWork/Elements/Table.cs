using GraduateWork.Helpers;
using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class Table
{
    private UIElement _uiElement;
    private List<string> _columns;
    private List<TableRow> _rows;

    /// <summary>
    /// Локатор данного элемента должен использовать тэг 'table'
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="by"></param>
    public Table(IWebDriver driver, By by)
    {
        _uiElement = new UIElement(driver, by);
        _columns = new List<string>();
        _rows = new List<TableRow>();
        WaitsHelper waitsHelper = new WaitsHelper(driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));

        foreach (var columnElement in _uiElement.FindUIElements(By.TagName("th")))
        {
            _columns.Add(columnElement.Text.ToLower());
        }
        // waitsHelper.WaitForVisibility()
        foreach (var rowElement in _uiElement.FindUIElements(By.XPath("//tr[@class!='table-header-row']")))
        {
            _rows.Add(new TableRow(rowElement));
        }
    }

    public TableCell GetCell(string targetColumn, string uniqueValue, string columnName)
    {
        return GetRow(targetColumn, uniqueValue).GetCell(_columns.IndexOf(columnName));
    }

    public TableCell GetCell(string targetColumn, string uniqueValue, int columnIndex)
    {
        return GetRow(targetColumn, uniqueValue).GetCell(columnIndex);
    }

    public TableRow GetRow(string targetColumn, string uniqueValue)
    {
        foreach (var row in _rows)
        {
            if (row.GetCell(_columns.IndexOf(targetColumn)).Text.Trim().Equals(uniqueValue))
                return row;
        }

        return null;
    }

    public bool Displayed => _uiElement.Displayed;
    public bool Until => _uiElement.Until;
}