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
		KategoriYonet ky = new KategoriYonet();

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
			Kategori kategori = ky.KategoriBul(id.Value);
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
		public ActionResult Create(Kategori kategori)
		{
			ModelState.Remove("DegistirenKullanici");  // validationda değiştiren kullanıcıyı kontrol etme dmeiş olduk yani kutuya tıkladığımızda verilen uyarılarda değiştiren kullanıcı kısmı çıkmasın

			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Kategori> sonuc = ky.KategoriEkle(kategori);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
					return View(kategori);
				}

				/*  ky.KategoriEkle(kategori);  */ // bu methodu burada oluşturduk           
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
			ModelState.Remove("DegistirenKullanici");

			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Kategori> sonuc = ky.KategoriUpdate(kategori);
				//db.Entry(kategori).State = EntityState.Modified;
				//db.SaveChanges();  bunlar yerine alttaki

				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
					return View(kategori);
				}
				//ky.KategoriUpdate(kategori);
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
		[ValidateAntiForgeryToken]    // delete sayfasında da forgery token var sayfaaya yapılan saldırılarda kontorl gibi bi şeu
		public ActionResult DeleteConfirmed(int id)
		{
			Kategori kategori = ky.KategoriBul(id);
			//db.Kategoris.Remove(kategori);
			//db.SaveChanges();

			BusinessLayerSonuc<Kategori> sonuc = ky.KategoriSil(kategori);
			if (sonuc.Hatalar.Count>0)
			{
				sonuc.Hatalar.ForEach(x=>ModelState.AddModelError("",x));
				return View(sonuc.nesne);
			}
			//ky.KategoriSil(kategori);
			return RedirectToAction("Index");
		}
	}
}
