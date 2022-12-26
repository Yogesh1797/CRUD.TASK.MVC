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
    public class ProductsController : Controller
    {
        private ProductContext db = new ProductContext();

        [HttpGet]
        public ActionResult Index(string searchBy, string search,int ? i)
        {
            if(searchBy == "ProductName")
            {
                var data = db.Products.Where(model => model.ProductName.StartsWith(search)).ToList().ToPagedList(i ?? 1, 10);
                return View(data);
            }
          
            else
            {
       
                var data = db.Products.ToList().ToPagedList(i ?? 1, 10);
                return View(data);
            }
           
            
        }
       

      [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,CategoryId")] Product product)
        {
            if (ModelState.IsValid == true)
            {
                db.Products.Add(product);
              int a=  db.SaveChanges();
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

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

      
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,CategoryId")] Product product)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(product).State = EntityState.Modified;
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

       [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
           int a= db.SaveChanges();
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
