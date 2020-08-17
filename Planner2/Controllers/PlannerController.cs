using Planner2.Entity;
using Planner2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planner2.Controllers
{
    public class PlannerController : Controller
    {
        private readonly EFContext _context;

        public PlannerController()
        {
            _context = new EFContext();
        }

        public ActionResult ListEvents()
        {
            List<EventViewModel> data = _context.Events.Select(t => new EventViewModel { 
                 Id = t.Id,
                 Date = t.Date,
                 Description = t.Description,
                 Image = t.Image,
                 Title = t.Title
            }).ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(new Event
                {
                    Date = model.Date,
                    Description = model.Description,
                    Image = model.Image,
                    Title = model.Title
                });
                _context.SaveChanges();
                return RedirectToAction("ListEvents", "Planner");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            _context.Events.Remove(_context.Events.FirstOrDefault(t => t.Id == id));
            _context.SaveChanges();

            return RedirectToAction("ListEvents", "Planner");
        }

    }
}