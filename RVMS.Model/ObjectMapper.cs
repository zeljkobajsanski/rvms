using AutoMapper;
using RVMS.Model.DTO;
using RVMS.Model.Entities;

namespace RVMS.Model
{
    public class ObjectMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<Linija, LinijaDTO>()
                .ForMember(x => x.Stajalista, opt => opt.MapFrom(x => x.Stajalista));
            CreateMap<Stajaliste, StajalisteDTO>();
            CreateMap<StajalisteLinije, StajalisteLinijeDTO>()
                .ForMember(x => x.NazivStajalista, opt => opt.MapFrom(x => x.Stajaliste.Naziv))
                .ForMember(x => x.Latituda, opt => opt.MapFrom(x => x.Stajaliste.GpsLatituda))
                .ForMember(x => x.Longituda, opt => opt.MapFrom(x => x.Stajaliste.GpsLongituda));
            CreateMap<Relacija, RelacijaDTO>();
        }

        public static LinijaDTO Map(Linija linija)
        {
            return Mapper.Map<LinijaDTO>(linija);
        }

        public static StajalisteDTO Map(Stajaliste stajaliste)
        {
            return Mapper.Map<StajalisteDTO>(stajaliste);
        }

        public static RelacijaDTO Map(Relacija relacija)
        {
            return Mapper.Map<RelacijaDTO>(relacija);
        }
    }
}