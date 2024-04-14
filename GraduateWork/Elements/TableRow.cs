using OpenQA.Selenium;

namespace GraduateWork.Elements;

public class TableRow
{
    private UIElement _uiElement;
    private List<TableCell> _cells;
    
    public TableRow(IWebDriver driver, By by)
    {
    }
    public TableRow(UIElement uiElement)
    {
        _uiElement = uiElement;
        _cells = new List<TableCell>();
        
        foreach (var cellElement in _uiElement.FindUIElements(By.TagName("td")))
        {
            _cells.Add(new TableCell(cellElement));
        }
    }

    public TableCell GetCell(int  columnIndex) => _cells[columnIndex];
}