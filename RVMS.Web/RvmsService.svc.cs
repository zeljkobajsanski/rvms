using System;
using System.Collections.Generic;
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
            return new Model.Repository.Repository<Opstina>().GetActive().ToArray();
        }

        public StajalisteDTO[] VratiStajalisteOpstine(int? idOpstine)
        {
            return new StajalistaRepository().VratiStajalistaOpstine(idOpstine).Select(x => new StajalisteDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Opstina = x.Opstina.NazivOpstine
            }).ToArray();
        }

        public RelacijaDTO[] VratiDaljinar()
        {
            return new RelacijeRepository().VratiRelacije().Select(x => new RelacijaDTO()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                DuzinaRelacije = x.DuzinaRelacije,
                VremeVoznje = x.VremeVoznje,
                SrednjaSaobracajnaBrzina = x.SrednjaSaobracajnaBrzina
            }).ToArray();
        }
    }
}
