using GameEngineTest.TestUtils;
using MainModule;
using MainModule.Graphics;
using NUnit.Framework;

namespace MainModuleTest.Graphics
{
    [TestFixture]
    public class CharacterSpriteTest
    {
        private GraphicComponentMock lookingDown;
        private GraphicComponentMock lookingLeft;
        private GraphicComponentMock lookingRight;
        private GraphicComponentMock lookingUp;

        [SetUp]
        public void Setup()
        {
            lookingDown = new GraphicComponentMock();
            lookingLeft = new GraphicComponentMock();
            lookingRight = new GraphicComponentMock();
            lookingUp = new GraphicComponentMock();
        }

        [Test]
        public void Draw_DefaultObject_CharacterLooksDown()
        {
            var characterSprite = CreateSprite();

            characterSprite.Draw();

            Assert.True(lookingDown.WasDrawn);
        }

        [Test]
        public void Draw_AfterCallTurn_ComponentIsDrawn()
        {
            var characterSprite = CreateSprite();

            characterSprite.TurnToDirection(Direction.Up);
            characterSprite.Draw();

            Assert.True(lookingUp.WasDrawn);

        }
        private CharacterSprite CreateSprite()
        {
            var sprite = new CharacterSprite(lookingLeft, lookingRight, lookingUp, lookingDown);
            sprite.Setup();
            return sprite;
        }
    }
}