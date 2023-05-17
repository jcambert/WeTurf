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
        CreateMap<Predicted, PredictedDto>()
            .ReverseMap();
        CreateMap<Predicted, PredictedOnlyDto>();
        CreateMap<Resultat, ResultatDto>().ReverseMap();
        CreateMap<ResultatOfPredicted, ResultatOfPredictedDto>().ReverseMap();
        CreateMap<ResultatOfPredicted, ResultatOfPredictedStatisticalDto>();
        CreateMap<PredictionPerClassifier, PredictionPerClassifierDto>().ReverseMap();
        CreateMap<ResultatPerClassifier, ResultatPerClassifierDto>().ReverseMap();
        CreateMap<ScrapTrigger, ScrapTriggerDto>().ReverseMap();
        CreateMap<LastScrapped, LastScrappedDto>().ReverseMap();
        CreateMap<AccuracyPerClassifier, AccuracyPerClassifierDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<ProgrammeCourse, ProgrammeCourseDto>().ReverseMap();
        CreateMap<ProgrammeReunion, ProgrammeReunionDto>().ReverseMap();
        CreateMap<ToPredict, ToPredictDto>().ReverseMap();
        CreateMap<Parameter, ParameterDto>().ReverseMap();
        CreateMap<Classifier, ClassifierDto>().ReverseMap();
    }
}
