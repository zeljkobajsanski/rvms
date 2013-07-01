using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class Repositories : IRepositories
    {
        private readonly DataContext fDataContext = new DataContext();

        public ILinijeRepository LinijeRepository {get {return new LinijeRepository(fDataContext);}}

        public IMedjustanicnaRastojanjaRepository MedjustanicnaRastojanjaRepository {get {return new MedjustanicnaRastojanjaRepository(fDataContext);}}

        public IMestaRepository MestaRepository {get {return new MestaRepository(fDataContext);}}

        public IPrevozniciRepository PrevozniciRepository {get {return new PrevozniciRepository(fDataContext);}}

        public IRelacijeRepository RelacijeRepository {get {return new RelacijeRepository(fDataContext);}}

        public IStajalistaRepository StajalistaRepository {get {return new StajalistaRepository(fDataContext);}}

        public IStajalistaLinijeRepository StajalistaLinijeRepository {get {return new StajalistaLinijeRepository(fDataContext);}}

        public void Save()
        {
            fDataContext.SaveChanges();
        }
    }
}