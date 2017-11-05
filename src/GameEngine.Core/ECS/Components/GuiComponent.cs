using System;
using GameEngine.Graphics.General;
using GameEngine.GUI;

namespace GameEngine.Core.ECS.Components
{
    public class GuiComponent : Component
    {
        private readonly IScreen _screen;

        public GuiComponent(Guid entityId, IScreen screen) : base(entityId)
        {
            _screen = screen;
        }

        public ISpriteBatch SpriteBatch => _screen.GuiSpriteBatch;
        public bool GuiVisible { get; set; }
        public ISkin Skin { get; set; }
    }
}