using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarsApp.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsAllDay { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}