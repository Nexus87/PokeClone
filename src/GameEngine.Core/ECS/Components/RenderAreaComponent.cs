using System;
using GameEngine.Graphics.General;

namespace GameEngine.Core.ECS.Components
{
    public class RenderAreaComponent : Component
    {
        private readonly IScreen _screen;

        public RenderAreaComponent(Guid entityId, IScreen screen) : base(entityId)
        {
            _screen = screen;
        }

        public ISpriteBatch SpriteBatch => _screen.SceneSpriteBatch;
    }
}