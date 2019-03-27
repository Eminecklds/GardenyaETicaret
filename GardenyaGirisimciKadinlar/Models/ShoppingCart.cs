﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Models
{
    public class ShoppingCart
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Urunler Urunler)
        {
            //Urunler Urunler =  db.Urunlers.Where(x => x.UrunID == Urun.UrunID).FirstOrDefault();
             
            // Get the matching cart and album instances
            var cartItem = db.Carts.SingleOrDefault(
                c => c.CartID == ShoppingCartId
                && c.UrunID == Urunler.UrunID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    UrunID = Urunler.UrunID,
                    CartID = ShoppingCartId,
                    Count = Urunler.Adet,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count+=Urunler.Adet;
            }
            // Save changes
            db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CartID == ShoppingCartId
                && cart.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(
                cart => cart.CartID == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            var result = db.Carts.Where(
                cart => cart.CartID == ShoppingCartId).ToList();
            foreach (var item in result)
            {
                item.SubTotal = item.Count * item.Urunler.Fiyat;
            }
            return result;
        }
        //public List<ShoppingCardViewModel> cartAraTplm(Cart liste)
        //{
        //   var cartt= db.Carts.Where(
        //        cart => cart.CartID == ShoppingCartId).ToList();
        //    var aratplm=new List<ShoppingCardViewModel>();
        //    foreach (var item in cartt)
        //    {
               
        //        aratplm.CartAraToplam = item.Count * item.Urunler.Fiyat;
        //    }
        //    return aratplm;
        //}
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Urunler.Fiyat).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateSiparis(Siparis Siparis)
        {
            decimal SiparisTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the Siparis details for each
            foreach (var item in cartItems)
            {
                var SiparisDetail = new SiparisDetay
                {
                    UrunID = item.UrunID,
                    SiparisID = Siparis.SiparisID,
                    Adet = item.Urunler.Fiyat,
                    Fiyat = item.Count
                };
                // Set the Siparis total of the shopping cart
                SiparisTotal += (item.Count * item.Urunler.Fiyat);

                db.SiparisDetays.Add(SiparisDetail);

            }
            // Set the Siparis's total to the SiparisTotal count
            Siparis.Total = SiparisTotal;

            // Save the Siparis
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the SiparisId as the confirmation number
            return Siparis.SiparisID;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(
                c => c.CartID == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }
    }
}