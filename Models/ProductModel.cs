using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class ProductModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public System.Decimal? ProductPrice { get; set; }
        public int? ProductRating { get; set; }
        public int? ProductQuantity { get; set; }
        public int? SupplierID { get; set; }
        public string? ProductImageLink { get; set; }

    }
}
