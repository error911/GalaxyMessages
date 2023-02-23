using System;

namespace GGTeam.GalaxyMessages.Runtime
{
    public sealed class Subscriber<T> : IPrioritizedObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly int _priority;

        public Subscriber(Action<T> onNext, int priority)
        {
            _onNext = onNext;
            _priority = priority;
        }

        #region IObserver<T>

        void IObserver<T>.OnCompleted()
        {
        }

        void IObserver<T>.OnError(Exception error)
        {
        }

        void IObserver<T>.OnNext(T value)
        {
            _onNext.Invoke(value);
        }

        #endregion

        #region IPrioritizedObserver

        int IPrioritizedObserver<T>.Priority => _priority;

        #endregion
    }
}
