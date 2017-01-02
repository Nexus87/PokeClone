using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    public class Panel : AbstractPanel
    {
        public Color BackgroundColor { get; set; }
        private readonly PanelRenderer _renderer;
        private IGraphicComponent _component;

        public Panel(PanelRenderer renderer)
        {
            _renderer = renderer;
        }

        public void SetContent(IGraphicComponent component)
        {
            if (_component != null)
                RemoveChild(_component);
            _component = component;
            AddChild(component);
        }

        protected override void Update()
        {

            _component.SetCoordinates(Area.X + 20, Area.Y + 20, Area.Width - 40, Area.Height - 40);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
            _component?.Draw(time, batch);
        }
    }
}