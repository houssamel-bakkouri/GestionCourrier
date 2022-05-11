using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public string Message { get; set; }
    }
}