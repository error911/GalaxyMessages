using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGTeam.GalaxyMessages.Runtime
{
    public sealed class Broadcaster<T> : IBroadcaster, IObservable<T>, IObserver<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        #region IDisposable

        void IDisposable.Dispose()
        {
#if UNITY_EDITOR
            if (_observers.Count > 0)
            {
                Debug.Log($"Unsubscribe messages {typeof(T)}. Counnt: {_observers.Count}");
            }
#endif
            _observers.Clear();
        }

        #endregion

        #region IObservable<T>

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            var subscription = new Subscription<T>(observer, _observers);
            return subscription;
        }

        #endregion

        #region IObserver<T>

        void IObserver<T>.OnCompleted()
        {
        }

        void IObserver<T>.OnError(Exception error)
        {
        }

        void IObserver<T>.OnNext(T value)
        {
            if(_observers.Count == 0)
                return;
            
            var observers = _observers.ToArray();

            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }

        #endregion
    }
}
