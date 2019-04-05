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
        public ActionResult Index(int? id)
        {
            if (id==null)
            {
                return View("Index","Home");
            }
            ViewBag.ID = id;
            var result = (from  ak in db.AltKategoris 
                          where ak.KategoriID == id
                          select new UrunlerGirisimciAltKatViewModel {
                              AltKategoriID=ak.AltKategoriID,
                               AltKategoriAdi=ak.AltKategoriAdi,
                              KategoriID=ak.KategoriID
                          }  ).ToList();
            return View(result);
        }
        [HttpGet]
        public JsonResult getByID(string id)
        {
            int no = Convert.ToInt32(id);
            var altkat = db.Urunlers.Where(x=>x.AltKategoriID==no).Select(x=>x.AltKategoriID).FirstOrDefault();
            return Json(altkat, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UrunlerListesi(Urunler altkatID)
        {
            //var liste=(from u in db.Urunlers 
            //           where u.AltKategoriID==kid
            //           select new UrunlerGirisimciAltKatViewModel
            //           {
            //               UrunID=u.UrunID,
            //               Baslik=u.Baslik,
            //               Aciklama=u.Aciklama,
            //               Resim=u.Resim,
            //               Fiyat=u.Fiyat
            //           } ).ToList();
            int id = altkatID.AltKategoriID;
            var liste = (from u in db.Urunlers
                         where u.AltKategoriID == id
                         select new UrunlerGirisimciAltKatViewModel {
                             UrunID = u.UrunID,
                             Baslik = u.Baslik,
                             Aciklama = u.Aciklama,
                             Resim = u.Resim,
                             Fiyat = u.Fiyat
                         }
                       ).ToList();
            return Json(liste, JsonRequestBehavior.AllowGet); //get olduğunu belirtmek için yanına virgül ile get e izin virdiğimizi yazdık
        }
        public ActionResult Urunler(int id)
        {
            //int id=ViewBag.KatID;
            var result = (from ak in db.AltKategoris join
                          u in db.Urunlers on ak.AltKategoriID equals u.AltKategoriID
                          where u.AltKategoriID == id select ak
                                  
                        ).ToList();
            //var resultUrunler = (from u in db.Urunlers
            //                     join k in result on u.AltKategoriID equals k.AltKategoriID
            //                     select new Urunler
            //                     {
            //                         UrunID = u.UrunID,
            //                         Baslik = u.Baslik,
            //                         Resim = u.Resim,
            //                         Fiyat= u.Fiyat
            //                     });

            ViewBag.KatID = id;
            return PartialView(result);
        }
        public ActionResult AltKategoriler(int id=1)
        {
            //int id = ViewBag.KatID;
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