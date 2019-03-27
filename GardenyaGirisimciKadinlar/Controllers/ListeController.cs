using GardenyaGirisimciKadinlar.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class ListeController : Controller
    {
        // GET: Liste
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult KullaniciListe()
        {

            var Kullanicilar = db.Users.Where(x=>x.UserRole=="User").ToList();
            return View(Kullanicilar.ToList());
        }
        public ActionResult GirisimciListe()
        {

            var Girisimciler = db.Users.Where(x=>x.UserRole== "Girisimci").ToList();
            return View(Girisimciler.ToList());
        }
        public ActionResult Profil()
        {
            if (User.Identity.IsAuthenticated)
            {
               var userid = User.Identity.GetUserId();
            var kisi = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            return View(kisi);
            }
            else {  return RedirectToAction("Login","Account");
            }
        }
        //Add view yapmadım 
        public ActionResult Edit(string Id)
        {
            if (Id!="")
            {
               var userid = User.Identity.GetUserId();
            var kisi = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            return View(kisi);
            }
            else {  return RedirectToAction("Profil","Liste");
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
 var userid = User.Identity.GetUserId();
            if (file != null && file.ContentLength > 0)
                {
                    WebImage img = new WebImage(file.InputStream);
                    FileInfo fotoinfo = new FileInfo(file.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(500, 775);
                    img.Save("~/Upload/Profil/" + newfoto);
                    user.Resim = newfoto;

            }
            else
            {
            ApplicationUser euser = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            user.Resim = euser.Resim;
            }
            ApplicationUser kuser = db.Users.Where(k => k.Id == userid).SingleOrDefault();
            kuser.Id = user.Id;
            kuser.Ad = user.Ad;
            kuser.Soyad = user.Soyad;
            kuser.PhoneNumber = user.PhoneNumber;
            kuser.Resim = user.Resim;
            db.SaveChanges();

            //db.Entry(user).State = EntityState.Modified;



            try
            {
                // Kodlarınız

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
                //}
                return RedirectToAction("Profil", "Liste");
        }
        public ActionResult GProfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                var kisi = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                return View(kisi);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult GEdit(string Id)
        {
            if (Id != "")
            {
                var userid = User.Identity.GetUserId();
                var kisi = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                return View(kisi);
            }
            else
            {
                return RedirectToAction("Profil", "Liste");
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GEdit(ApplicationUser user, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            var userid = User.Identity.GetUserId();
            if (file != null && file.ContentLength > 0)
            {
                WebImage img = new WebImage(file.InputStream);
                FileInfo fotoinfo = new FileInfo(file.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(500, 775);
                img.Save("~/Upload/Profil/" + newfoto);
                user.Resim = newfoto;

            }
            else
            {
                ApplicationUser euser = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                user.Resim = euser.Resim;
            }
            ApplicationUser kuser = db.Users.Where(k => k.Id == userid).SingleOrDefault();
            kuser.Id = user.Id;
            kuser.Ad = user.Ad;
            kuser.Soyad = user.Soyad;
            kuser.PhoneNumber = user.PhoneNumber;
            kuser.Resim = user.Resim;
            db.SaveChanges();

            //db.Entry(user).State = EntityState.Modified;



            try
            {
                // Kodlarınız

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            //}
            return RedirectToAction("Profil", "Liste");
        }
    }
}