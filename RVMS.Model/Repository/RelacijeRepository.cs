using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class RelacijeRepository : Repository<Relacija>, IRelacijeRepository
    {
        public RelacijeRepository()
        {
        }

        public RelacijeRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Relacija> VratiRelacije(int tipStajalista, int? idStajalista)
        {
            var relacije = DataContext.Relacije.Include("MedjustanicnaRastojanja").Where(x => x.Aktivan);
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
             return relacije;
         }

        public IQueryable<Relacija> VratiRelacijeSaRastojanjima()
        {
            return DataContext.Relacije.Include("MedjustanicnaRastojanja")
                                       .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                                       .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                                       .Where(x => x.Aktivan);
        }

        public IQueryable<Relacija> VratiRelacijeKojePolazeSaStanice(int idStajalista)
        {
            return DataContext.Relacije.Include("MedjustanicnaRastojanja")
                                        .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                                        .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                                        .Where(x => x.MedjustanicnaRastojanja.Any(m => m.PolaznoStajalisteId == idStajalista && m.Rbr == 1));
        }

        public IQueryable<Relacija> VratiRelacijeKojeProlazeKrozStanicu(int idStajalista)
        {
            return DataContext.Relacije.Include("MedjustanicnaRastojanja")
                                        .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                                        .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                                        .Where(x => x.MedjustanicnaRastojanja.Any(m => m.PolaznoStajalisteId == idStajalista));
        }

        public Relacija VratiRelacijuSaRastojanjima(int idRelacije)
        {
            return DataContext.Relacije.Include("MedjustanicnaRastojanja")
                .Include("MedjustanicnaRastojanja.PolaznoStajaliste")
                .Include("MedjustanicnaRastojanja.DolaznoStajaliste")
                .SingleOrDefault(x => x.Id == idRelacije);
        }

        public bool PostojiRelacijaSaStajalistem(int idStajalista)
        {
            return
                DataContext.Daljinar.Any(
                    x => x.PolaznoStajalisteId == idStajalista || x.DolaznoStajalisteId == idStajalista);
        }
    }
}