using System.Text.RegularExpressions;
using We.Utilities;

namespace We.Turf.Entities;

[Serializable]
public partial class ProgrammeCourseDto:EntityDto<Guid>
{
    static readonly Regex regex = MyRegex();
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public string? Discipline { get; set; }
    public string? Libelle { get; set; }
    public int Distance { get; set; }

    public string? DistanceUnite { get; set; }
    public int NombrePartants { get; set; }
    public string? OrdreArrivee { get; set; }
    public string? Hippodrome { get; set; }

    public int[] Arrivee=>regex.Matches(OrdreArrivee??string.Empty).ToIntArray();

    [GeneratedRegex("[0-9][0-9]*", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace, "fr-FR")]
    private static partial Regex MyRegex();
}
