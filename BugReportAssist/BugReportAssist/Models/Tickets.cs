using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace BugTracker.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string objet { get; set; }
        public string desc { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class TicketDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
}