using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class GenreModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int GenreID { get; set; }
        public string GenreTitle { get; set; }


    }
}
