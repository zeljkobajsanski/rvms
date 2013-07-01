using RVMS.Model.Entities;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class StajalistaLinijeRepository : Repository<StajalisteLinije>, IStajalistaLinijeRepository
    {
        public StajalistaLinijeRepository()
        {
        }

        public StajalistaLinijeRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}