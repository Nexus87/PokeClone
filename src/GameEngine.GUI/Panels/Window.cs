using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    [GameType]
    public class Window : AbstractPanel
    {
        private readonly IWindowRenderer _renderer;
        private IGraphicComponent _content;

        public Window(IWindowRenderer renderer)
        {
            _renderer = renderer;
        }

        public void SetContent(IGraphicComponent component)
        {
            RemoveChild(_content);
            _content = component;
            AddChild(_content);
            Invalidate();
        }


        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
            _content?.Draw(time, batch);
        }

        protected override void Update()
        {
            _content?.SetCoordinates(
                Area.X + _renderer.LeftMargin,
                Area.Y + _renderer.TopMargin,
                Area.Width - (_renderer.RightMargin + _renderer.LeftMargin),
                Area.Height - (_renderer.BottomMargin + _renderer.TopMargin)
            );
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            _content?.HandleKeyInput(key);
        }

        public override void Setup()
        {
            _content?.Setup();
        }
    }
}