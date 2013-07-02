using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RVMS.Model;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using RVMS.Model.Repository.Interfaces;
using RVMS.Services.Services.Interfaces;

namespace RVMS.Services.Services
{
    [ServiceContract]
    public class Linije
    {
        private readonly IRepositories fRepositories = new Repositories();

        private readonly IStajalista fStajalista;

        public Linije() : this(new Repositories(), new Stajalista())
        {
        }

        public Linije(IRepositories repositories, IStajalista stajalista)
        {
            fRepositories = repositories;
            fStajalista = stajalista;
        }

        [OperationContract]
        public int SacuvajLiniju(LinijaDTO linijaDto)
        {
            var linija = new Linija
            {
                Id = linijaDto.Id,
                PrevoznikId = linijaDto.PrevoznikId,
                Naziv = linijaDto.Naziv
            };
            if (linija.Id == 0)
            {
                fRepositories.LinijeRepository.Add(linija);
            }
            else
            {
                fRepositories.LinijeRepository.Update(linija);
            }
            fRepositories.Save();
            return linija.Id;
        }

        [OperationContract]
        public void AzurirajStajalisteLinije(StajalisteLinijeDTO stajalisteLinijeDto)
        {
            var stajalisteLinije = ObjectMapper.Map(stajalisteLinijeDto);
            fRepositories.StajalistaLinijeRepository.Update(stajalisteLinije);
            fRepositories.Save();
        }

        [OperationContract]
        public LinijaSaKandidatimaDTO DodajStajalisteNaLiniju(int idLinije, int idStajalista)
        {
            var linija = fRepositories.LinijeRepository.UcitajLinijuIStajalista(idLinije);
            decimal rastojanje = 0;
            if (linija.Stajalista.Any())
            {
                var poslednjeStajaliste = linija.Stajalista.Last();
                var medjustanicnoRastojanje = fRepositories.MedjustanicnaRastojanjaRepository
                                          .VratiMedjustanicnaRastojanja(poslednjeStajaliste.Stajaliste.Id, idStajalista);
                rastojanje =  (medjustanicnoRastojanje ?? 0) + poslednjeStajaliste.Rastojanje;
            }
            linija.Stajalista.Add(new StajalisteLinije()
            {
                LinijaId = idLinije,
                StajalisteId = idStajalista,
                Stajaliste = fRepositories.StajalistaRepository.Get(idStajalista),
                Rastojanje = rastojanje
            });
            fRepositories.Save();

            var dto = new LinijaSaKandidatimaDTO
            {
                Linija = ObjectMapper.Map(linija),
                Stajalista = fStajalista.VratiSusednaStajalista(idStajalista)
            };

            var relacije = fRepositories.RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(idStajalista).ToArray();
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

        

        [OperationContract]
        public LinijaSaKandidatimaDTO DodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije)
        {
            //var relacija = fRepositories.RelacijeRepository.VratiRelacijuSaRastojanjima(idRelacije);
            //var dto = new LinijaSaKandidatimaDTO()
            //{
            //    Linija = new LinijaDTO { Id = idLinije }
            //};
            //var poslednje = relacija.MedjustanicnaRastojanja.Last();
            //dto.Stajalista = fStajalista.VratiSusednaStajalista(poslednje.DolaznoStajalisteId);
            //var relacije = fRepositories.RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(poslednje.DolaznoStajalisteId).ToArray();
            //dto.Relacije = new List<RelacijaDTO>();
            //foreach (var r in relacije)
            //{
            //    var relacijaDto = new RelacijaDTO()
            //    {
            //        Id = r.Id,
            //        Naziv = r.Naziv,
            //        Napomena = KreirajStringOpisaRelacije(r).Replace(Environment.NewLine, ",")
            //    };
            //    dto.Relacije.Add(relacijaDto);
            //}
            //var medjustanicnaRastojanja = relacija.MedjustanicnaRastojanja.ToArray();

            //if (medjustanicnaRastojanja.Length == 1)
            //{
            //    dto.Linija.Stajalista.Add(new StajalisteDTO
            //    {
            //        Id = medjustanicnaRastojanja[0].Id,
            //        Naziv = medjustanicnaRastojanja[0].DolaznoStajaliste.Naziv,
            //        Latituda = medjustanicnaRastojanja[0].DolaznoStajaliste.GpsLatituda,
            //        Longituda = medjustanicnaRastojanja[0].DolaznoStajaliste.GpsLongituda,
            //    });
            //}
            //var found = false;
            //for (int i = 1; i < medjustanicnaRastojanja.Length; i++)
            //{
            //    var medjustanicnoRastojanje = medjustanicnaRastojanja[i];
            //    if (i < medjustanicnaRastojanja.Length - 1)
            //    {
            //        dto.Linija.Stajalista.Add(new StajalisteDTO
            //        {
            //            Id = medjustanicnoRastojanje.Id,
            //            Naziv = medjustanicnoRastojanje.PolaznoStajaliste.Naziv,
            //            Latituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLatituda,
            //            Longituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLongituda,
            //        });
            //    }
            //    else
            //    {
            //        dto.Linija.Stajalista.Add(new StajalisteDTO
            //        {
            //            Id = medjustanicnoRastojanje.Id,
            //            Naziv = medjustanicnoRastojanje.DolaznoStajaliste.Naziv,
            //            Latituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLatituda,
            //            Longituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLongituda,
            //        });
            //    }
            //}

            //return dto;
            return null;
        }

        [OperationContract]
        public LinijaSaKandidatimaDTO SkloniStajalisteSaLinije(int idLinije, int idStajalistaLinije)
        {
            var linija = fRepositories.LinijeRepository.UcitajLinijuIStajalista(idLinije);
            var found = false;
            foreach (var stajalisteLinije in linija.Stajalista.ToArray())
            {
                if (stajalisteLinije.Id == idStajalistaLinije)
                {
                    found = true;
                }
                if (found)
                {
                    linija.Stajalista.Remove(stajalisteLinije);
                    fRepositories.StajalistaLinijeRepository.Delete(stajalisteLinije);
                }
            }
            fRepositories.Save();
            var dto = new LinijaSaKandidatimaDTO()
            {
                Linija = ObjectMapper.Map(linija)
            };
            var poslednjeStajaliste = linija.Stajalista.LastOrDefault();
            if (poslednjeStajaliste == null)
            {
                dto.Stajalista = fStajalista.VratiStajalista(null, null);
                dto.Relacije = new List<RelacijaDTO>();
            }
            else
            {
                dto.Stajalista = fStajalista.VratiSusednaStajalista(poslednjeStajaliste.StajalisteId);
                var relacije = fRepositories.RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(poslednjeStajaliste.StajalisteId);
                dto.Relacije = relacije.Select(ObjectMapper.Map).ToList();
                foreach (var relacija in relacije)
                {
                    var relacijaDto = dto.Relacije.Single(x => x.Id == relacija.Id);
                    relacijaDto.Napomena = KreirajStringOpisaRelacije(relacija);
                }
            }
            
            return dto;
        }

        public static string KreirajStringOpisaRelacije(Relacija relacija)
        {
            var sb = new StringBuilder();
            var len = relacija.MedjustanicnaRastojanja.Count;

            for (int i = 0; i < len; i++)
            {
                var medjustanicnoRastojanje = relacija.MedjustanicnaRastojanja[i];
                if (i == 0)
                {
                    sb.Append(medjustanicnoRastojanje.PolaznoStajaliste.Naziv).Append(",");
                }
                sb.Append(medjustanicnoRastojanje.DolaznoStajaliste.Naziv);
                if (i < len - 1) sb.Append(',');
            }
            
            return sb.ToString();
        }
    }
}