namespace We.Turf.Entities;

public record ResultatOfPredictedCountByReunionCourse(
    DateOnly Date,
    string Classifier,
    int Reunion,
    int Course,
    int NombreDePrediction
);
