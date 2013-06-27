using System.Collections.Generic;
using System.Data;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class StajalistaRepository : Repository<Stajaliste>
    {
        public IQueryable<Stajaliste> VratiStajalista(int? idOpstine, int? idMesta, bool includeParents = false)
        {
            var set = fDataContext.Stajalista;
            if (includeParents)
            {
                set.Include("Opstina");
                set.Include("Mesto");
            }
            var stajalista = set.Where(x => x.Aktivan);
            if (idOpstine.HasValue)
            {
                stajalista = stajalista.Where(x => x.OpstinaId == idOpstine);
            }
            if (idMesta.HasValue)
            {
                stajalista = stajalista.Where(x => x.MestoId == idMesta);
            }
            return stajalista;
        }

        public IEnumerable<Stajaliste> VratiAktivnaStajalista()
        {
            return fDataContext.Stajalista.Include("Opstina").Where(x => x.Aktivan).ToArray();
        }

        public Stajaliste VratiStajalisteIOpstinu(int idStajalista)
        {
            return fDataContext.Stajalista.Include("Opstina").SingleOrDefault(x => x.Id == idStajalista);
        }

        public IEnumerable<Stajaliste> PretraziStajalista(int? idOpstine, int? idMesta, string nazivStajalista)
        {
            var stajalista = fDataContext.Stajalista.Include("Opstina").Include("Mesto").Where(x => x.Aktivan);
            if (idOpstine.HasValue)
            {
                stajalista = stajalista.Where(x => x.OpstinaId == idOpstine);
            }
            if (idMesta.HasValue)
            {
                stajalista = stajalista.Where(x => x.MestoId == idMesta);
            }
            if (!string.IsNullOrEmpty(nazivStajalista))
            {
                stajalista = stajalista.Where(x => x.Naziv.Contains(nazivStajalista));
            }
            return stajalista.OrderBy(x => x.Naziv).ToArray();
        }

        public IQueryable<Stajaliste> VratiStajalistaOpstine(int? idOpstine)
        {
            var stajalista = fDataContext.Stajalista.Include("Opstina").Where(x => x.Aktivan);
            if (idOpstine.HasValue)
            {
                stajalista = stajalista.Where(x => x.OpstinaId == idOpstine);
            }
            return stajalista;
        }
    }
}