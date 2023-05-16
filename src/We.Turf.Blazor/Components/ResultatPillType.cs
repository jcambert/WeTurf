using System.Diagnostics;

namespace We.Turf.Blazor.Components;

public enum ResultatPillType
{
    Arrivee,
    Prediction
}

[DebuggerDisplay("{NumPmu}-{Count}-{DividendePlace}")]
public sealed record ResultatPlace(int NumPmu, int Count, double DividendePlace);
