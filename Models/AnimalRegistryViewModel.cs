namespace pis.Models;

public class AnimalRegistryViewModel
{
    public string FilterName { get; set; }
    public string SortBy { get; set; }
    public bool IsAscending { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}