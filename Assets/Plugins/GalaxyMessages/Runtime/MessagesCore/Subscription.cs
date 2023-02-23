using System;
using System.Collections.Generic;

namespace GGTeam.GalaxyMessages.Runtime
{
    public sealed class Subscription<T> : IDisposable
    {
        private readonly IObserver<T> _observer;
        private readonly IList<IObserver<T>> _observers;

        public Subscription(IObserver<T> observer, IList<IObserver<T>> observers)
        {
            _observer = observer;
            _observers = observers;
            _observers.Add(_observer);
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            _observers.Remove(_observer);
        }

        #endregion
    }
}
