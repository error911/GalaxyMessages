
namespace GGTeam.GalaxyMessages.Sample
{
    public static partial class Message
    {
        public static class Player
        {
            public sealed class ChangeHP : GalaxyMessage<ChangeHP, int>
            {
                public int Data => Model;
            }

            public sealed class ChangeMana : GalaxyMessage<ChangeMana, int>
            {
                public int Data => Model;
            }
        }
    }
}