using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RVMS.Model.Entities;
using RVMS.Model.Repository;

namespace RVMS.Web.Controllers
{
    [Authorize]
    public class MestaController : Controller
    {
        private readonly MestaRepository fRepository = new MestaRepository(); 

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VratiMesta(int idOpstine)
        {
            var mesta = fRepository.VratiMestaOpstine(idOpstine);
            return Json(mesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VratiAktivnaMesta(int idOpstine)
        {
            var mesta = fRepository.GetActive().Where(x => x.OpstinaId == idOpstine).OrderBy(x => x.Naziv);
            return Json(mesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Insert(Mesto mesto)
        {
            mesto.Aktivan = true;
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Add(mesto);
            fRepository.Save();
        }

        [HttpPost]
        public void Update(Mesto mesto)
        {
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Update(mesto);
            fRepository.Save();
        }
    }
}
