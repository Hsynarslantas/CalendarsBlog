using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CalendarsApp.Entities;


namespace CalendarsApp.Context
{
    public class CalendarDbContext:DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}