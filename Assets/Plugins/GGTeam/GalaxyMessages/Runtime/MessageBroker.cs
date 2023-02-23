using System;
using System.Collections.Generic;

namespace GGTeam.GalaxyMessages.Runtime
{
    public sealed class MessageBroker : IDisposable, IMessageBroker
    {
        private readonly Dictionary<Type, IBroadcaster> _broadcasters = new Dictionary<Type, IBroadcaster>();

        #region IDisposable

        void IDisposable.Dispose()
        {
            foreach (var broadcaster in _broadcasters.Values)
            {
                broadcaster.Dispose();
            }

            _broadcasters.Clear();
        }

        #endregion

        #region IMessagePublisher

        bool IMessagePublisher.Publish<T>(T message)
        {
            if (!_broadcasters.TryGetValue(typeof(T), out var broadcaster))
            {
                return false;
            }

            if (broadcaster is IObserver<T> observer)
            {
                observer.OnNext(message);
            }

            return true;
        }

        #endregion

        #region IMessageReceiver

        IObservable<T> IMessageReceiver.Receive<T>()
        {
            if (!_broadcasters.TryGetValue(typeof(T), out var broadcaster))
            {
                _broadcasters[typeof(T)] = broadcaster = new SortedBroadcaster<T>();
            }

            return broadcaster as IObservable<T>;
        }

        #endregion
    }
}
