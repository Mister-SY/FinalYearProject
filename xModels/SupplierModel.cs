using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class SupplierModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string SupplierEmail { get; set; }
        public string Telephone { get; set; }
    }
}
