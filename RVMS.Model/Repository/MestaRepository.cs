using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class MestaRepository : Repository<Mesto>
    {
         public IEnumerable<Mesto> VratiMestaOpstine(int idOpstine)
         {
             return fDataContext.Mesta.Where(x => x.OpstinaId == idOpstine).ToArray();
         } 
    }
}