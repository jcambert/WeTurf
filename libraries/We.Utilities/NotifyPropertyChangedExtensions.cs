using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;

namespace We.Utilities;

public static class NotifyPropertyChangedExtensions
{
    public static IObservable<EventPattern< PropertyChangedEventArgs>> WhenPropertyChanged(this INotifyPropertyChanged notifyPropertyChanged)
    {
        var res= Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
          ev => notifyPropertyChanged.PropertyChanged += ev,
          ev => notifyPropertyChanged.PropertyChanged -= ev);
        return res;
    }
}
