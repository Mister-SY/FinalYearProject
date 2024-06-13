using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class RentedModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int RentedID { get; set; }
        public int UserID { get; set; }
        public int RentID { get; set; }
        public DateTime RentalDate { get; set; }
        public Boolean Status { get; set; }
    }
}
