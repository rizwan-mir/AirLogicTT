using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirLogicTT.Models
{
    public class Artist
    {
        [Required]
        public string ArtistName { get; set; }
        public double Avg { get; set; }
        public int MinWords { get; set; }
        public int MaxWords { get; set; }
        public int Range { get; set; }
        public double Var { get; set; }
        public double StdDev { get; set; }
        public List<Song> Songs { get; set; }
    }
}