using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class MovieGenreModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int MovieGenreID { get; set; }
        public int MovieID { get; set; }

        public int GenreID { get; set; }

    }
}
