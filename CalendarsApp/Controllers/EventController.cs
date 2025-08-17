using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalendarsApp.Context;
using CalendarsApp.Entities;

namespace CalendarsApp.Controllers
{
    public class EventController : Controller
    {
        private readonly CalendarDbContext db = new CalendarDbContext();
        public ActionResult EventList()
        {
            var values = db.Events.ToList();
            return View(values);
        }
        public ActionResult EventDelete(int id)
        {
            var values = db.Events.Find(id);
            db.Events.Remove(values);
            return RedirectToAction("EventList");
        }
        [HttpGet]
        public ActionResult EventUpdate(int id)
        {
            var values = db.Events.Find(id);
            return View("EventUpdate",values);
        }
        [HttpPost]
        public ActionResult EventUpdate(Event @event)
        {
            var values = db.Events.Find(@event.Id);
            values.Title = @event.Title;
            values.Description = @event.Description;
            values.Start = @event.Start;
            values.Start = @event.Start;
            values.End = @event.End;
            values.IsAllDay = @event.IsAllDay;
            values.CategoryId = @event.CategoryId;
            db.SaveChanges();
            return View("EventList", values);
        }
    }
}
