using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string? AppUserId { get; set; }
        public int? StockId { get; set; }

        // Navigation Properties are for developers
        public AppUser? AppUser { get; set; }
        public Stock? Stock { get; set; }
    }
}