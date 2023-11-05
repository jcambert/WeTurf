using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We.Results;

public class ReactiveResult<T> :Result<T>, IObservable<Result<T>>, IDisposable
{
    private List<IObserver<Result<T>>> observers = new List<IObserver<Result<T>>>();

    private bool _set;
    private bool _completed;
    private bool disposedValue;

    public ReactiveResult() : base()
    {
    }

    public void Ok( T value)
    {
        if (_completed)
            throw new ReactiveResultException<T>(this, ReactiveResultException<T>.ALREADY_COMPLETED);
        if (!_set )
        {
            this.Value = value;
            this.IsSuccess = true;
            this.InternalPublish();
            _set = true;
        }
    }
    public void Fail(Exception ex)
    {
        if (_completed)
            throw new ReactiveResultException<T>(this, ReactiveResultException<T>.ALREADY_COMPLETED);
        if (!_set )
        {
            this.Value = default;
            this.IsSuccess = false;
            this.InternalError(ex);
            _set = true;
        }
    }

    public void Reset()
    {
        this._errors.Clear();
        _set=false;
    }
    public void Complete()
    {
        this.InternalComplete();
        _completed = true;
    }
    #region IObserbvable<T>
    public IDisposable Subscribe(IObserver<Result<T>> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }

        // Return a disposable that unsubscribes the observer when disposed.
        return new Subscription(this, observer);
    }

    protected void InternalPublish()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this.Value);
        }
    }

    protected void InternalError(Exception error)
    {
        foreach (var observer in observers)
        {
            observer.OnError(error);
        }
    }

    protected void InternalComplete()
    {
        foreach (var observer in observers)
        {
            observer.OnCompleted();
        }
    }
    #endregion
    #region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: supprimer l'état managé (objets managés)
            }

            // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
            // TODO: affecter aux grands champs une valeur null
            disposedValue = true;
        }
    }

    // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
    // ~ReactiveResult()
    // {
    //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

 

    #endregion
    #region Private class
    private class Subscription : IDisposable
    {
        private ReactiveResult<T> observable;
        private IObserver<Result<T>> observer;

        public Subscription(ReactiveResult<T> observable, IObserver<Result<T>> observer)
        {
            this.observable = observable;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observable != null && observer != null)
            {
                observable.observers.Remove(observer);
                observable = null;
                observer = null;
            }
        }
    }
    #endregion
}


public static class ReactiveResultExtensions
{

    public static  IDisposable Match<TOut>(
        this ReactiveResult<TOut> result,
        Action<TOut> onSuccess,
        Action<IReadOnlyList<Error>> onFailure
    )
    =>result.Subscribe(
            r=>onSuccess(r.Value),
            (Exception e)=> {
                result.AddError(e);
                onFailure(result.Errors);

            });

    public static IDisposable Match<TOut>(
        this ReactiveResult<TOut> result,
        Action<TOut> onSuccess,
        Action<IReadOnlyList<Error>> onFailure,
        Action completed
    )
    => result.Subscribe(
            r => onSuccess(r.Value),
            (Exception e) => {
                result.AddError(e);
                onFailure(result.Errors);

            },
            ()=>completed()
            );
}


public sealed class ReactiveResultException<T> : ApplicationException
{
    internal const string ALREADY_COMPLETED = "The reactive result is already completed";
    public ReactiveResultException(ReactiveResult<T> result, string message) : base(message)
    {
    }
}
