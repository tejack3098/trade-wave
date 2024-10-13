using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max length is 10 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000_000_000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000_000_000_000)]
        public long MarketCap { get; set; }
    }
}