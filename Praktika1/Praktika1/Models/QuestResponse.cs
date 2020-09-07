using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praktika1.Models
{
    public class QuestResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
    }
}