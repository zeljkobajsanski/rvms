using AutoMapper;
using RVMS.Model.DTO;
using RVMS.Model.Entities;

namespace RVMS.Model
{
    public class ObjectMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<Linija, LinijaDTO>();
            CreateMap<Stajaliste, StajalisteDTO>();
        }

        public static LinijaDTO Map(Linija linija)
        {
            return Mapper.Map<LinijaDTO>(linija);
        }
    }
}