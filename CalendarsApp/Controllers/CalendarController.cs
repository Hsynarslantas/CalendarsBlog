using System;
using System.Linq;
using System.Web.Mvc;
using CalendarsApp.Context;
using CalendarsApp.Entities;

namespace CalendarsApp.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarDbContext db = new CalendarDbContext();

        public ActionResult Index()
        {
            var events = db.Events.Include("Category").ToList();
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.CategoryColors = categories.ToDictionary(c => c.Id.ToString(), c => c.Color);
            return View(events);
        }

        public JsonResult GetEvents()
        {
            var eventList = db.Events.Include("Category").ToList(); // Veritabanından çekerken bitir

            var events = eventList.Select(e => new
            {
                id = e.Id,
                title = e.Title,
                start = e.Start?.ToString("s"),
                end = e.End?.ToString("s"),
                allDay = e.IsAllDay,
                backgroundColor = e.Category != null ? e.Category.Color : "#3788d8",
                borderColor = e.Category != null ? e.Category.Color : "#3788d8"
            }).ToList();

            return Json(events, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddEvent(string title, DateTime? start, DateTime? end, int? categoryId, string description = "", bool isAllDay = false)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Json(new { success = false, message = "Başlık boş olamaz." });

            if (categoryId == null)
                return Json(new { success = false, message = "Kategori seçimi zorunludur." });

            var category = db.Categories.Find(categoryId);
            if (category == null)
                return Json(new { success = false, message = "Kategori bulunamadı." });

            var newEvent = new Event
            {
                Title = title,
                Description = description,
                Start = start ?? DateTime.Now,
                End = end ?? start?.AddHours(6) ?? DateTime.Now.AddHours(1),
                IsAllDay = isAllDay,
                CategoryId = categoryId.Value
            };

            db.Events.Add(newEvent);
            db.SaveChanges();

            return Json(new { success = true, eventId = newEvent.Id });
        }

        [HttpPost]
        public JsonResult UpdateEventDate(string id, DateTime start, DateTime? end)
        {
            if (!int.TryParse(id, out int eventId))
            {
                return Json(new { success = false, message = "Geçersiz etkinlik ID'si." });
            }

            var eventToUpdate = db.Events.Find(eventId);
            if (eventToUpdate == null)
            {
                return Json(new { success = false, message = "Etkinlik bulunamadı." });
            }

            eventToUpdate.Start = start;
            eventToUpdate.End = end ?? start;
            db.SaveChanges();

            return Json(new { success = true });
        }

    }
}
