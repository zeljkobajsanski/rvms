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
    [Authorize]
    public class StajalistaController : Controller
    {
        private readonly StajalistaRepository fRepository = new StajalistaRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Insert(Stajaliste stajaliste)
        {
            stajaliste.Aktivan = true;
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Add(stajaliste);
            fRepository.Save();
        }

        [HttpPost]
        public void Update(Stajaliste stajaliste)
        {
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Update(stajaliste);
            fRepository.Save();
        }

        public JsonResult VratiStajalista(int idOpstine, int? idMesta)
        {
            var stajalista = fRepository.VratiStajalista(idOpstine, idMesta);
            return Json(stajalista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VratiAktivnaStajalista()
        {
            var aktivnaStajalista = fRepository.VratiAktivnaStajalista().OrderBy(x => x.Naziv).Select(x => new StajalisteDTO
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Opstina = x.Opstina.NazivOpstine,
                Stanica = x.Stanica
            });
            
            return Json(aktivnaStajalista, JsonRequestBehavior.AllowGet);
        }
    }
}
