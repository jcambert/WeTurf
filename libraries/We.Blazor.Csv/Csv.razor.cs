using Microsoft.AspNetCore.Components;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using We.Utilities;
using System.Reactive.Linq;

namespace We.Blazor.Csv;

public partial class Csv : INotifyPropertyChanged, IDisposable
{
    #region CTOR
    public Csv()
    {
        _whenContentChanged = this.WhenPropertyChanged()
            .Where(e => e.EventArgs.PropertyName == nameof(Content))
            .Select(e => Content)
            .Subscribe(content => ContentUpdated());
    }
    #endregion

    #region private Vars
    private string _internalContent;
    private int[] ColumnIndexes;
    private List<Row> Rows;
    private IDisposable _whenContentChanged;
    private int Count => Lines?.Count ?? 0;
    private Row Header
    {
        get
        {
            if (!HasHeader || Count == 0)
                return default;
            return Rows[0];
        }
    }
    private List<Row> Lines
    {
        get
        {
            if (Rows is null)
                return default;
            int skip = HasHeader ? 1 : 0;
            if (LimitRow is null)
                return Rows.Skip(skip).ToList();
            return Rows.Skip(skip).Take(LimitRow ?? 0).ToList();
        }
    }
    #endregion

    #region private methods

    private void ContentUpdated()
    {
        string[] lines = new string[0];
        if (!string.IsNullOrEmpty(Content))
            lines = Content.Split("\n");

        if (lines.Length > 0)
            Rows = lines.Select(line => new Row(line, Separator)).ToList();
        if (HasHeader && ColumnNames is not null)
        {
            ColumnIndexes = Header
                ?.Columns.Select((value, index) => new { Index = index, Value = value })
                .Where(x => ColumnNames.Contains(x.Value))
                .Select(x => x.Index)
                .ToArray();
        }
    }

    private IEnumerable<string> FilterColmun(Row row)
    {
        if (LimitColumn is null && ColumnNames is null)
            return row.Columns;
        if (ColumnNames is null)
            return row.Columns.Take(LimitColumn ?? 0);
        if (HasHeader && ColumnNames is not null)
            return row.Columns.TakeOnly(ColumnIndexes);
        return row.Columns;
    }
    #endregion

    #region public Parameter Properties
#pragma warning disable BL0007

    [Parameter]
    public string Content
    {
        get => _internalContent;
        set { SetAndRaise(ref _internalContent, value); }
    }

#pragma warning restore BL0007

    [Parameter]
    public bool HasHeader { get; set; }

    [Parameter]
    public string Separator { get; set; } = ";";

    [Parameter]
    public int? LimitColumn { get; set; } = null;

    [Parameter]
    public int? LimitRow { get; set; } = null;

    [Parameter]
    public List<string> ColumnNames { get; set; }
    #endregion

    #region NotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    private void SetAndRaise<T>(ref T dest, T value, [CallerMemberName] string propertyName = "")
    {
        if (dest is null & value is null)
            return;
        bool raise = !value.Equals(dest);
        dest = value;
        NotifyPropertyChanged(propertyName);
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


    #region IDisposable

    public void Dispose()
    {
        _whenContentChanged?.Dispose();
    }

    #endregion

    internal class Row : IEnumerable
    {
        internal Row(string line, string sep)
        {
            this.Columns = line.Split(sep);
        }

        internal string[] Columns { get; private set; }

        internal string this[int index] => Columns[index];

        internal int Count => Columns.Count();

        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < Count; index++)
            {
                // Yield each day of the week.
                yield return this[index];
            }
        }
    }
}
