using Blazorise.Charts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Reactive.Linq;
using Volo.Abp.AspNetCore.Components.Notifications;
using We.Blazor;
using We.Results;
using We.Turf.Blazor.Components;
using We.Turf.Entities;
using We.Turf.Queries;
using We.Utilities;

namespace We.Turf.Blazor.Pages;

// TODO: Charger les resultats de la course du 08/01/2023 R2C10 et verifier les valeurs. C'est une course internationale
// https://www.pmu.fr/turf/08012023/r2/c10/ voir resulat et json
public partial class Index : WeComponentBase
{
    #region private vars


    #endregion

#pragma warning disable CS8618
    #region CTOR
    public Index() : base() { }
    #endregion
    [Inject]
    IPmuServiceAppService PmuService { get; set; }

    [Inject]
    IPmuStatAppService PmuStatService { get; set; }

    [Inject]
    IUiNotificationService Notification { get; set; }
#pragma warning restore CS8618

    private const string ALL_CLASSIFIER = "tous";
    private DateOnly? _date;
    private DateOnly? Today;
    private int _currentClassifier,
        _indiceConfiance;
    private TypePari _currentPari = TypePari.Place;

    private SelectMode? _mode;
    private bool ShowProgrammeCourse => _mode == SelectMode.Course;
    private bool ShowProgrammeReunion => _mode == SelectMode.Reunion;
    private List<PredictedDto> Predicteds { get; set; } = new();
    private List<ProgrammeCourseDto> ProgrammeCourses { get; set; } = new();
    private List<ProgrammeReunionDto> ProgrammeReunions { get; set; } = new();
    private List<ResultatDto> Resultats { get; set; } = new();
    private List<ResultatOfPredictedDto> ResultatOfPredicteds { get; set; } = new();
    private List<ResultatOfPredictedStatisticalDto> ResultatOfPredictedStatistical { get; set; } =
        new();
    private List<PredictedOnlyDto> PredictionsOnly { get; set; } = new();
    private List<AccuracyPerClassifierDto> Classifiers { get; set; } = new();

    private Dictionary<(int, int), IEnumerable<ResultatPlace>> ResultatsOfPredicteds = new();

    public SommeDesMises SommeDesMises = new(0, 0.0);
    private IDisposable? _whenDateChangedToToday,
        _whenDateChangeBeforeToday,
        _whenClassifierChanged,
        _whenPariChanged;

    protected override void OnInitialized()
    {
        _whenClassifierChanged = this.WhenPropertyChanged
            .Where(e => e.EventArgs.PropertyName == nameof(CurrentClassifier))
            .Select(e => (_date, _currentClassifier))
            .Subscribe(
                async item =>
                {
                    await LoadPredictionAsync(item._date);
                    await LoadResultatOfPredicteds(item._date);
                    await LoadPredictedOnly(item._date);
                    await InternalInitializationAsync();
                    await InvokeAsync(StateHasChanged);
                }
            );

        _whenDateChangedToToday = this.WhenPropertyChanged
            .Where(e => e.EventArgs.PropertyName == nameof(CurrentDate))
            .Select(e => _date)
            .Where(d => d == Today)
            .Subscribe(
                async date =>
                {
                    await LoadClassifiersAsync();
                    await LoadPredictionAsync(date);
                }
            );

        _whenDateChangeBeforeToday = this.WhenPropertyChanged
            .Where(e => e.EventArgs.PropertyName == nameof(CurrentDate))
            .Select(e => _date)
            .Where(d => d < Today)
            .Subscribe(
                async date =>
                {
                    await LoadProgammeCourseAsync(date);
                    await LoadProgammeReunionAsync(date);
                    await LoadResultats(date);
                    await LoadClassifiersAsync();
                    await LoadPredictionAsync(date);
                    await LoadResultatOfPredicteds(date);
                    await LoadPredictedOnly(date);

                    await InternalInitializationAsync();

                    await InvokeAsync(StateHasChanged);
                }
            );

        _whenPariChanged = this.WhenPropertyChanged
            .Where(e => e.EventArgs.PropertyName == nameof(CurrentPari))
            .Select(e => (_date, _currentPari))
            .Subscribe(
                async item =>
                {
                    await LoadResultatOfPredicteds(item._date);
                    await InternalInitializationAsync();

                    await InvokeAsync(StateHasChanged);
                }
            );
        this.WhenPropertyChanged
            .Where(e => e.EventArgs.PropertyName == "IndiceConfiance")
            .Select(e => IndiceConfiance)
            .Subscribe(
                async i =>
                {
                    await InternalInitializationAsync();

                    await InvokeAsync(StateHasChanged);
                }
            );
        ShowCourseOrReunion(SelectMode.Course);
        Today = DateOnly.FromDateTime(DateTime.Now);
        NotifyPropertyChanged("CurrentDate");
    }

    protected async Task InternalInitializationAsync()
    {
        ResultatsOfPredicteds.Clear();
        foreach (var course in ProgrammeCourses.OrderBy(x => x.Reunion).ThenBy(x => x.Course))
        {
            ResultatsOfPredicteds[(course.Reunion, course.Course)] = GetResultats(course);
        }
        SommeDesMises = await CalculSommeDesMises();
        Labels = ProgrammeCourses
            .OrderBy(x => x.Reunion)
            .ThenBy(x => x.Course)
            .Select(x => $"R{x.Reunion}C{x.Course}")
            .ToArray();

        await HandleRedraw();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (!firstRender)
            await ScrollToCardAsync();
    }

    public string TargetId => $"r{Reunion}c{Course}";

    private async Task ScrollToCardAsync()
    {
        if (Reunion is not null && Course is not null)
            await JSRuntime.InvokeVoidAsync("scrollToElement", TargetId);
    }

    IEnumerable<PredictedDto> GetPredicted(ProgrammeCourseDto course) =>
        Predicteds
            .Where(x => x.Reunion == course.Reunion && x.Course == course.Course)
            .OrderBy(x => x.NumeroPmu);

    IEnumerable<ResultatPlace> GetResultats(ProgrammeCourseDto course)
    {
        var res =
            from resultat in GetPredicted(course)
            group resultat by resultat.NumeroPmu into gp
            select new ResultatPlace(gp.Key, gp.Count(), GetDividende(course, gp.Key));

        return res;
    }

    bool GetIsPlace(ProgrammeCourseDto course, int numeroPmu) =>
        course.Arrivee.Take(3).Contains(numeroPmu);

    bool GetIsPremier(ProgrammeCourseDto course, int numeroPmu) =>
        course.Arrivee.First() == numeroPmu;

    double GetDividende(ProgrammeCourseDto course, int numeroPmu)
    {
        /*   if(course.Date==new DateOnly(2023,1,8) && course.Reunion==8 && course.Course==3 && numeroPmu==5)
               Debugger.Break();*/
        var res = ResultatOfPredictedStatistical
            ?.Where(
                x =>
                    x.Reunion == course.Reunion
                    && x.Course == course.Course
                    && x.NumeroPmu == numeroPmu
                    && (
                        _currentPari == TypePari.Place
                            ? GetIsPlace(course, numeroPmu)
                            : GetIsPremier(course, numeroPmu)
                    )
            )
            .ToList();
        var res1 = res?.FirstOrDefault()?.Dividende ?? 0.0;

        return res1;
    }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = null!;

#pragma warning disable BL0007
    [Parameter]
    public string Date
    {
        get => _date?.ToShortDateString() ?? string.Empty;
        set
        {
            var res = value.TryParseToDateOnly(out var __date);
            if (!res)
            {
                __date = DateOnlyExtensions.Now;
            }
            CurrentDate = __date;
        }
    }
#pragma warning restore BL0007
    [Parameter]
    public int? Reunion { get; set; }

    [Parameter]
    public int? Course { get; set; }

    internal DateOnly? CurrentDate
    {
        get => _date;
        set
        {
            _date = value;
            NotifyPropertyChanged();
        }
    }

    public AccuracyPerClassifierDto CurrentClassifier
    {
        get => Classifiers[_currentClassifier];
        set
        {
            _currentClassifier = Classifiers.IndexOf(value);

            _indiceConfiance = 1;

            NotifyPropertyChanged();
        }
    }

    Task OnCurrentClassifierChanged(int selected)
    {
        CurrentClassifier = Classifiers[selected];
        return Task.CompletedTask;
    }

    public TypePari CurrentPari
    {
        get => _currentPari;
        set
        {
            _currentPari = value;
            NotifyPropertyChanged();
        }
    }

    Task OnCurrentPariChanged(TypePari selected)
    {
        CurrentPari = selected;
        return Task.CompletedTask;
    }

    public int IndiceConfiance
    {
        get => _indiceConfiance;
        set
        {
            _indiceConfiance = value;
            NotifyPropertyChanged();
        }
    }

    Task OnCurrentIndiceConfianceChanged(int selected)
    {
        IndiceConfiance = selected;
        return Task.CompletedTask;
    }

    private void SetDate(DateOnly d)
    {
        CurrentDate = d;
    }

    private void ShowCourseOrReunion(SelectMode mode)
    {
        _mode = mode;
        // ShowProgrammeCourse = mode == SelectMode.Course;
        //ShowProgrammeReunion = !ShowProgrammeCourse;
        StateHasChanged();
    }

    private async Task LoadClassifiersAsync()
    {
        if (Classifiers.Any())
            return;
        var t = PmuService.BrowseAccuracyOfClassifier(new BrowseAccuracyOfClassifierQuery());
        var (res, response, errors) = await t;
        if (res)
        {
            Classifiers = response.ClassifiersAccuracy;
            var c = new AccuracyPerClassifierDto()
            {
                Classifier = ALL_CLASSIFIER,
                Percentage = 1.0,
                PredictionCount = 1,
                ResultatCount = 1
            };
            Classifiers.Insert(0, c);
            _currentClassifier = 0;
        }
        else
        {
            await Notification.Warn(errors.AsString());
        }
    }

    private async Task LoadPredictedOnly(DateOnly? date)
    {
        string? classifier =
            CurrentClassifier.Classifier == ALL_CLASSIFIER ? null : CurrentClassifier.Classifier;
        var t = PmuService.BrowsePredictionOnly(new() { Date = date, Classifier = classifier, });
        var (res, response, errors) = await t;
        if (res)
        {
            PredictionsOnly = response.Predicteds;
        }
        else
        {
            await Notification.Warn(errors.AsString());
        }
    }

    private async Task LoadResultatOfPredicteds(DateOnly? date)
    {
        ResultatOfPredictedStatistical.Clear();
        string? classifier =
            CurrentClassifier.Classifier == ALL_CLASSIFIER ? null : CurrentClassifier.Classifier;
        var t = PmuService.BrowseResultatOfPredictedStatistical(
            new()
            {
                Date = date,
                Classifier = classifier,
                Pari = _currentPari
            }
        );
        var (res, response, errors) = await t;
        if (res)
        {
            ResultatOfPredictedStatistical = response.Resultats;
        }
        else
        {
            await Notification.Warn(errors.AsString());
        }
    }

    private async Task LoadResultats(DateOnly? date)
    {
        var t = PmuService.BrowseResultat(new() { Date = date });
        var (res, response, errors) = await t;
        if (res)
        {
            Resultats = response.Resultats;
        }
        else
        {
            await Notification.Warn(errors.AsString());
        }
    }

    private async Task LoadPredictionAsync(DateOnly? date)
    {
        string? classifier =
            CurrentClassifier.Classifier == ALL_CLASSIFIER ? null : CurrentClassifier.Classifier;
        var t = PmuService.BrowsePrediction(new() { Date = date, Classifier = classifier });
        var (res, response, errors) = await t;
        if (res)
        {
            Predicteds = response.Predicteds;
        }
        else
        {
            await Notification.Warn(errors.AsString());
        }
        //await InvokeAsync(StateHasChanged);
    }

    private async Task LoadProgammeCourseAsync(DateOnly? date)
    {
        if (date is null)
            return;
        var t = PmuService.BrowseProgrammeCourse(new() { Date = (DateOnly)date });
        var (res, response, errors) = await t;
        if (res)
            ProgrammeCourses = response.Programmes;
        else
            await Notification.Warn(errors.AsString());
    }

    private async Task LoadProgammeReunionAsync(DateOnly? date)
    {
        if (date is null)
            return;
        var t = PmuService.BrowseProgrammeReunion(new() { Date = (DateOnly)date });
        var (res, response, errors) = await t;
        if (res)
            ProgrammeReunions = response.Reunions;
        else
            await Notification.Warn(errors.AsString());
    }

    public async Task<SommeDesMises> CalculSommeDesMises()
    {
        /* double dividende = 0.0;
         int count = 0;
         foreach (var key in ResultatsOfPredicteds.Keys)
         {
             var r = ResultatsOfPredicteds[key].Where(x => x.Count >= IndiceConfiance).ToList();
             dividende += r.Sum(x => x.DividendePlace);
             count += r.Count();
         }
         return (count, dividende);*/
        if (CurrentDate == null)
        {
            await Notification.Warn("Je ne peux pas calculter la somme des mise. La date est null");
            return new SommeDesMises(0, 0.0);
        }
        var t = PmuStatService.GetSommeDesMise(
            (DateOnly)CurrentDate,
            (CurrentClassifier.Classifier == ALL_CLASSIFIER)
              ? string.Empty
              : CurrentClassifier.Classifier,
            CurrentPari
        );
        var (res, response, errors) = await t;
        if (res)
            return response;
        else
            await Notification.Warn(errors.AsString());
        return new SommeDesMises(0, 0.0);
    }

    #region Chart
    LineChart<double> lineChart = new();

    async Task HandleRedraw()
    {
        await lineChart.Clear();

        await lineChart.AddLabelsDatasetsAndUpdate(Labels, GetLineChartDataset());
    }

    LineChartDataset<double> GetLineChartDataset()
    {
        return new LineChartDataset<double>
        {
            Label = "€",
            Data = GetChartData(), // RandomizeData(),
            BackgroundColor = backgroundColors,
            BorderColor = borderColors,
            Fill = true,
            PointRadius = 3,
            CubicInterpolationMode = "monotone",
        };
    }

    string[] Labels = Array.Empty<string>();
    List<string> backgroundColors = new List<string>
    {
        ChartColor.FromRgba(255, 99, 132, 0.2f),
        ChartColor.FromRgba(54, 162, 235, 0.2f),
        ChartColor.FromRgba(255, 206, 86, 0.2f),
        ChartColor.FromRgba(75, 192, 192, 0.2f),
        ChartColor.FromRgba(153, 102, 255, 0.2f),
        ChartColor.FromRgba(255, 159, 64, 0.2f)
    };
    List<string> borderColors = new List<string>
    {
        ChartColor.FromRgba(255, 99, 132, 1f),
        ChartColor.FromRgba(54, 162, 235, 1f),
        ChartColor.FromRgba(255, 206, 86, 1f),
        ChartColor.FromRgba(75, 192, 192, 1f),
        ChartColor.FromRgba(153, 102, 255, 1f),
        ChartColor.FromRgba(255, 159, 64, 1f)
    };

    List<double> GetChartData()
    {
        if (!ResultatOfPredictedStatistical?.Any() ?? false)
            return new();
        Dictionary<(int, int), double> res = new();
        double cumul = 0;

        foreach (var key in ResultatsOfPredicteds.Keys)
        {
            var v = ResultatsOfPredicteds[key].Where(x => x.Count >= IndiceConfiance);

            cumul = cumul + v.Sum(x => x.DividendePlace) - v.Count();
            res[key] = cumul;
        }
        return res.Values.ToList();
    }
    #endregion

    #region IDisposable
    protected override void InternalDispose()
    {
        _whenDateChangedToToday?.Dispose();
        _whenDateChangeBeforeToday?.Dispose();
        _whenClassifierChanged?.Dispose();
        _whenPariChanged?.Dispose();
    }
    #endregion

}
