using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class MedjustanicnaRastojanjaRepository : Repository<MedjustanicnoRastojanje>
    {
        public IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanja(int idRelacije)
        {
            return fDataContext.Relacije.Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                                        .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                                        .Single(x => x.Id == idRelacije).MedjustanicnaRastojanja;
        }

        public IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanja(int? polaznoStajaliste)
        {
            return fDataContext.Daljinar.Include("PolaznoStajaliste")
                                        .Include("PolaznoStajaliste.Opstina")
                                        .Include("DolaznoStajaliste")
                                        .Include("DolaznoStajaliste.Opstina")
                                        .Where(x => x.PolaznoStajalisteId == polaznoStajaliste || x.DolaznoStajalisteId == polaznoStajaliste).ToArray();
        }

        public decimal? VratiMedjustanicnaRastojanja(int odStajalista, int doStajalista)
        {
            var msr =
                fDataContext.Daljinar.SingleOrDefault(x => 
                    (x.PolaznoStajalisteId == odStajalista && x.DolaznoStajalisteId == doStajalista) || 
                    (x.PolaznoStajalisteId == doStajalista && x.DolaznoStajalisteId == odStajalista));
            return msr != null ? msr.Rastojanje : (decimal?)null;
        }

        public IQueryable<MedjustanicnoRastojanje> VratiSvaMedjustanicnaRastojanjaSaStajalistem(int idStajalista)
        {
            return
                fDataContext.Daljinar.Where(
                    x => x.PolaznoStajalisteId == idStajalista || x.DolaznoStajalisteId == idStajalista);
        }
    }
}