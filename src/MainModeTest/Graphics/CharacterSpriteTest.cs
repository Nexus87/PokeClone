using FakeItEasy;
using GameEngine.Graphics.Textures;
using MainMode.Core;
using MainMode.Core.Graphics;
using NUnit.Framework;

namespace MainModeTest.Graphics
{
    [TestFixture]
    public class CharacterSpriteTest
    {
        private ITexture2D _lookingDown;
        private ITexture2D _lookingLeft;
        private ITexture2D _lookingRight;
        private ITexture2D _lookingUp;

        [SetUp]
        public void Setup()
        {
            _lookingDown = A.Fake<ITexture2D>();
            _lookingLeft = A.Fake<ITexture2D>();
            _lookingRight = A.Fake<ITexture2D>();
            _lookingUp = A.Fake<ITexture2D>();
        }

        [Test]
        public void Draw_DefaultObject_CharacterLooksDown()
        {
            var characterSprite = CreateSprite();

            Assert.AreEqual(_lookingDown, characterSprite.Texture);
        }

        [Test]
        public void Draw_AfterCallTurn_ComponentIsDrawn()
        {
            var characterSprite = CreateSprite();

            characterSprite.TurnToDirection(Direction.Up);

            Assert.AreEqual(_lookingUp, characterSprite.Texture);

        }
        private CharacterSprite CreateSprite()
        {
            var sprite = new CharacterSprite(_lookingLeft, _lookingRight, _lookingUp, _lookingDown);
            return sprite;
        }
    }
}