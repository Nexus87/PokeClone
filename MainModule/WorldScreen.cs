using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;

namespace MainModule
{
    public class WorldScreen : AbstractGraphicComponent
    {
        private readonly IGraphicComponent player;
        private readonly IMap map;
        private readonly ScreenConstants constants;


        public WorldScreen(TextureBox player, IMap map, ScreenConstants constants)
            : this((IGraphicComponent) player, map, constants)
        {
        }

        internal WorldScreen(IGraphicComponent player, IMap map, ScreenConstants constants)
        {
            this.player = player;
            this.map = map;
            this.constants = constants;
        }

        public FieldSize FieldSize { get { return map.FieldSize; } }

        protected override void Update()
        {
            base.Update();

            var playerX = constants.ScreenWidth / 2.0f;
            var playerY = constants.ScreenHeight / 2.0f;

            player.XPosition = playerX;
            player.YPosition = playerY;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            player.Draw(time, batch);
            map.Draw(time, batch);
        }

        public override void Setup()
        {
            player.Setup();
            map.Setup();
        }

        public void FocusMapAt(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}