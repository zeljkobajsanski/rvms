using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using rs.mvc.Korisnici.Repository;

namespace RVMS.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RvmsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RvmsService.svc or RvmsService.svc.cs at the Solution Explorer and start debugging.
    public class RvmsService : IRvmsService
    {
        public Opstina[] VratiOpstine()
        {
            return new Model.Repository.Repository<Opstina>().GetActive().OrderBy(x => x.NazivOpstine).ToArray();
        }

        public StajalisteDTO[] VratiStajalisteOpstine(int? idOpstine)
        {
            return new StajalistaRepository().VratiStajalistaOpstine(idOpstine).OrderBy(x => x.Naziv).Select(x => new StajalisteDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Opstina = x.Opstina.NazivOpstine
            }).ToArray();
        }

        public RelacijaDTO[] VratiDaljinar(int? idStajalista)
        {
            return new RelacijeRepository().VratiRelacije(idStajalista).Select(x => new RelacijaDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                DuzinaRelacije = x.DuzinaRelacije,
                VremeVoznje = x.VremeVoznje,
                SrednjaSaobracajnaBrzina = x.SrednjaSaobracajnaBrzina,
                Napomena = x.Napomena
            }).ToArray();
        }

        public RelacijaSaMedjustanicnimRastojanjimaDTO VratiRelacijuSaRastojanjima(int idRelacije)
        {
            var relacija = new RelacijeRepository().VratiRelacijuSaRastojanjima(idRelacije);
            if (relacija == null) return null;
            var retVal = new RelacijaSaMedjustanicnimRastojanjimaDTO
            {
                IdRelacije = relacija.Id,
                NazivRelacije = relacija.Naziv,
                Napomena = relacija.Napomena,
                Stanice = relacija.MedjustanicnaRastojanja.Select(x => new MedjustanicnoRastojanjeDTO()
                {
                    Id = x.Id,
                    Rbr = x.Rbr,
                    PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                    PolaznoStajalisteId = x.PolaznoStajalisteId,
                    DolaznoStajalisteId = x.DolaznoStajalisteId,
                    DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                    Rastojanje = x.Rastojanje,
                    VremeVoznje = x.VremeVoznje,
                    LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                    LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                    LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                    LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
                }).OrderBy(x => x.Rbr).ToArray()
            };
            IzracunajRelaciju(retVal.Stanice);
            return retVal;
        }

        

        public int SacuvajRelaciju(Relacija relacija)
        {
            if (Validator.TryValidateObject(relacija, new ValidationContext(relacija, null, null),
                                            new Collection<ValidationResult>()))
            {
                var repository = new RelacijeRepository();
                if (relacija.Id == 0)
                {
                    repository.Add(relacija);
                }
                else
                {
                    repository.Update(relacija);
                }
                repository.Save();
                return relacija.Id;
            }
            throw new InvalidOperationException("Podaci nisu validni");
        }

        public void ObrisiRelaciju(int idRelacije)
        {
            var repo = new RelacijeRepository();
            var relacija = repo.Get(idRelacije);
            if (relacija != null)
            {
                relacija.Aktivan = false;
                repo.Save();
            }
        }

        public MedjustanicnoRastojanjeDTO[] SacuvajRastojanje(MedjustanicnoRastojanje rastojanje)
        {
            var r = new MedjustanicnaRastojanjaRepository();
            var broj = r.VratiMedjustanicnaRastojanja(rastojanje.RelacijaId).Count();
            rastojanje.Rbr = broj + 1;
            if (rastojanje.Id == 0)
            {
                r.Add(rastojanje);
            }
            else
            {
                var msr = r.Get(rastojanje.Id);
                msr.PolaznoStajalisteId = rastojanje.PolaznoStajalisteId;
                msr.DolaznoStajalisteId = rastojanje.DolaznoStajalisteId;
                msr.Rastojanje = rastojanje.Rastojanje;
                msr.VremeVoznje = rastojanje.VremeVoznje;
            }
            r.Save();
            var retVal = r.VratiMedjustanicnaRastojanja(rastojanje.RelacijaId).Select(x => new MedjustanicnoRastojanjeDTO()
            {
                Id = x.Id,
                Rbr = x.Rbr,
                PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                PolaznoStajalisteId = x.PolaznoStajalisteId,
                DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                DolaznoStajalisteId = x.DolaznoStajalisteId,
                Rastojanje = x.Rastojanje,
                VremeVoznje = x.VremeVoznje,
                LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
            }).OrderBy(x => x.Rbr).ToArray();
            IzracunajRelaciju(retVal);
            return retVal;
        }

        public MedjustanicnoRastojanjeDTO[] ObrisiRastojanje(int id)
        {
            var r = new MedjustanicnaRastojanjaRepository();
            var rastojanje = r.Get(id);
            r.Delete(rastojanje);
            r.Save();
            var retVal = r.VratiMedjustanicnaRastojanja(rastojanje.RelacijaId).Select(x => new MedjustanicnoRastojanjeDTO()
            {
                Id = x.Id,
                Rbr = x.Rbr,
                PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                PolaznoStajalisteId = x.PolaznoStajalisteId,
                DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                DolaznoStajalisteId = x.DolaznoStajalisteId,
                Rastojanje = x.Rastojanje,
                VremeVoznje = x.VremeVoznje,
                LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
            }).OrderBy(x => x.Rbr).ToArray();
            IzracunajRelaciju(retVal);
            return retVal;
        }

        public Mesto[] VratiMesta(int? idOpstine)
        {
            return new MestaRepository().VratiMestaOpstine(idOpstine).OrderBy(x => x.Naziv).ToArray();
        }

        public StajalisteDTO[] VratiStajalistaMestaIOpstine(int? idOpstine, int? idMesta)
        {
            return new StajalistaRepository().VratiStajalista(idOpstine, idMesta, true)
                                             .Select(x => new StajalisteDTO()
                                             {
                                                 Id = x.Id,
                                                 Naziv = x.Naziv,
                                                 Opstina = x.Opstina.NazivOpstine,
                                                 Mesto = x.Mesto.Naziv,
                                                 Stanica = x.Stanica,
                                                 Latituda = x.GpsLatituda,
                                                 Longituda = x.GpsLongituda,
                                                 OpstinaId = x.OpstinaId,
                                                 MestoId = x.MestoId,
                                                 Novo = x.Novo
                                             }).ToArray();
        }

        public int SacuvajStajaliste(Stajaliste stajaliste)
        {
            var repo = new StajalistaRepository();
            if (stajaliste.Id == 0)
            {
                repo.Add(stajaliste);
            }
            else
            {
                repo.Update(stajaliste);
            }
            repo.Save();
            return stajaliste.Id;
        }

        public MedjustanicnoRastojanje VratiMedjustanicnoRastojanje(int id)
        {
            return new MedjustanicnaRastojanjaRepository().Get(id);
        }

        public MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeGore(int idRelacije, int idMedjustanicnogRastojanja)
        {
            var r = new MedjustanicnaRastojanjaRepository();
            var rastojanja = r.VratiMedjustanicnaRastojanja(idRelacije);
            var msr = rastojanja.Single(x => x.Id == idMedjustanicnogRastojanja);
            if (msr.Rbr > 0)
            {
                var rbr = msr.Rbr - 1;
                var zameni = rastojanja.Where(x => x.Rbr == rbr);
                foreach (var medjustanicnoRastojanje in zameni)
                {
                    medjustanicnoRastojanje.Rbr = msr.Rbr;
                }
                msr.Rbr = rbr;
                r.Save();
            }
            var retVal = r.VratiMedjustanicnaRastojanja(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO()
            {
                Id = x.Id,
                Rbr = x.Rbr,
                PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                PolaznoStajalisteId = x.PolaznoStajalisteId,
                DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                DolaznoStajalisteId = x.DolaznoStajalisteId,
                Rastojanje = x.Rastojanje,
                VremeVoznje = x.VremeVoznje,
                LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
            }).OrderBy(x => x.Rbr).ToArray();
            IzracunajRelaciju(retVal);
            return retVal;
        }

        public MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeDole(int idRelacije, int idMedjustanicnogRastojanja)
        {
            var r = new MedjustanicnaRastojanjaRepository();
            var rastojanja = r.VratiMedjustanicnaRastojanja(idRelacije);
            var msr = rastojanja.Single(x => x.Id == idMedjustanicnogRastojanja);
            if (msr.Rbr < rastojanja.Count())
            {
                var rbr = msr.Rbr + 1;
                var zameni = rastojanja.Where(x => x.Rbr == rbr);
                foreach (var medjustanicnoRastojanje in zameni)
                {
                    medjustanicnoRastojanje.Rbr = msr.Rbr;
                }
                msr.Rbr = rbr;
                r.Save();
            }
            var retVal = r.VratiMedjustanicnaRastojanja(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO()
            {
                Id = x.Id,
                Rbr = x.Rbr,
                PolaznoStajaliste = x.PolaznoStajaliste.Naziv,
                PolaznoStajalisteId = x.PolaznoStajalisteId,
                DolaznoStajaliste = x.DolaznoStajaliste.Naziv,
                DolaznoStajalisteId = x.DolaznoStajalisteId,
                Rastojanje = x.Rastojanje,
                VremeVoznje = x.VremeVoznje,
                LatitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLatituda,
                LongitudaPolaznogStajalista = x.PolaznoStajaliste.GpsLongituda,
                LatitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLatituda,
                LongitudaDolaznogStajalista = x.DolaznoStajaliste.GpsLongituda
            }).OrderBy(x => x.Rbr).ToArray();
            IzracunajRelaciju(retVal);
            return retVal;
        }

        private static void IzracunajRelaciju(IEnumerable<MedjustanicnoRastojanjeDTO> rastojanja)
        {
            var duzinaRelacije = 0M;
            var vremeVoznje = 0;

            foreach (var medjustanicnoRastojanjeDto in rastojanja)
            {
                duzinaRelacije += medjustanicnoRastojanjeDto.Rastojanje;
                vremeVoznje += medjustanicnoRastojanjeDto.VremeVoznje;
                medjustanicnoRastojanjeDto.DuzinaRelacije = duzinaRelacije;
                medjustanicnoRastojanjeDto.VremeVoznjePoRelaciji = vremeVoznje;
            }
        }

        
    }
}
