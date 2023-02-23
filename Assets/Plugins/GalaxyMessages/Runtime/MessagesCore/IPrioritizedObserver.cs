using System;

namespace GGTeam.GalaxyMessages.Runtime
{
    public interface IPrioritizedObserver<in T> : IObserver<T>
    {
        int Priority { get; }
    }
}
