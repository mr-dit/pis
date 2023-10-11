namespace pis_web_api.Models;

public class ReportItem
{
    public int LocalityId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalCost { get; set; }
}