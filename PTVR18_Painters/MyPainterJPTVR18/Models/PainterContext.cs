using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPainterJPTVR18.Models
{
    public class PainterContext:DbContext
    {
        public DbSet<Painter> Painters { get; set; }
        public DbSet <Picture> Pictures { get; set; }
    }
}