using System;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.GUI
{
    public class Button : AbstractWidget
    {
        public event EventHandler ButtonSelected;
        private readonly IGraphicComponent content;

        public Button(IGraphicComponent content)
        {
            this.content = content;
        }

        protected override void Update()
        {
            base.Update();
            content.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            content.Draw(time, batch);
        }

        public override void Setup()
        {
            content.Setup();
        }

        public override bool HandleInput(CommandKeys key)
        {
            if (key != CommandKeys.Select)
                return false;

            OnButtonSelected();
            return true;
        }

        protected void OnButtonSelected()
        {
            var handler = ButtonSelected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}