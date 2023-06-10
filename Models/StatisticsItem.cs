using System;
namespace pis.Models
{
	public class StatisticsItem
	{

        public string? Locality { get; set; }
        public int TotalVaccines { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
    }
    //public StatisticsItem()
    //{

    //}
}


