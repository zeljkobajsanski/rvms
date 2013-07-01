using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class MedjustanicnaRastojanjaRepository : Repository<MedjustanicnoRastojanje>, IMedjustanicnaRastojanjaRepository
    {
        public MedjustanicnaRastojanjaRepository()
        {
        }

        public MedjustanicnaRastojanjaRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanjaNaRelaciji(int idRelacije)
        {
            return DataContext.Relacije.Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                                        .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                                        .Single(x => x.Id == idRelacije).MedjustanicnaRastojanja;
        }

        public IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanja(int? polaznoStajaliste)
        {
            return DataContext.Daljinar.Include("PolaznoStajaliste")
                                        .Include("PolaznoStajaliste.Opstina")
                                        .Include("DolaznoStajaliste")
                                        .Include("DolaznoStajaliste.Opstina")
                                        .Where(x => x.PolaznoStajalisteId == polaznoStajaliste || x.DolaznoStajalisteId == polaznoStajaliste).ToArray();
        }

        public decimal? VratiMedjustanicnaRastojanja(int odStajalista, int doStajalista)
        {
            var msr =
                DataContext.Daljinar.SingleOrDefault(x => 
                    (x.PolaznoStajalisteId == odStajalista && x.DolaznoStajalisteId == doStajalista) || 
                    (x.PolaznoStajalisteId == doStajalista && x.DolaznoStajalisteId == odStajalista));
            return msr != null ? msr.Rastojanje : (decimal?)null;
        }

        public IQueryable<MedjustanicnoRastojanje> VratiSvaMedjustanicnaRastojanjaSaStajalistem(int idStajalista)
        {
            return
                DataContext.Daljinar.Where(
                    x => x.PolaznoStajalisteId == idStajalista || x.DolaznoStajalisteId == idStajalista);
        }
    }
}