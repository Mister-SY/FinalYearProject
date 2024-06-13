using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class SeriesGenreModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int SeriesGenreID { get; set; }
        public int SeriesID { get; set; }

        public int GenreID { get; set; }

    }
}
