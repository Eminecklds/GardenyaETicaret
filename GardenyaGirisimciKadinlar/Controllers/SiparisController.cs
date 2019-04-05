using GardenyaGirisimciKadinlar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenyaGirisimciKadinlar.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Siparis
        public ActionResult SiparisTamamla(Cart cart )
        {
            if (User.Identity.IsAuthenticated)
            {

                return View();
            }
            else {
                 return View("Login","Account");
            }
          
        }
    }
}