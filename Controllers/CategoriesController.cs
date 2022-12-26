using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD.TASK.MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace CRUD.TASK.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private ProductContext db = new ProductContext();

      [HttpGet]
        public ActionResult Index(string searchBy, string search, int? i)
        {
            if (searchBy == "CategoryName")
            {
                var data = db.Categories.Where(model => model.CategoryName.StartsWith(search)).ToList().ToPagedList(i ?? 1, 10);
                return View(data);
            }

            else
            {

                var data = db.Categories.ToList().ToPagedList(i ?? 1, 10);
                return View(data);
            }
        }
      

       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
              int a =  db.SaveChanges();
                if (a > 0)
                {

                    TempData["InsertMessage"] = "<script>alert('Data Inserted !!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["InsertMessage"] = "<script>alert('Data Not Inserted !!')</script>";
                }
                return RedirectToAction("Index");
            }

            return View(category);
        }

       [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {

                    TempData["UpdateMessage"] = "<script>alert('Data Updated !!')</script>";

                    return RedirectToAction("Index");


                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert('Data Not Updated !!')</script>";
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
          int a =  db.SaveChanges();
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Data Deleted !!')</script>";
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Data Not Deleted !!')</script>";
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
    }
}
