using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModule.Graphics
{
    public interface IWorldScreenController : IGraphicComponent
    {
        void PlayerTurnDirection(Direction direction);
        void PlayerMoveDirection(Direction direction);
    }
}
