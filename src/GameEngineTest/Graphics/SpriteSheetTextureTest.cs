using FakeItEasy;
using GameEngine.GUI;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class SpriteSheetTextureTest : IGraphicComponentTest
    {
        protected override IGraphicComponent CreateComponent()
        {
            var textureMock = A.Fake<ITexture2D>();
            var source = new Rectangle(10, 10, 200, 200);
            return new SpriteSheetTexture(textureMock, source);
        }
    }
}