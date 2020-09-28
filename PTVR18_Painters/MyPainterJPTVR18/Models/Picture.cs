using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPainterJPTVR18.Models
{
    public class Picture
    {
        public int Id { get; set; }
        [Display(Name = "Название картины")]
        public string Title { get; set; }
        [Display(Name = "Год")]
        public int Year { get; set; }
        [Display(Name = "Музей")]
        public string Museum { get; set; }
        [Display(Name = "Картина")]
        public byte[] Canvas { get; set; }
        public string CanvasType { get; set; }

        [Display(Name = "Художник")]
        public int PainterId { get; set; }

        public Painter painter { get; set; }

    }
}