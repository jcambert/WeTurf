namespace We.Turf.Entities;


public class ToPredictDto : EntityDto<Guid>
{
    //date;reunion;course;
    ///hippo_code;hippo_nom;nom;numPmu;rapport;
    ///age;sexe;race;statut;oeilleres;
    ///deferre;indicateurInedit;musique;
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public string? HippoCode { get; set; }
    public string? HippoNom { get; set; }
    public string? Nom { get; set; }
    public int NumeroPmu { get; set; }
    public double Rapport { get; set; }
    public int Age { get; set; }
    public string? Sexe { get; set; }
    public string? Race { get; set; }
    public string? Statut { get; set; }
    public string? Oeilleres { get; set; }
    public string? Deferre { get; set; }
    public string? IndicateurInedit { get; set; }
    public string? Musique { get; set; }

    ///nombreCourses;nombreVictoires;nombrePlaces;nombrePlacesSecond;nombrePlacesTroisieme;ordreArrivee;
    ///distance;handicapDistance;gain_carriere;gain_victoires;gain_places;gain_annee_en_cours;gain_annee_precedente;
    ///placeCorde;handicapValeur;handicapPoids
    public int NombreCourses { get; set; }
    public int NombreVictoires { get; set; }
    public int NombrePlaces { get; set; }
    public int NombrePlacesSecond { get; set; }
    public int NombrePlacesTroisieme { get; set; }
    public int OrdreArrivee { get; set; }
    public int Distance { get; set; }
    public int HadicapDistance { get; set; }
    public int GainsCarriere { get; set; }
    public int GainsVictoires { get; set; }
    public int GainsPlaces { get; set; }
    public int GainsAnneeEnCours { get; set; }
    public int GainsAnneePrecedente { get; set; }
    public int PlaceCorde { get; set; }
    public int HadicapValeur { get; set; }
    public int HandicapPoids { get; set; }
}
