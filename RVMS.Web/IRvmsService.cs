using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RVMS.Model.DTO;
using RVMS.Model.Entities;

namespace RVMS.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRvmsService" in both code and config file together.
    [ServiceContract]
    public interface IRvmsService
    {
        [OperationContract]
        Opstina[] VratiOpstine();

        [OperationContract]
        StajalisteDTO[] VratiStajalisteOpstine(int? idOpstine);

        [OperationContract]
        RelacijaDTO[] VratiDaljinar();

        [OperationContract]
        RelacijaSaMedjustanicnimRastojanjimaDTO VratiRelacijuSaRastojanjima(int idRelacije);

        [OperationContract]
        int SacuvajRelaciju(Relacija relacija);

        [OperationContract]
        void ObrisiRelaciju(int idRelacije);

        [OperationContract]
        MedjustanicnoRastojanjeDTO[] SacuvajRastojanje(MedjustanicnoRastojanje rastojanje);

        [OperationContract]
        MedjustanicnoRastojanjeDTO[] ObrisiRastojanje(int id);

        [OperationContract]
        Mesto[] VratiMesta(int? idOpstine);

        [OperationContract]
        StajalisteDTO[] VratiStajalistaMestaIOpstine(int? idOpstine, int? idMesta);

        [OperationContract]
        int SacuvajStajaliste(Stajaliste stajaliste);

        [OperationContract]
        MedjustanicnoRastojanje VratiMedjustanicnoRastojanje(int id);

        [OperationContract]
        MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeDole(int idRelacije, int idMedjustanicnogRastojanja);

        [OperationContract]
        MedjustanicnoRastojanjeDTO[] PomeriMedjustanicnoRastojanjeGore(int idRelacije, int idMedjustanicnogRastojanja);
    }
}
