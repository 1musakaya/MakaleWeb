using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_BusinessLayer;
using Makale_Entities;


namespace Makale_Web.Controllers
{/// <summary>
///  bu xcontrolleri en alt seçenekteki controller iseçerek oluşturduk dolu geldi
/// </summary>
    public class KategoriController : Controller
    {
        KategoriYonet ky=new KategoriYonet();

        // GET: Kategori
        public ActionResult Index()
        {
            return View(ky.Listele());
        }

        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori =ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Kategori kategori)
        {
            if (ModelState.IsValid)
            {
               ky.KategoriEkle(kategori);   // bu metohodu burada oluşturduk
            
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(kategori).State = EntityState.Modified;
                //db.SaveChanges();  bunlar yerine alttaki
                ky.KategoriUpdate(kategori);
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori =ky.KategoriBul(id);
            //db.Kategoris.Remove(kategori);
            //db.SaveChanges();
            ky.KategoriSil(kategori);
            return RedirectToAction("Index");
        }

  
    }
}
