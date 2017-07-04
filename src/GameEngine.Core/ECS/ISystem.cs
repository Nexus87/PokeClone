using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS
{
    public interface ISystem
    {
        void Init(MessagingSystem messagingSystem);
    }
}