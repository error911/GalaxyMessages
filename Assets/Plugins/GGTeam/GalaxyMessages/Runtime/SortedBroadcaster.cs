using System;
using System.Collections.Generic;

namespace GGTeam.GalaxyMessages.Runtime
{
    public class SortedBroadcaster<T> : IBroadcaster, IObservable<T>, IObserver<T>
    {
        private readonly SortedDictionary<int, (IBroadcaster Broadcaster, IObservable<T> Observable, IObserver<T> Observer)> _containers =
            new SortedDictionary<int, (IBroadcaster, IObservable<T>, IObserver<T>)>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

        #region IDisposable

        void IDisposable.Dispose()
        {
            foreach (var container in _containers.Values)
            {
                container.Broadcaster.Dispose();
            }

            _containers.Clear();
        }

        #endregion

        #region IObservable<T>

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            var priority = observer is IPrioritizedObserver<T> prioritizedObserver ? prioritizedObserver.Priority : 0;
            
            if (!_containers.TryGetValue(priority, out var container))
            {
                var value = new Broadcaster<T>();
                container = (value, value, value);
                _containers.Add(priority, container);
            }

            var observable = container.Observable;
            var subscription = observable.Subscribe(observer);
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
            foreach (var observer in _containers)
            {
                observer.Value.Observer.OnNext(value);
            }
        }

        #endregion
    }
}