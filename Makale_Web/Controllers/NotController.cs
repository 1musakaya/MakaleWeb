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
{
	public class NotController : Controller
	{
		NotYonet ny = new NotYonet();

		// GET: Not
		public ActionResult Index()
		{
			var nots = ny.ListeleQueryable().Include(n => n.Kategori);

			if (Session["login"] != null)
			{
				Kullanici kullanici = (Kullanici)Session["login"];
				nots = ny.ListeleQueryable().Include(n => n.Kategori).Where(x => x.Kullanici.Id == kullanici.Id);
			}

			//Kullanici kullanici=(Kullanici)Session["login"];

			//var nots = ny.ListeleQueryable().Include(n => n.Kategori).Where(x=>x.Kullanici.Id==kullanici.Id);  //queryable işleminde kategoriye join ekledik ; include join yapıyor; select*from Notlar inner join kategori;
			//	// telefona bak 11:44 iki kod var aynı anlama geliyor

			//		select* from Notlar
			//where Kullanici_Id = 1
			return View(nots.ToList());
		}

		public ActionResult Begendiklerim()
		{
			LikeYonet ly = new LikeYonet();
			var nots = ny.ListeleQueryable().Include(n => n.Kategori);

			if (Session["login"] != null)
			{
				Kullanici kullanici = (Kullanici)Session["login"];

				nots = ly.ListeleQueryable().Include("Kullanici").Include("Not").Where(x => x.Kullanici.Id == kullanici.Id).Select(x => x.Not).Include(k => k.Kategori);
				/// bağlı oldukları tabloyu gösteren fieldları yazdık
				/// yani
				/// select n.*from Begeni f
				///inner join Notlar n on f.Not_Id = n.Id
				///where f.Kullanici_Id = 1
			}

			return View("Index", nots.ToList());
		}
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Not not = ny.NotBul(id.Value);
			if (not == null)
			{
				return HttpNotFound();
			}
			return View(not);
		}

		// GET: Not/Create
		KategoriYonet ky = new KategoriYonet();
		public ActionResult Create()
		{
			ViewBag.KategoriId = new SelectList(ky.Listele(), "Id", "Baslik");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Not not)
		{
			Kullanici kullanici = null;

			if (Session["login"] != null)
			{
				kullanici = (Kullanici)Session["login"];
			}
			not.Kullanici = kullanici;

			ViewBag.KategoriId = new SelectList(ky.Listele(), "Id", "Baslik", not.KategoriId);

			ModelState.Remove("DegistirenKullanici");

			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Not> sonuc = ny.NotKaydet(not);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
					return View(not);
				}

				//ny.NotKaydet(not);
				return RedirectToAction("Index");
			}
			return View(not);
		}

		// GET: Not/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Not not = ny.NotBul(id.Value);
			if (not == null)
			{
				return HttpNotFound();
			}
			ViewBag.KategoriId = new SelectList(ky.Listele(), "Id", "Baslik", not.KategoriId);
			return View(not);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Not not)
		{
			ViewBag.KategoriId = new SelectList(ky.Listele(), "Id", "Baslik", not.KategoriId);

			ModelState.Remove("DegistirenKullanici");

			if (ModelState.IsValid)
			{
				//db.Entry(not).State = EntityState.Modified;
				//db.SaveChanges();,
				//ny.NotUpdate(not);
				BusinessLayerSonuc<Not> sonuc = ny.NotUpdate(not);
				if (sonuc.Hatalar.Count>0)
				{
					sonuc.Hatalar.ForEach(x=>ModelState.AddModelError("",x));
					return View(not);
				}
				return RedirectToAction("Index");
			}

			return View(not);
		}

		// GET: Not/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Not not = ny.NotBul(id.Value);
			if (not == null)
			{
				return HttpNotFound();
			}
			return View(not);
		}

		// POST: Not/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Not not = ny.NotBul(id);

			BusinessLayerSonuc<Not> sonuc=ny.NotSil(not);
			if (sonuc.Hatalar.Count > 0)
			{
				sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
				return View(not);
			}
			//ny.NotSil(not);
			return RedirectToAction("Index");
		}
	}
}
