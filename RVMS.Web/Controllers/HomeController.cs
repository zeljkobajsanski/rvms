using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using rs.mvc.Korisnici.Filters;
using rs.mvc.Korisnici.Services;

namespace RVMS.Web.Controllers
{
    [Authorize]
    [LogujAktivnost]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult VratiKorisnika()
        {
            var korisnik = Cookies.VratiKorisnikaIzKukija(Request);
            return korisnik != null ? (ActionResult)Content(korisnik.Korisnik) : (ActionResult)new EmptyResult();
        }
    }
}
