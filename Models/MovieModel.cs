using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class MovieModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public string? Synopsis { get; set; }
        public string? ActorList { get; set; }
        public string? Duration { get; set; }

        public string? MovieLink { get; set; }

        public string? MovieTrailerLink { get; set; }

    }
}
