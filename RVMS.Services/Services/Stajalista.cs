﻿using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using RVMS.Model.DTO;
using RVMS.Model.Repository;

namespace RVMS.Services.Services
{
    [ServiceContract]
    public class Stajalista
    {
        private readonly StajalistaRepository fStajalistaRepository = new StajalistaRepository();

        private readonly MedjustanicnaRastojanjaRepository fMedjustanicnaRastojanjaRepository = new MedjustanicnaRastojanjaRepository();

         [OperationContract]
         public StajalisteDTO[] VratiStajalista(int? idOpstine, int? idMesta)
         {
             return fStajalistaRepository.VratiStajalista(idOpstine, idMesta, true).Select(
                 x => new StajalisteDTO()
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

         [OperationContract]
         public StajalisteDTO[] VratiSusednaStajalista(int idStajalista)
         {
             var stajalista = new List<StajalisteDTO>();
             var medjustanicnaRastojanja = fMedjustanicnaRastojanjaRepository.VratiMedjustanicnaRastojanja(idStajalista);
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
    }
}