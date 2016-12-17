using System;
using GameEngine.Globals;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.GUI
{
    public class Button : AbstractWidget
    {
        public event EventHandler ButtonSelected;
        private readonly IGraphicComponent _content;

        public Button(IGraphicComponent content)
        {
            _content = content;
        }

        protected override void Update()
        {
            base.Update();
            _content.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _content.Draw(time, batch);
        }

        public override void Setup()
        {
            _content.Setup();
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            OnButtonSelected();
        }

        protected void OnButtonSelected()
        {
            var handler = ButtonSelected;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}