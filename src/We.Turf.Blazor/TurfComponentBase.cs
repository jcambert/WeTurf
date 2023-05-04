using System.ComponentModel;
using System.Runtime.CompilerServices;
using Volo.Abp.AspNetCore.Components;
using We.Turf.Localization;

namespace We.Turf.Blazor;

public abstract class TurfComponentBase : AbpComponentBase, INotifyPropertyChanged
{
    protected TurfComponentBase()
    {
        LocalizationResource = typeof(TurfResource);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
