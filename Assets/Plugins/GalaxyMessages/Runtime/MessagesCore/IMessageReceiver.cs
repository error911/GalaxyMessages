using System;

namespace GGTeam.GalaxyMessages.Runtime
{
    public interface IMessageReceiver
    {
        IObservable<T> Receive<T>();
    }
}
