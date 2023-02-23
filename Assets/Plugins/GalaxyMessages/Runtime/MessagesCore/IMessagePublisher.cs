namespace GGTeam.GalaxyMessages.Runtime
{
    public interface IMessagePublisher
    {
        bool Publish<T>(T message);
    }
}
