using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class LinijeRepository : Repository<Linija>
    {
        public IEnumerable<StajalisteLinije> VratiStajalistaLinije(int idLinije)
        {
            return fDataContext.Linije.Include("Stajalista").Include("Stajalista.Stajaliste").Single(x => x.Id == idLinije).Stajalista.OrderBy(x => x.Rbr).ToArray();
        }

        public Linija UcitajLinijuIStajalista(int id)
        {
            return fDataContext.Linije.Include("Stajalista").Include("Stajalista.Stajaliste").SingleOrDefault(x => x.Id == id);
        }

        public int? VratiIdPoslednjegStajalistaNaLiniji(int idLinije)
        {
            if (idLinije == 0) return null;
            var stajalista = fDataContext.Linije.Include("Stajalista").Single(x => x.Id == idLinije).Stajalista.ToArray();
            if (!stajalista.Any()) return null;
            return stajalista.Last().StajalisteId;
        }
    }
}