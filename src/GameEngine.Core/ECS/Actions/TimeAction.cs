using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Actions
{
    public class TimeAction
    {

        public readonly GameTime Time;

        public TimeAction(GameTime time)
        {
            Time = time;
        }
    }
}