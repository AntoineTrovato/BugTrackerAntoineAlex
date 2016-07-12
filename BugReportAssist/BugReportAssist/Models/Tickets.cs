using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BugTracker.Models;


namespace BugTracker.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Sujet { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Ticket()
        {
            this.ReleaseDate = DateTime.Now;
        }
    }
}