namespace We.Turf.Blazor.Components;

public enum ResultatPillType
{
    Arrivee,
    Prediction
}

public sealed record ResultatPlace(int NumPmu, int Count, double DividendePlace);
