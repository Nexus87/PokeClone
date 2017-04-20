using Microsoft.Xna.Framework;

namespace GameEngine.ECS
{
    public interface ISystem
    {
        void Init(MessagingSystem messagingSystem);
        void Update(GameTime time);
    }
}