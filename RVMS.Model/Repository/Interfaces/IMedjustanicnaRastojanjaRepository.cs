using System.Collections.Generic;
using System.Linq;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IMedjustanicnaRastojanjaRepository : IRepository<MedjustanicnoRastojanje>
    {
        IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanjaNaRelaciji(int idRelacije);
        IEnumerable<MedjustanicnoRastojanje> VratiMedjustanicnaRastojanja(int? polaznoStajaliste);
        decimal? VratiMedjustanicnaRastojanja(int odStajalista, int doStajalista);
        IQueryable<MedjustanicnoRastojanje> VratiSvaMedjustanicnaRastojanjaSaStajalistem(int idStajalista);
    }
}