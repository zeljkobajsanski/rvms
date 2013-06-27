using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class RelacijeRepository : Repository<Relacija>
    {
        public IEnumerable<Relacija> VratiRelacije(int tipStajalista, int? idStajalista)
        {
            var relacije = fDataContext.Relacije.Include("MedjustanicnaRastojanja").Where(x => x.Aktivan);
            if (idStajalista.HasValue)
            {
                switch (tipStajalista)
                {
                    case 1:
                        relacije = relacije.Where(
                        x =>
                        x.MedjustanicnaRastojanja.Any(s => s.PolaznoStajalisteId == idStajalista));
                        break;
                    case 2:
                        relacije = relacije.Where(
                        x =>
                        x.MedjustanicnaRastojanja.Any(s => s.DolaznoStajalisteId == idStajalista));
                        break;
                }
                
            }
             return relacije.ToArray();
         }

        public Relacija VratiRelacijuSaRastojanjima(int idRelacije)
        {
            return fDataContext.Relacije.Include("MedjustanicnaRastojanja")
                .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                .SingleOrDefault(x => x.Id == idRelacije);
        }

        public bool PostojiRelacijaSaStajalistem(int idStajalista)
        {
            return
                fDataContext.Daljinar.Any(
                    x => x.PolaznoStajalisteId == idStajalista || x.DolaznoStajalisteId == idStajalista);
        }
    }
}