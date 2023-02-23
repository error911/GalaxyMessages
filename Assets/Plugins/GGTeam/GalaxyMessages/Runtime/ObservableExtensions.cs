using System;

namespace GGTeam.GalaxyMessages.Runtime
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> self, Action<T> onNext, int priority = 0)
        {
            var subscriber = new Subscriber<T>(onNext, priority);
            var disposable = self.Subscribe(subscriber);

            return disposable;
        }
    }
}
