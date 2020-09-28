using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPainterJPTVR18.Models
{
    public class Painter
    {
        public int Id { get; set; }
        [Display(Name = "Фамилия и имя художника")]
        public string Name { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Биография")]
        public string Biography { get; set; }
        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }

        public ICollection<Picture> pictures { get; set; }

        //Создаем конструктор класса 
        public Painter()
        {
            this.pictures = new List<Picture>();
        }
    }
}