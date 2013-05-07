using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rs.mvc.Korisnici.Filters;

namespace RVMS.Web.Controllers
{
    [Authorize]
    [Aktivnost]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
