using System;
using Volo.Abp.Domain.Entities;
using We.Csv;

namespace We.Turf.Entities;

public class ToPredict : Entity<Guid>
{
    [CsvField(0)]
    public DateOnly Date { get; set; }

    [CsvField(1)]
    public int Reunion { get; set; }

    [CsvField(2)]
    public int Course { get; set; }

    [CsvField(3)]
    public string? HippoCode { get; set; }

    [CsvField(4)]
    public string? HippoNom { get; set; }

    [CsvField(5)]
    public string? Nom { get; set; }

    [CsvField(6)]
    public int NumeroPmu { get; set; }

    [CsvField(7)]
    public double Rapport { get; set; }

    [CsvField(8)]
    public int Age { get; set; }

    [CsvField(9)]
    public string? Sexe { get; set; }

    [CsvField(10)]
    public string? Race { get; set; }

    [CsvField(11)]
    public string? Statut { get; set; }

    [CsvField(12)]
    public string? Oeilleres { get; set; }

    [CsvField(13)]
    public string? Deferre { get; set; }

    [CsvField(14)]
    public string? IndicateurInedit { get; set; }

    [CsvField(15)]
    public string? Musique { get; set; }

    [CsvField(16)]
    public int NombreCourses { get; set; }

    [CsvField(17)]
    public int NombreVictoires { get; set; }

    [CsvField(18)]
    public int NombrePlaces { get; set; }

    [CsvField(19)]
    public int NombrePlacesSecond { get; set; }

    [CsvField(20)]
    public int NombrePlacesTroisieme { get; set; }

    [CsvField(21)]
    public int OrdreArrivee { get; set; }

    [CsvField(22)]
    public int Distance { get; set; }

    [CsvField(23)]
    public int HadicapDistance { get; set; }

    [CsvField(24)]
    public int GainsCarriere { get; set; }

    [CsvField(25)]
    public int GainsVictoires { get; set; }

    [CsvField(26)]
    public int GainsPlaces { get; set; }

    [CsvField(27)]
    public int GainsAnneeEnCours { get; set; }

    [CsvField(28)]
    public int GainsAnneePrecedente { get; set; }

    [CsvField(29)]
    public int PlaceCorde { get; set; }

    [CsvField(30)]
    public int HadicapValeur { get; set; }

    [CsvField(31)]
    public int HandicapPoids { get; set; }
}
