using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardenyaGirisimciKadinlar.Models;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class KategorisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kategoris
        public ActionResult Index()
        {
            return View(db.Kategoris.ToList());
        }

        // GET: Kategoris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                if (ViewBag.KID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    id = ViewBag.KID;
                }
            }
            Kategori kategori = db.Kategoris.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.KID = id;
            ViewBag.KAD = kategori.KategoriAdi;
            return View(kategori);
        }
        public ActionResult AltKategoriPartial(int? id)
        {
            if (id == null)
            {
                if (ViewBag.KID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    id = ViewBag.KID;
                }
            }
            var result = (from k in db.Kategoris
                          join ak in db.AltKategoris on k.KategoriID equals ak.KategoriID
                          where ak.KategoriID == id
                          select new KategoriAltkategoriViewModel
                          {
                           AltKategoriID=    ak.AltKategoriID,
                              AltKategoriAdi= ak.AltKategoriAdi,
                              AltKategoriLink=ak.AltKategoriLink,
                              KategoriAdi=k.KategoriAdi,
                             KategoriID= ak.KategoriID
                          }).ToList();
            var ad = db.Kategoris.Where(x => x.KategoriID == id).FirstOrDefault();
            ViewBag.KID = id;
            ViewBag.KAD = ad.KategoriAdi;
            return PartialView(result);
        }
        public ActionResult AltKategoriEkle(int? id)
        {
            if (id == null)
            {
                if (ViewBag.KID==null)
                {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    id = ViewBag.KID;
                }
                
            }
            var result = (from k in db.Kategoris
                          
                          where k.KategoriID == id
                          select new KategoriAltkategoriViewModel
                          {
                             KategoriAdi=k.KategoriAdi,
                             KategoriID= k.KategoriID
                          }).FirstOrDefault();
            ViewBag.KID = id;
            ViewBag.KAD = result.KategoriAdi;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AltKategoriEkle(KategoriAltkategoriViewModel altk)
        {
            AltKategori alt = new AltKategori();
            alt.AltKategoriAdi = altk.AltKategoriAdi;
            alt.AltKategoriLink = altk.AltKategoriLink;
            alt.KategoriID = altk.KategoriID;
            db.AltKategoris.Add(alt);
            db.SaveChanges();
            ViewBag.KID = altk.KategoriID;
            ViewBag.KAD = altk.KategoriAdi;
            return View("Details");
        }
        // GET: Kategoris/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AltKategoriDuzenle(int? id,int? kid)
        {
            if (kid == null)
            {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var altkategori  = db.AltKategoris.Where(x=>x.AltKategoriID==kid).FirstOrDefault();
            Kategori kategori = db.Kategoris.Find(id);
            if (altkategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.KAD = kategori.KategoriAdi;
            ViewBag.KID = kategori.KategoriID;
            return View(altkategori);
        }
        [HttpPost]
           public ActionResult AltKategoriDuzenle([Bind(Include = "AltKategoriID,AltKategoriAdi,AltKategoriLink")]AltKategori altkategori)
        {
            AltKategori alt = db.AltKategoris.Where(x => x.AltKategoriID == altkategori.AltKategoriID).FirstOrDefault();
            alt.AltKategoriAdi = altkategori.AltKategoriAdi;
            alt.AltKategoriLink = altkategori.AltKategoriLink;
            
            db.SaveChanges();

            Kategori kategori = db.Kategoris.Where(x=>x.KategoriID== alt.KategoriID).FirstOrDefault();
            if (altkategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.KAD = kategori.KategoriAdi;
            ViewBag.KID = kategori.KategoriID;
            return View("Details");
        }
        [HttpPost]
        public ActionResult AltKategoriSil(int? kid)
        {
            if (kid == null)
            {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AltKategori altkategori = db.AltKategoris.Find(kid);
            db.AltKategoris.Remove(altkategori);
            db.SaveChanges();
            ViewBag.KID = altkategori.KategoriID;
            Kategori ktgr = db.Kategoris.Where(x => x.KategoriID == altkategori.KategoriID).FirstOrDefault();
            ViewBag.KAD = ktgr.KategoriAdi;
            return View("Details");
        }
        public ActionResult AltKategoriSil(int? id, int? kid)
        {
            if (kid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AltKategori altkategori = db.AltKategoris.Find(kid);
            Kategori kategori = db.Kategoris.Find(id);
            if (altkategori == null)
            {
                return HttpNotFound();
            }

           
            ViewBag.KAD = kategori.KategoriAdi;
            ViewBag.KID = kategori.KategoriID;
            return View(altkategori);
        }

        // POST: Kategoris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriID,KategoriAdi,KategoriLink")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategoris.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        // GET: Kategoris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategoris.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategoris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriID,KategoriAdi,KategoriLink")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategoris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategoris.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = db.Kategoris.Find(id);
            db.Kategoris.Remove(kategori);
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
