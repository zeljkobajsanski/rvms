using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class MestaRepository : Repository<Mesto>
    {
         public IQueryable<Mesto> VratiMestaOpstine(int? idOpstine)
         {
             var mesta = GetActive();
             if (idOpstine.HasValue)
             {
                 mesta = mesta.Where(x => x.OpstinaId == idOpstine);
             }
             return mesta;
         } 
    }
}