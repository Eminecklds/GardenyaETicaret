using GardenyaGirisimciKadinlar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //View modelimle çalışıp bana bilgi verecek
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartviewmodel = new ShoppingCardViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),

            };

           
            return View(cartviewmodel);
        }
        //addCart
        public ActionResult AddToCart([Bind(Include = "UrunID,Adet")] Urunler urunler)
        {
            var eklenen = db.Urunlers.FirstOrDefault(x => x.UrunID == urunler.UrunID);
            eklenen.Adet = urunler.Adet;
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(eklenen);
            return RedirectToAction("Index");

        }
        //RemoveCart
        public ActionResult RemoveToCart(int id)
        {
            //item countu değişti
            //total amount değişti
            //view model mesaj vermiştik bunu da gönderelim

            //var kaldir = db.Products.FirstOrDefault(x => x.ProductID == id).ProductID;
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //Kullanıcıya mesaj vermek istiyorum
            //şu isimli ürün sepetinizden kaldırıldı gibi
            //o yüzden şunu yazmam gerekecek
            string itemname = db.Carts.FirstOrDefault(x => x.RecordID == id).Urunler.Baslik;//Ürünün adını getirmek için yazdık

            int itemcount = cart.RemoveFromCart(id);
            var result = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemname) + " sepetinizden başarıyla kaldırıldı",
                CartTotal = cart.GetTotal(),
                Count = cart.GetCount(),
                ItemCount = itemcount
            };
            return Json(result);

        }
        //Cart Summary
        //Partial view olabilir
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}