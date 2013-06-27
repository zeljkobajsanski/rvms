using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using rs.mvc.Korisnici.Filters;

namespace RVMS.Web.Controllers
{
    
    public class StajalistaController : Controller
    {
        private readonly StajalistaRepository fRepository = new StajalistaRepository();

        [Authorize]
        [LogujAktivnost]
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var stajaliste = fRepository.Get(id.Value);
                return View(stajaliste);
            }
            return View();
        }

        [Authorize]
        [LogujAktivnost]
        [HttpPost]
        public void Insert(Stajaliste stajaliste)
        {
            stajaliste.Aktivan = true;
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Add(stajaliste);
            fRepository.Save();
        }

        [Authorize]
        [LogujAktivnost]
        [HttpPost]
        public void Update(Stajaliste stajaliste)
        {
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fRepository.Update(stajaliste);
            fRepository.Save();
        }

        [HttpPost]
        public void AzurirajKoordinatu(Stajaliste stajaliste)
        {
            var s = fRepository.Get(stajaliste.Id);
            s.GpsLatituda = stajaliste.GpsLatituda;
            s.GpsLongituda = stajaliste.GpsLongituda;
            fRepository.Save();
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult VratiStajalista(int idOpstine, int? idMesta)
        {
            var stajalista = fRepository.VratiStajalista(idOpstine, idMesta).ToArray();
            return Json(stajalista, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult PretraziStajalista(int? idOpstine, int? idMesta, string nazivStajalista)
        {
            var stajalista = fRepository.PretraziStajalista(idOpstine, idMesta, nazivStajalista).Select(x => new StajalisteDTO
            {
                Id = x.Id,
                Opstina = x.Opstina.NazivOpstine,
                Mesto = x.Mesto != null ? x.Mesto.Naziv : null,
                Naziv = x.Naziv
            }).ToArray();
            return Json(stajalista, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult VratiAktivnaStajalista()
        {
            var aktivnaStajalista = fRepository.VratiAktivnaStajalista().OrderBy(x => x.Opstina.NazivOpstine).ThenBy(x => x.Naziv).Select(x => new StajalisteDTO
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Opstina = x.Opstina.NazivOpstine,
                Stanica = x.Stanica
            });
            
            return Json(aktivnaStajalista, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult VratiStajaliste(int id)
        {
            var stajaliste = fRepository.Get(id);
            return Json(stajaliste, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapaStajalista(int id)
        {
            var stajaliste = fRepository.Get(id);
            return View(stajaliste);
        }

        public ActionResult PoredjenjeStajalista()
        {
            return View();
        }
    }
}
