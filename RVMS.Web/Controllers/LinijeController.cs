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
    public class LinijeController : Controller
    {
        private readonly MedjustanicnaRastojanjaRepository m_MedjustanicnaRastojanjaRepository =
            new MedjustanicnaRastojanjaRepository();

        private readonly LinijeRepository m_LinijeRepository = new LinijeRepository();

        private readonly StajalistaRepository m_StajalistaRepository = new StajalistaRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Linija(int? id)
        {
            if (id.HasValue)
            {
                var linija = m_LinijeRepository.Get(id.Value);
                return View(linija);
            }
            return View(new Linija());
        }

        public JsonResult VratiSusednaStajalista(int? idLinije)
        {
            List<StajalisteDTO> stajalista = new List<StajalisteDTO>();
            var polaznoStajaliste = m_LinijeRepository.VratiIdPoslednjegStajalistaNaLiniji(idLinije.Value);

            if (!polaznoStajaliste.HasValue)
            {
                stajalista = m_StajalistaRepository.VratiAktivnaStajalista().Select(x => new StajalisteDTO
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Opstina = x.Opstina.NazivOpstine,
                    Latituda = x.GpsLatituda,
                    Longituda = x.GpsLongituda
                }).ToList();
            }
            else
            {
                var medjustanicnaRastojanja =
                    m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanja(polaznoStajaliste);
                stajalista.AddRange(medjustanicnaRastojanja.Where(x => x.PolaznoStajalisteId == polaznoStajaliste)
                                                           .Select(x => new StajalisteDTO()
                                                           {
                                                               Id = x.DolaznoStajalisteId,
                                                               Naziv = x.DolaznoStajaliste.Naziv,
                                                               Opstina = x.DolaznoStajaliste.Opstina.NazivOpstine,
                                                               Latituda = x.DolaznoStajaliste.GpsLatituda,
                                                               Longituda = x.DolaznoStajaliste.GpsLongituda
                                                           }).Distinct(StajalisteDTO.IdComparer));
                stajalista.AddRange(medjustanicnaRastojanja.Where(x => x.DolaznoStajalisteId == polaznoStajaliste)
                                                           .Select(x => new StajalisteDTO()
                                                           {
                                                               Id = x.PolaznoStajalisteId,
                                                               Naziv = x.PolaznoStajaliste.Naziv,
                                                               Opstina = x.PolaznoStajaliste.Opstina.NazivOpstine,
                                                               Latituda = x.PolaznoStajaliste.GpsLatituda,
                                                               Longituda = x.PolaznoStajaliste.GpsLongituda
                                                           }).Distinct(StajalisteDTO.IdComparer));
                //stajalista = stajalista.Where(x => x.Id != polaznoStajaliste).ToList();
            }
            var returnList = stajalista.OrderBy(x => x.Opstina).ThenBy(x => x.Naziv);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IzaberiStajaliste(int idLinije, int id)
        {
            var linija = m_LinijeRepository.UcitajLinijuIStajalista(idLinije);
            var stajalisteLinije = new StajalisteLinije
            {
                Rbr = linija.Stajalista.Any() ? linija.Stajalista.Count() + 1 : 1,
                LinijaId = linija.Id,
                StajalisteId = id,
            };
            linija.Stajalista.Add(stajalisteLinije);
            var pretposledjeStajaliste = linija.PretposledjeStajaliste();
            if (pretposledjeStajaliste != null)
            {
                stajalisteLinije.Rastojanje = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanja(pretposledjeStajaliste.StajalisteId, id) ?? 0;
            }
            m_LinijeRepository.Save();
            var stajalista = m_LinijeRepository.VratiStajalistaLinije(idLinije).OrderBy(x => x.Rbr).Select(x => new StajalisteDTO()
            {
                Id = x.Id,
                IdStajalistaLinije = x.Id,
                Naziv = x.Stajaliste.Naziv,
                Latituda = x.Stajaliste.GpsLatituda,
                Longituda = x.Stajaliste.GpsLongituda,
                Udaljenost = x.Rastojanje
            }).ToArray();
            return Json(stajalista);
        }

        public JsonResult UcitajLiniju(int id)
        {
            var linija = m_LinijeRepository.UcitajLinijuIStajalista(id);
            var stajalista = linija.Stajalista.OrderBy(x => x.Rbr);
            var dto = new LinijaDTO
            {
                Id = linija.Id,
                Naziv = linija.Naziv,
                PrevoznikId = linija.PrevoznikId,
                Stajalista = new List<StajalisteDTO>(stajalista.Select(x => new StajalisteDTO()
                {
                    Id = x.StajalisteId,
                    IdStajalistaLinije = x.Id,
                    Naziv = x.Stajaliste.Naziv,
                    Latituda = x.Stajaliste.GpsLatituda,
                    Longituda = x.Stajaliste.GpsLongituda,
                    Udaljenost = x.Rastojanje
                }))
            };
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public void ObrisiStavku(int id)
        {
            var repos = new Repository<StajalisteLinije>();
            var stajalisteLinije = repos.Get(id);
            repos.Delete(stajalisteLinije);
            repos.Save();
        }

        [HttpPost]
        public ActionResult SacuvajLiniju(Linija linija)
        {
            if (linija.Id == 0)
            {
                linija.Aktivan = true;
                m_LinijeRepository.Add(linija);
            }
            else
            {
                var l = m_LinijeRepository.Get(linija.Id);
                l.Naziv = linija.Naziv;
            }
            m_LinijeRepository.Save();
            return Json(linija);
        }
    }
}
