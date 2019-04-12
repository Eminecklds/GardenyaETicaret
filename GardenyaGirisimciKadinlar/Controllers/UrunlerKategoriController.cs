using GardenyaGirisimciKadinlar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class UrunlerKategoriController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UrunlerKategori

            
        public ActionResult Index(int? id, int? kid)
        {
            if (id==null)
            {
                return View("Index","Home");
            }
            var result = (from  ak in db.AltKategoris 
                          where ak.KategoriID == id
                          select new UrunlerGirisimciAltKatViewModel {
                              AltKategoriID=ak.AltKategoriID,
                               AltKategoriAdi=ak.AltKategoriAdi,
                              KategoriID=ak.KategoriID
                          }  ).ToList();
            if (kid!=null)
            {
                var urunler = (from u in db.Urunlers
                               where u.AltKategoriID == kid
                               select new UrunlerGirisimciAltKatViewModel
                               {
                                   UrunID = u.UrunID,
                                   Baslik = u.Baslik,
                                   Aciklama = u.Aciklama,
                                   Resim = u.Resim,
                                   Fiyat = u.Fiyat
                               }).ToList();
                ViewBag.urunler = urunler;
            }
            else
            {
                var urunler = db.Urunlers.Where(x => db.AltKategoris.Select(i => i.AltKategoriID).Contains(x.AltKategoriID));

            }
            return View(result);
        }

        public ActionResult Urunler(int id)
        {
 
            var result = (from ak in db.AltKategoris join
                          u in db.Urunlers on ak.AltKategoriID equals u.AltKategoriID
                          where u.AltKategoriID == id select ak
                                  
                        ).ToList();


            ViewBag.KatID = id;
            return PartialView(result);
        }
        public ActionResult AltKategoriler(int id=1)
        {
   
            var result = (from k in db.Kategoris
                          join ak in db.AltKategoris on k.KategoriID equals ak.KategoriID
                          where k.KategoriID == id
                          select new UrunlerGirisimciAltKatViewModel {
                              AltKategoriID=ak.AltKategoriID,
                              AltKategoriAdi=ak.AltKategoriAdi
                          }
                        ).ToList();
            ViewBag.KatID = id;
            return PartialView(result);
        }
    }
}