using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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