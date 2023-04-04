using System.Linq;
using System.Text.RegularExpressions;
using We.Utilities;

namespace We.Turf.Entities;

public class ProgrammeCourseDto:EntityDto<Guid>
{
    static Regex regex = new Regex("[0-9][0-9]*", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public string Discipline { get; set; }
    public string Libelle { get; set; }
    public int Distance { get; set; }

    public string DistanceUnite { get; set; }
    public int NombrePartants { get; set; }
    public string OrdreArrivee { get; set; }
    public string Hippodrome { get; set; }

    public int[] Arrivee=>regex.Matches(OrdreArrivee).ToIntArray();
}
