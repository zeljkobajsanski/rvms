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
        public LinijaSaKandidatimaDTO DodajStajalisteNaLiniju(int idLinije, int idStajalista)
        {
            fRepositories.StajalistaLinijeRepository.Add(new StajalisteLinije()
            {
                LinijaId = idLinije,
                StajalisteId = idStajalista
            });
            fRepositories.Save();
            
            var dto = new LinijaSaKandidatimaDTO
                          {
                              Linija = UcitajLiniju(idLinije),
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
            var relacija = fRepositories.RelacijeRepository.VratiRelacijuSaRastojanjima(idRelacije);
            var dto = new LinijaSaKandidatimaDTO()
            {
                Linija = new LinijaDTO { Id = idLinije }
            };
            var poslednje = relacija.MedjustanicnaRastojanja.Last();
            dto.Stajalista = fStajalista.VratiSusednaStajalista(poslednje.DolaznoStajalisteId);
            var relacije = fRepositories.RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(poslednje.DolaznoStajalisteId).ToArray();
            dto.Relacije = new List<RelacijaDTO>();
            foreach (var r in relacije)
            {
                var relacijaDto = new RelacijaDTO()
                {
                    Id = r.Id,
                    Naziv = r.Naziv,
                    Napomena = KreirajStringOpisaRelacije(r).Replace(Environment.NewLine, ",")
                };
                dto.Relacije.Add(relacijaDto);
            }
            var medjustanicnaRastojanja = relacija.MedjustanicnaRastojanja.ToArray();

            if (medjustanicnaRastojanja.Length == 1)
            {
                dto.Linija.Stajalista.Add(new StajalisteDTO
                {
                    Id = medjustanicnaRastojanja[0].Id,
                    Naziv = medjustanicnaRastojanja[0].DolaznoStajaliste.Naziv,
                    Latituda = medjustanicnaRastojanja[0].DolaznoStajaliste.GpsLatituda,
                    Longituda = medjustanicnaRastojanja[0].DolaznoStajaliste.GpsLongituda,
                });
            }
            var found = false;
            for (int i = 1; i < medjustanicnaRastojanja.Length; i++)
            {
                var medjustanicnoRastojanje = medjustanicnaRastojanja[i];
                if (i < medjustanicnaRastojanja.Length - 1)
                {
                    dto.Linija.Stajalista.Add(new StajalisteDTO
                    {
                        Id = medjustanicnoRastojanje.Id,
                        Naziv = medjustanicnoRastojanje.PolaznoStajaliste.Naziv,
                        Latituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLatituda,
                        Longituda = medjustanicnoRastojanje.PolaznoStajaliste.GpsLongituda,
                    });
                }
                else
                {
                    dto.Linija.Stajalista.Add(new StajalisteDTO
                    {
                        Id = medjustanicnoRastojanje.Id,
                        Naziv = medjustanicnoRastojanje.DolaznoStajaliste.Naziv,
                        Latituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLatituda,
                        Longituda = medjustanicnoRastojanje.DolaznoStajaliste.GpsLongituda,
                    });
                }
            }

            return dto;
        }

        [OperationContract]
        public LinijaSaKandidatimaDTO SkloniStajalisteSaLinije(int idLinije, int idStajalista)
        {
            var dto = new LinijaSaKandidatimaDTO();
            dto.Stajalista = fStajalista.VratiSusednaStajalista(idStajalista);
            return dto;
        }

        private LinijaDTO UcitajLiniju(int idLinije)
        {
            var linija = fRepositories.LinijeRepository.UcitajLinijuIStajalista(idLinije);
            return linija != null ? ObjectMapper.Map(linija) : null;
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
    }
}