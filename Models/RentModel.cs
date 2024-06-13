using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class RentModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int RentID { get; set; }
        public string ProductName { get; set; }
        public decimal RentalPrice { get; set; }
        public int SupplierID { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string ProductImageLink { get; set; }
    }
}
