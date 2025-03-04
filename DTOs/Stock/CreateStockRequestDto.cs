using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Stock
{
    public class CreateStockRequestDto
    {

        [Required]
        [MinLength(1, ErrorMessage = "Stock symbol must be atleast 1 character")]
        [MaxLength(5, ErrorMessage = "Stock symbol exceeds 5 character length")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Company name must exceed 2 characters")]
        [MaxLength(35, ErrorMessage = "Company exceeds 35 character length")]
        public string CompanyName { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 1000000000)]
        [Required]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Dividend not in range")]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Industry type must be 3 or more characters")]
        [MaxLength(80, ErrorMessage = "Industry exceeds 80 character length")]
        public string Industry { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, long.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [Required]
        public long MarketCap { get; set; }
    }
}