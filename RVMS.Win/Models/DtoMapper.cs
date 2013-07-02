using AutoMapper;
using Bootstrap.AutoMapper;
using RVMS.Model.DTO;

namespace RVMS.Win.Models
{
    public class DtoMapper : IMapCreator
    {
        public void CreateMap(IProfileExpression mapper)
        {
            mapper.CreateMap<StajalisteLinijeDTO, StajalisteLinije>()
                .ForMember(x => x.PrimaPutnike, opt => opt.MapFrom(x => x.Aktivan));
            mapper.CreateMap<StajalisteLinije, StajalisteLinijeDTO>()
                .ForMember(x => x.Aktivan, opt => opt.MapFrom(x => x.PrimaPutnike));
        }

        public static StajalisteLinije Map(StajalisteLinijeDTO stajalisteLinijeDto)
        {
            return Mapper.Map<StajalisteLinije>(stajalisteLinijeDto);
        }

        public static StajalisteLinijeDTO Map(StajalisteLinije stajalisteLinije)
        {
            return Mapper.Map<StajalisteLinijeDTO>(stajalisteLinije);
        }
    }
}