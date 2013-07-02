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
                rastojanje =  Math.Round(medjustanicnoRastojanje ?? 0, 0) + poslednjeStajaliste.Rastojanje;
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

            UcitajRelacijeKojeProlazeKrozStajaliste(idStajalista, dto);
            return dto;
        }

        private void UcitajRelacijeKojeProlazeKrozStajaliste(int idStajalista, LinijaSaKandidatimaDTO dto)
        {
            var relacije = fRepositories.RelacijeRepository.VratiRelacijeKojeProlazeKrozStanicu(idStajalista).ToArray();
            dto.Relacije = new List<RelacijaDTO>();
            foreach (var relacija in relacije)
            {
                var relacijaDto = new RelacijaDTO()
                {
                    Id = relacija.Id,
                    Naziv = relacija.Naziv,
                    Napomena = KreirajStringOpisaRelacije(relacija)
                };
                dto.Relacije.Add(relacijaDto);
            }
        }


        [OperationContract]
        public LinijaSaKandidatimaDTO DodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije)
        {
            var relacija = fRepositories.RelacijeRepository.VratiRelacijuSaRastojanjima(idRelacije);
            var linija = fRepositories.LinijeRepository.UcitajLinijuIStajalista(idLinije);
            var poslednjeStajaliste = linija.Stajalista.LastOrDefault();
            if (poslednjeStajaliste == null)
            {
                var dodatoPrvo = false;
                StajalisteLinije poslednjeDodatoStajaliste = null;
                foreach (var msr in relacija.MedjustanicnaRastojanja)
                {
                    if (!dodatoPrvo)
                    {
                        var stajalisteLinije = new StajalisteLinije()
                        {
                            LinijaId = idLinije,
                            Stajaliste = msr.PolaznoStajaliste,
                        };
                        linija.Stajalista.Add(stajalisteLinije);
                        stajalisteLinije = new StajalisteLinije()
                        {
                            LinijaId = idLinije,
                            Stajaliste = msr.DolaznoStajaliste,
                            Rastojanje = Math.Round(msr.Rastojanje, 0)
                        };
                        linija.Stajalista.Add(stajalisteLinije);
                        dodatoPrvo = true;
                        poslednjeDodatoStajaliste = stajalisteLinije;
                    }
                    else
                    {
                        var stajalisteLinije = new StajalisteLinije()
                        {
                            LinijaId = idLinije,
                            Stajaliste = msr.DolaznoStajaliste,
                            Rastojanje = poslednjeDodatoStajaliste.Rastojanje + Math.Round(msr.Rastojanje, 0)
                        };
                        linija.Stajalista.Add(stajalisteLinije);
                        poslednjeDodatoStajaliste = stajalisteLinije;
                    }
                }
            }
            else
            {
                var found = false;
                foreach (var msr in relacija.MedjustanicnaRastojanja)
                {
                    if (msr.PolaznoStajalisteId == poslednjeStajaliste.StajalisteId)
                    {
                        found = true;
                        var stajalisteLinije = new StajalisteLinije()
                        {
                            LinijaId = idLinije,
                            Stajaliste = msr.DolaznoStajaliste,
                            Rastojanje = poslednjeStajaliste.Rastojanje + Math.Round(msr.Rastojanje, 0)
                        };
                        linija.Stajalista.Add(stajalisteLinije);
                        continue;
                    }
                    if (found)
                    {
                        linija.Stajalista.Add(new StajalisteLinije()
                        {
                            LinijaId = idLinije,
                            Stajaliste = msr.DolaznoStajaliste,
                            Rastojanje = linija.Stajalista.Last().Rastojanje + Math.Round(msr.Rastojanje, 0)
                        });
                    }
                }
            }
            fRepositories.Save();
            poslednjeStajaliste = linija.Stajalista.Last();
            var dto = new LinijaSaKandidatimaDTO()
            {
                Linija = ObjectMapper.Map(linija),
                Stajalista = fStajalista.VratiSusednaStajalista(poslednjeStajaliste.StajalisteId),
            };
            UcitajRelacijeKojeProlazeKrozStajaliste(poslednjeStajaliste.StajalisteId, dto);
            return dto;
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