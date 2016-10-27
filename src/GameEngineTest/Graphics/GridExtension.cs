using GameEngine.Graphics.TableView;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;

namespace GameEngineTest.Graphics
{
    internal static class GridExtension
    {
        public static void Draw(this TableGrid grid)
        {
            grid.Draw(new GameTime(), new SpriteBatchMock());
        }
    }
}