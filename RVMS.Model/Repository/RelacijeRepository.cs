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
    }
}