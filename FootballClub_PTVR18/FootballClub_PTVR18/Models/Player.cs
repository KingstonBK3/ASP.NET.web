using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FootballClub_PTVR18.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Display(Name = "Player name")]
        public string Name { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }//Team id
    }
}