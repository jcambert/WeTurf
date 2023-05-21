using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using U = We.Utilities;

namespace We.Blazor;

public abstract class WeComponentBase : ComponentBase, IDisposable, INotifyPropertyChanged
{
    #region privates vars
    private bool disposedValue;

    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    public WeComponentBase() : base() { }

    #region INotifyPropertyChanged
    protected IObservable<EventPattern<PropertyChangedEventArgs>> WhenPropertyChanged =>
        U.NotifyPropertyChangedExtensions.WhenPropertyChanged(this);

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    #region IDisposable

    protected abstract void InternalDispose();

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                InternalDispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
