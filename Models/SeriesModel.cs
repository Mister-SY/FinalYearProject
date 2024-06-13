using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class SeriesModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int SeriesID { get; set; }
        public string SeriesTitle { get; set; }
        public string Genre { get; set; }
        public string? Synopsis { get; set; }
        public string? ActorList { get; set; }
        public string? EpisodeList { get; set; }

        public string? SeriesLink { get; set; }

        public string? SeriesTrailerLink { get; set; }

    }
}
