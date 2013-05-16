using System.Collections.Generic;
using System.Data;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class StajalistaRepository : Repository<Stajaliste>
    {
        public IEnumerable<Stajaliste> VratiStajalista(int idOpstine, int? idMesta)
        {
            var stajalista = fDataContext.Stajalista.Where(x => x.OpstinaId == idOpstine);
            if (idMesta.HasValue)
            {
                stajalista = stajalista.Where(x => x.MestoId == idMesta);
            }
            return stajalista.ToArray();
        }

        public IEnumerable<Stajaliste> VratiAktivnaStajalista()
        {
            return fDataContext.Stajalista.Include("Opstina").Where(x => x.Aktivan).ToArray();
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
    }
}