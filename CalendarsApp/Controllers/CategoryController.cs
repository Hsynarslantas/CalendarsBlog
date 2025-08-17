using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalendarsApp.Context;
using CalendarsApp.Entities;

namespace CalendarsApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CalendarDbContext db = new CalendarDbContext();
        public ActionResult CategoryList()
        {
            var values=db.Categories.ToList();
            return View(values);
        }
        public ActionResult CategoryDelete(int id)
        {
            var values = db.Categories.Find(id);
            db.Categories.Remove(values);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            var values= db.Categories.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult CategoryUpdate(Category category)
        {
            var values=db.Categories.Find(category.Id);
            values.Name = category.Name;
            values.Color = category.Color;
            values.Description = category.Description;
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}