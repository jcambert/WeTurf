using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;

namespace We.Utilities;

public static class NotifyPropertyChangedExtensions
{
    /// <summary>
    /// Reactive notification handler when a property changed
    /// </summary>
    /// <param name="notifyPropertyChanged"></param>
    /// <returns></returns>
    public static IObservable<EventPattern<PropertyChangedEventArgs>> WhenPropertyChanged(
        this INotifyPropertyChanged notifyPropertyChanged
    )
    {
        var res = Observable.FromEventPattern<
            PropertyChangedEventHandler,
            PropertyChangedEventArgs
        >(
            ev => notifyPropertyChanged.PropertyChanged += ev,
            ev => notifyPropertyChanged.PropertyChanged -= ev
        );
        return res;
    }
}
