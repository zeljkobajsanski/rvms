using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;

namespace RVMS.Web.Controllers
{
    public class PrevozniciController : Controller
    {
        private readonly PrevozniciRepository fPrevozniciRepository = new PrevozniciRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prevoznik(int? id)
        {
            var prevoznik = new Prevoznik();

            if (id.HasValue)
            {
                prevoznik = fPrevozniciRepository.Get(id.Value);
            }
            return View(prevoznik);
        }

        public JsonResult Sacuvaj(Prevoznik prevoznik)
        {
            if (prevoznik.Id == 0)
            {
                prevoznik.Aktivan = true;
                fPrevozniciRepository.Add(prevoznik);
            }
            else
            {
                var p = fPrevozniciRepository.Get(prevoznik.Id);
                p.Naziv = prevoznik.Naziv;
                p.Adresa = prevoznik.Adresa;
                p.Mesto = prevoznik.Mesto;
            }
            fPrevozniciRepository.Save();
            return Json(prevoznik);
        }

        public JsonResult VratiAktivnePrevoznike()
        {
            var prevoznici = fPrevozniciRepository.VratiAktivnePrevoznike().Select(x => new PrevoznikDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv
            });
            return Json(prevoznici, JsonRequestBehavior.AllowGet);
        }

    }
}
