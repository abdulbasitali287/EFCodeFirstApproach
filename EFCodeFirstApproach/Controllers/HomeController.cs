using EFCodeFirstApproach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace EFCodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student std)
        {
            if (ModelState.IsValid == true)
            {
                db.Students.Add(std);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertSuccess"] = "Data Inserted Successfully...!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.InsertFail = "Data not Inserted...!";
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.id == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(std).State = EntityState.Modified;
                var a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateSuccess"] = "Data Updated Successfully...!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UpdateFail = "Data not Updated...!";
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var row = db.Students.Where(model => model.id == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(Student std)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(std).State = EntityState.Deleted;
                var a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["DeletedSuccess"] = "Data Deleted Successfully...!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["DeleteFail"] = "Data not Deleted...!";
                }
            }
            return View();
        }
    }
}