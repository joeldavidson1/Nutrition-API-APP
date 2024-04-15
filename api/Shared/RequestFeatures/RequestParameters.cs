namespace Shared.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 50;
    /// <summary>
    /// Specify which page number to go to 
    /// </summary>
    /// <example>7</example>
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    /// <summary>
    /// Specify how many items to display on the page. Max items = 50 
    /// </summary>
    /// <example>25</example>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
}