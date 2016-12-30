using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.GUI
{
    public class Dialog : AbstractGraphicComponent
    {
        private readonly IGraphicComponent _border;
        private IGraphicComponent _widget;

        public Dialog(IImageBoxRenderer renderer, ITexture2D borderTexture = null) :
            this(new ImageBox(renderer){ Image = borderTexture})
        {}
        public Dialog(IGraphicComponent border)
        {
            _border = border;
            _widget = new NullGraphicObject();
        }


        public void AddWidget(IGraphicComponent widget)
        {
            widget.CheckNull("widget");
            _widget = widget;
        }

        public override void Setup()
        {
            _border.Setup();
            _widget.Setup();
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            _widget.HandleKeyInput(key);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _border?.Draw(time, batch);
            _widget.Draw(time, batch);
        }

        protected override void Update()
        {
            _widget.Area = new Rectangle(Area.X + 100, Area.Y + 100, Area.Width - 200, Area.Height - 200);
            _border.SetCoordinates(this);
        }
    }
}