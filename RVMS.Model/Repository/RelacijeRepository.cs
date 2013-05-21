using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class RelacijeRepository : Repository<Relacija>
    {
         public IEnumerable<Relacija> VratiRelacije()
         {
             return fDataContext.Relacije.Include("MedjustanicnaRastojanja").Where(x => x.Aktivan).ToArray();
         }

        public Relacija VratiRelacijuSaRastojanjima(int idRelacije)
        {
            return fDataContext.Relacije.Include("MedjustanicnaRastojanja")
                .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                .SingleOrDefault(x => x.Id == idRelacije);
        }
    }
}