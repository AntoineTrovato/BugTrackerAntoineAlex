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
        public string Statut { get; set; }
        public string Importance { get; set; }

        public string Application { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public Ticket()
        {
            this.DateModification = DateTime.Now;
        }
    }
}