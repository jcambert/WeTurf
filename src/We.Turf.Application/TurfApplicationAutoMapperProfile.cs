using AutoMapper;
using We.Turf.Entities;

namespace We.Turf;

public class TurfApplicationAutoMapperProfile : Profile
{
    public TurfApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Predicted, PredictedDto>().ReverseMap();
        CreateMap<Resultat, ResultatDto>().ReverseMap();
        CreateMap<ResultatOfPredicted, ResultatOfPredictedDto>().ReverseMap();  
    }
}
