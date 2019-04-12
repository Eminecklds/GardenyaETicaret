using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GardenyaGirisimciKadinlar.Models;
using Microsoft.AspNet.Identity;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class UrunlersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Urunlers
        [Authorize(Roles = "Admin")]
        public ActionResult TumUrunler()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var urunlers = db.Urunlers.Include(u => u.AltKategori);

            return View(urunlers.ToList());
        }
        [Authorize(Roles = "Girisimci")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userid = User.Identity.GetUserId();
            

            var urunlers = db.Urunlers.Where(x=>x.GirisimciID==userid).ToList();
            return View(urunlers.ToList());
        }

        // GET: Urunlers/Details/5
        [Authorize(Roles = "Girisimci")]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            string rsm = "../" + urunler.Resim;
            ViewBag.Resim = rsm;
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        [Authorize(Roles = "Girisimci")]
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.AltKategoriID = new SelectList(db.AltKategoris, "AltKategoriID", "AltKategoriAdi");
            //var userid = User.Identity.GetUserId();

            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,Baslik,Aciklama,Adet,Fiyat,AltKategoriID")] Urunler urunler, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    WebImage img = new WebImage(file.InputStream);
                    FileInfo fotoinfo = new FileInfo(file.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(500, 775);
                    img.Save("~/Upload/Products/" + newfoto);
                    urunler.Resim = "../Upload/Products/" + newfoto;
                    urunler.EklenmeTarihi = DateTime.Now;

                }
                var userid = User.Identity.GetUserId();
                urunler.GirisimciID =userid;

                db.Urunlers.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AltKategoriID = new SelectList(db.AltKategoris, "AltKategoriID", "AltKategoriAdi", urunler.AltKategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        [Authorize(Roles = "Girisimci")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltKategoriID = new SelectList(db.AltKategoris, "AltKategoriID", "AltKategoriAdi", urunler.AltKategoriID);
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,Baslik,Aciklama,Adet,Fiyat,EklenmeTarihi,AltKategoriID")] Urunler urunler, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength>0)
                {
                    WebImage img = new WebImage(file.InputStream);
                    FileInfo fotoinfo = new FileInfo(file.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(500, 775);
                    img.Save("~/Upload/Products/" + newfoto);
                    urunler.Resim = "../Upload/Products/" + newfoto;

                }
                var userid = User.Identity.GetUserId();
                urunler.GirisimciID = userid;
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AltKategoriID = new SelectList(db.AltKategoris, "AltKategoriID", "AltKategoriAdi", urunler.AltKategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            string rsm = "../" + urunler.Resim;
            ViewBag.Resim =rsm ;
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunlers.Find(id);
            db.Urunlers.Remove(urunler);
            db.SaveChanges();
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
    }
}
