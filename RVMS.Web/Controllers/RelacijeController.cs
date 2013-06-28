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
    
    public class RelacijeController : Controller
    {
        private readonly RelacijeRepository fRelacijeRepository = new RelacijeRepository();

        private readonly MedjustanicnaRastojanjaRepository fMedjustanicnaRastojanjaRepository = new MedjustanicnaRastojanjaRepository();

        [Authorize]
        [LogujAktivnost]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [LogujAktivnost]
        [System.Web.Mvc.HttpGet]
        public ActionResult Nova()
        {
            return View();
        }

        [Authorize]
        [LogujAktivnost]
        [System.Web.Mvc.HttpGet]
        public ActionResult Izmeni(int id)
        {
            var relacija = fRelacijeRepository.Get(id);
            if (relacija == null) throw new HttpException(404, "Relacija nije pronađena");
            return View("Nova", relacija);
        }

        [Authorize]
        [LogujAktivnost]
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

        [Authorize]
        [LogujAktivnost]
        [System.Web.Mvc.HttpPost]
        public void InsertMedjustanicnoRastojanje(MedjustanicnoRastojanje medjustanicnoRastojanje)
        {
            if (!ModelState.IsValid) throw new HttpRequestValidationException();
            fMedjustanicnaRastojanjaRepository.Add(medjustanicnoRastojanje);
            fMedjustanicnaRastojanjaRepository.Save();
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult MedjustanicnaRastojanja(int idRelacije)
        {
            var medjustanicnaRastojanja =
                fMedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO
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

        [Authorize]
        [LogujAktivnost]
        [System.Web.Mvc.HttpPost]
        public void UpdateMedjustanicnoRastojanje(MedjustanicnoRastojanje medjustanicnoRastojanje)
        {
            //if (!ModelState.IsValid) throw new HttpRequestValidationException();
            var msr = fMedjustanicnaRastojanjaRepository.Get(medjustanicnoRastojanje.Id);
            msr.Rastojanje = medjustanicnoRastojanje.Rastojanje;
            msr.VremeVoznje = medjustanicnoRastojanje.VremeVoznje;
            fMedjustanicnaRastojanjaRepository.Save();
        }

        [Authorize]
        [LogujAktivnost]
        public JsonResult VratiRelacije()
        {
            var relacije = fRelacijeRepository.VratiRelacije(1, null).Select(x => new RelacijaDTO
            {
                Id = x.Id,
                Naziv = x.Naziv,
                DuzinaRelacije = x.DuzinaRelacije,
                VremeVoznje = x.VremeVoznje,
                SrednjaSaobracajnaBrzina = x.SrednjaSaobracajnaBrzina,
                Napomena = x.Napomena
            });
            return Json(relacije, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [LogujAktivnost]
        public void Obrisi(int id)
        {
            var relacija = fRelacijeRepository.Get(id);
            relacija.Aktivan = false;
            fRelacijeRepository.Save();
        }

        [Authorize]
        [LogujAktivnost]
        [System.Web.Mvc.HttpPost]
        public void ObrisiMedjustanicnoRastojanje(int id)
        {
            var msr = fMedjustanicnaRastojanjaRepository.Get(id);
            fMedjustanicnaRastojanjaRepository.Delete(msr);
            fMedjustanicnaRastojanjaRepository.Save();
        }

        public ActionResult MapaRelacije(int? id)
        {
            if (id.HasValue)
            {
                var stajalista = fMedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(id.Value)
                                                  .Select(x => new MedjustanicnoRastojanjeDTO()
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
                return View(stajalista);
            }
            return View(Enumerable.Empty<MedjustanicnoRastojanjeDTO>());
        }

    }
}
