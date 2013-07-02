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
        private readonly MedjustanicnaRastojanjaRepository m_MedjustanicnaRastojanjaRepository = new MedjustanicnaRastojanjaRepository();

        private readonly RelacijeRepository m_RelacijeRepository = new RelacijeRepository();

        private readonly StajalistaRepository m_StajalistaRepository = new StajalistaRepository();

        private readonly MestaRepository m_MestaRepository = new MestaRepository();

        public Opstina[] VratiOpstine()
        {
            return new Model.Repository.Repository<Opstina>().GetActive().OrderBy(x => x.NazivOpstine).ToArray();
        }

        public StajalisteDTO[] VratiStajalisteOpstine(int? idOpstine)
        {
            return m_StajalistaRepository.VratiStajalistaOpstine(idOpstine).OrderBy(x => x.Naziv).Select(x => new StajalisteDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Opstina = x.Opstina.NazivOpstine
            }).ToArray();
        }

        public RelacijaDTO[] VratiDaljinar(int tipStajalista, int? idStajalista)
        {
            return m_RelacijeRepository.VratiRelacije(tipStajalista, idStajalista).Select(x => new RelacijaDTO()
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
            var relacija = m_RelacijeRepository.VratiRelacijuSaRastojanjima(idRelacije);
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
                var repository = m_RelacijeRepository;
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
            var repo = m_RelacijeRepository;
            var relacija = repo.Get(idRelacije);
            if (relacija != null)
            {
                relacija.Aktivan = false;
                repo.Save();
            }
        }

        public MedjustanicnoRastojanjeDTO[] SacuvajRastojanje(MedjustanicnoRastojanje rastojanje)
        {
            var broj = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(rastojanje.RelacijaId).Count();
            rastojanje.Rbr = broj + 1;
            if (rastojanje.Id == 0)
            {
                m_MedjustanicnaRastojanjaRepository.Add(rastojanje);
            }
            else
            {
                var msr = m_MedjustanicnaRastojanjaRepository.Get(rastojanje.Id);
                msr.PolaznoStajalisteId = rastojanje.PolaznoStajalisteId;
                msr.DolaznoStajalisteId = rastojanje.DolaznoStajalisteId;
                msr.Rastojanje = rastojanje.Rastojanje;
                msr.VremeVoznje = rastojanje.VremeVoznje;
            }
            m_MedjustanicnaRastojanjaRepository.Save();
            var retVal = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(rastojanje.RelacijaId).Select(x => new MedjustanicnoRastojanjeDTO()
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
            var rastojanje = m_MedjustanicnaRastojanjaRepository.Get(id);
            m_MedjustanicnaRastojanjaRepository.Delete(rastojanje);
            m_MedjustanicnaRastojanjaRepository.Save();
            var retVal = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(rastojanje.RelacijaId).Select(x => new MedjustanicnoRastojanjeDTO()
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
            return m_MestaRepository.VratiMestaOpstine(idOpstine).OrderBy(x => x.Naziv).ToArray();
        }

        public StajalisteDTO[] VratiStajalistaMestaIOpstine(int? idOpstine, int? idMesta)
        {
            return m_StajalistaRepository.VratiStajalista(idOpstine, idMesta, true)
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
            if (stajaliste.Id == 0)
            {
                m_StajalistaRepository.Add(stajaliste);
            }
            else
            {
                m_StajalistaRepository.Update(stajaliste);
            }
            m_StajalistaRepository.Save();
            return stajaliste.Id;
        }

        public MedjustanicnoRastojanje VratiMedjustanicnoRastojanje(int id)
        {
            return m_MedjustanicnaRastojanjaRepository.Get(id);
        }

        public MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeGore(int idRelacije, int idMedjustanicnogRastojanja)
        {
            var rastojanja = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(idRelacije);
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
                m_MedjustanicnaRastojanjaRepository.Save();
            }
            var retVal = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO()
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

        public StajalisteSaRelacijamaDTO VratiStajalisteSaRelacijama(int idStajalista)
        {
            var stajaliste = m_StajalistaRepository.VratiStajalisteIOpstinu(idStajalista);
            var relacije = m_RelacijeRepository.VratiRelacije(1, idStajalista).ToArray();
            return new StajalisteSaRelacijamaDTO
            {
                Id = stajaliste.Id,
                Naziv = stajaliste.Naziv,
                Opstina = stajaliste.Opstina.NazivOpstine,
                Latitude = stajaliste.GpsLatituda,
                Longitude = stajaliste.GpsLongituda,
                Relacije = relacije.Select(x => new RelacijaDTO()
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToArray()
            };
        }

        public string ObrisiStajaliste(int idStajalista)
        {
            var postoji = m_RelacijeRepository.PostojiRelacijaSaStajalistem(idStajalista);
            if (postoji) return "Brisanje nije dozvoljeno: Postoje relacije sa izabranim stajalištem";
            var stajaliste = m_StajalistaRepository.Get(idStajalista);
            if (stajaliste != null)
            {
                stajaliste.Aktivan = false;
                m_StajalistaRepository.Save();
            }
            return null;
        }

        public void SvediStajalistaNaPodrazumevano(int idPodrazumevanogStajalista, int[] stajalistaKojaSeSvode)
        {
            foreach (var idStajalista in stajalistaKojaSeSvode)
            {
                var daljinar = m_MedjustanicnaRastojanjaRepository.VratiSvaMedjustanicnaRastojanjaSaStajalistem(idStajalista).ToArray();
                foreach (var medjustanicnoRastojanje in daljinar)
                {
                    if (medjustanicnoRastojanje.PolaznoStajalisteId == idStajalista)
                    {
                        medjustanicnoRastojanje.PolaznoStajalisteId = idPodrazumevanogStajalista;
                    }
                    if (medjustanicnoRastojanje.DolaznoStajalisteId == idStajalista)
                    {
                        medjustanicnoRastojanje.DolaznoStajalisteId = idPodrazumevanogStajalista;
                    }
                }
                ObrisiStajaliste(idStajalista);
            }
            m_MedjustanicnaRastojanjaRepository.Save();
        }

        public LinijaSaKandidatimaDTO DodajStajalisteNaLiniju(int idLinije, int idStajalista)
        {
            var dto = new LinijaSaKandidatimaDTO();
            dto.Stajalista = VratiSusednaStajalista(idStajalista);
            var relacije = m_RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(idStajalista).ToArray();
            dto.Relacije = new List<RelacijaDTO>();
            foreach (var relacija in relacije)
            {
                var relacijaDto = new RelacijaDTO()
                {
                    Id = relacija.Id,
                    Naziv = relacija.Naziv,
                    Napomena = KreirajStringOpisaRelacije(relacija).Replace(Environment.NewLine, ",")
                };
                dto.Relacije.Add(relacijaDto);
            }
            return dto;
        }

        public LinijaSaKandidatimaDTO SkloniStajalisteSaLinije(int idLinije, int idStajalista)
        {
            var dto = new LinijaSaKandidatimaDTO();
            dto.Stajalista = VratiSusednaStajalista(idStajalista);
            return dto;
        }

        public string VratiTooltipZaRelaciju(int idRelacije)
        {
            var relacija = m_RelacijeRepository.VratiRelacijuSaRastojanjima(idRelacije);
            return KreirajStringOpisaRelacije(relacija);
        }

        private static string KreirajStringOpisaRelacije(Relacija relacija)
        {
            var sb = new StringBuilder();
            var len = relacija.MedjustanicnaRastojanja.Count;
            
            for (int i = 0; i < len; i++)
            {
                var medjustanicnoRastojanje = relacija.MedjustanicnaRastojanja[i];
                if (i == 0)
                {
                    sb.AppendLine(medjustanicnoRastojanje.PolaznoStajaliste.Naziv);
                }
                else
                {
                    sb.AppendLine(medjustanicnoRastojanje.DolaznoStajaliste.Naziv);
                }
            }
            if (len == 1)
            {
                sb.AppendLine(relacija.MedjustanicnaRastojanja[0].DolaznoStajaliste.Naziv);
            }
            return sb.ToString();
        }

        public StajalisteDTO[] VratiSusednaStajalista(int idStajalista)
        {
            var stajalista = new List<StajalisteDTO>();
            var medjustanicnaRastojanja = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanja(idStajalista);
            foreach (var medjustanicnoRastojanje in medjustanicnaRastojanja)
            {
                if (medjustanicnoRastojanje.PolaznoStajalisteId == idStajalista)
                {
                    stajalista.Add(new StajalisteDTO()
                    {
                        Id = medjustanicnoRastojanje.DolaznoStajalisteId,
                        Naziv = medjustanicnoRastojanje.DolaznoStajaliste.Naziv,
                        Latituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLatituda,
                        Longituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLongituda,
                        Opstina = medjustanicnoRastojanje.DolaznoStajaliste.Opstina.NazivOpstine
                    });
                }
                if (medjustanicnoRastojanje.DolaznoStajalisteId == idStajalista)
                {
                    stajalista.Add(new StajalisteDTO()
                    {
                        Id = medjustanicnoRastojanje.PolaznoStajalisteId,
                        Naziv = medjustanicnoRastojanje.PolaznoStajaliste.Naziv,
                        Latituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLatituda,
                        Longituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLongituda,
                        Opstina = medjustanicnoRastojanje.PolaznoStajaliste.Opstina.NazivOpstine
                    });
                }
            }
            
            return stajalista.Distinct(StajalisteDTO.IdComparer).ToArray();
        }

        

        public MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeDole(int idRelacije, int idMedjustanicnogRastojanja)
        {
            var rastojanja = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(idRelacije);
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
                m_MedjustanicnaRastojanjaRepository.Save();
            }
            var retVal = m_MedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanjaNaRelaciji(idRelacije).Select(x => new MedjustanicnoRastojanjeDTO()
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
