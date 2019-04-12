using GardenyaGirisimciKadinlar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var result = (from u in db.Urunlers orderby u.EklenmeTarihi descending select u).Take(7).ToList();
            ViewBag.EnYeni = result;
            return View();
        }
        public ActionResult Menu()
        {
            
            return PartialView(db.Kategoris.ToList());
        }

        public ActionResult getUrunler()
        {
            
            return PartialView(db.Urunlers.Take(5).ToList());
        }
        public ActionResult getUrunlerID()
        {
            
            return PartialView(db.Urunlers.Take(5).ToList());
        }

        public ActionResult Urunler()
        {
            ViewBag.Kategoriler = db.AltKategoris.ToList();
            return PartialView(db.Urunlers.Take(30).ToList());
        }
        public ActionResult UrunDetay(int? id)
        {          ViewBag.Adet = 1;
            if (id==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var result = (from u in db.Urunlers
                              join k in db.AltKategoris on u.AltKategoriID equals k.AltKategoriID
                              join g in db.Users on u.GirisimciID equals g.Id
                              where u.UrunID == id
                              select new UrunlerGirisimciAltKatViewModel()
                              {
                                  GirisimciAdi=g.Ad+" "+g.Soyad,
                                  AltKategoriAdi=k.AltKategoriAdi,
                                  Aciklama=u.Aciklama,
                                  Baslik=u.Baslik,
                                  Fiyat=u.Fiyat,
                                  Resim=u.Resim,
                                  Adet=1,
                                  UrunID=u.UrunID
                              }).FirstOrDefault();
                    return View(result);
            }
  
        }



    }
}