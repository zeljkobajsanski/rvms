using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using rs.mvc.Korisnici.Filters;

namespace RVMS.Web.Controllers
{
    [Authorize]
    [LogujAktivnost]
    public class RelacijeController : Controller
    {
        private readonly RelacijeRepository fRelacijeRepository = new RelacijeRepository();

        private readonly MedjustanicnaRastojanjaRepository fMedjustanicnaRastojanjaRepository = new MedjustanicnaRastojanjaRepository();

        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Nova()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Izmeni(int id)
        {
            var relacija = fRelacijeRepository.Get(id);
            if (relacija == null) throw new HttpException(404, "Relacija nije pronađena");
            return View("Nova", relacija);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult SacuvajRelaciju(Relacija relacija)
        {
            relacija.Aktivan = true;
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            if (relacija.Id == 0)
            {
                fRelacijeRepository.Add(relacija);
            }
            else
            {
                fRelacijeRepository.Update(relacija);
            }
            fRelacijeRepository.Save();
            return Json(relacija.Id);
        }

        [System.Web.Mvc.HttpPost]
        public void InsertMedjustanicnoRastojanje(MedjustanicnoRastojanje medjustanicnoRastojanje)
        {
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fMedjustanicnaRastojanjaRepository.Add(medjustanicnoRastojanje);
            fMedjustanicnaRastojanjaRepository.Save();
        }

        public JsonResult MedjustanicnaRastojanja(int idRelacije)
        {
            var medjustanicnaRastojanja =
                fMedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanja(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO
                {
                    Id = x.Id,
                    PolaznoStajalisteId = x.PolaznoStajalisteId,
                    PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                    DolaznoStajalisteId = x.DolaznoStajalisteId,
                    DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                    Rastojanje = x.Rastojanje,
                    VremeVoznje = x.VremeVoznje,
                    LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                    LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                    LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                    LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
                }).ToArray();
            decimal duzinaRelacije = 0;
            int vremeVoznje = 0;
            foreach (var msr in medjustanicnaRastojanja)
            {
                duzinaRelacije += msr.Rastojanje;
                msr.DuzinaRelacije = duzinaRelacije;
                vremeVoznje += msr.VremeVoznje;
                msr.VremeVoznjePoRelaciji = vremeVoznje;
            }

            return Json(medjustanicnaRastojanja, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public void UpdateMedjustanicnoRastojanje(MedjustanicnoRastojanje medjustanicnoRastojanje)
        {
            //if (!ModelState.IsValid) throw new HttpRequestValidationException();
            var msr = fMedjustanicnaRastojanjaRepository.Get(medjustanicnoRastojanje.Id);
            msr.Rastojanje = medjustanicnoRastojanje.Rastojanje;
            msr.VremeVoznje = medjustanicnoRastojanje.VremeVoznje;
            fMedjustanicnaRastojanjaRepository.Save();
        }

        public JsonResult VratiRelacije()
        {
            var relacije = fRelacijeRepository.VratiRelacije().Select(x => new RelacijaDTO
            {
                Id = x.Id,
                Naziv = x.Naziv,
                DuzinaRelacije = x.DuzinaRelacije,
                VremeVoznje = x.VremeVoznje,
                SrednjaSaobracajnaBrzina = x.SrednjaSaobracajnaBrzina
            });
            return Json(relacije, JsonRequestBehavior.AllowGet);
        }

        public void Obrisi(int id)
        {
            var relacija = fRelacijeRepository.Get(id);
            relacija.Aktivan = false;
            fRelacijeRepository.Save();
        }

        [System.Web.Mvc.HttpPost]
        public void ObrisiMedjustanicnoRastojanje(int id)
        {
            var msr = fMedjustanicnaRastojanjaRepository.Get(id);
            fMedjustanicnaRastojanjaRepository.Delete(msr);
            fMedjustanicnaRastojanjaRepository.Save();
        }

    }
}
