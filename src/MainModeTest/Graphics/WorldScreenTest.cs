using FakeItEasy;
using GameEngine.Globals;
using MainMode.Core;
using MainMode.Core.Graphics;
using MainModeTest.Utils;
using NUnit.Framework;

namespace MainModeTest.Graphics
{
    [TestFixture]
    public class WorldScreenTest
    {
//        [TestCase]
//        public void Draw_NormalSetup_PlayerAndMapAreDrawn()
//        {
//            var player = new CharacterSpriteMock();
//            var mapMock = CreateMap();
//            var screen = CreateWorldScreen(player, mapMock);
//
//            screen.Draw();
//
//            Assert.True(player.WasDrawn);
//            Assert.True(mapMock.WasDrawn);
//        }

        [TestCase(Direction.Up, Direction.Down)]
        [TestCase(Direction.Left, Direction.Right)]
        [TestCase(Direction.Right, Direction.Left)]
        [TestCase(Direction.Down, Direction.Up)]
        public void PlayerMove_GivenDirection_MapIsMovedInExpectedDirection(Direction moveDirection, Direction expectedMapMoveDirection)
        {
            var player = new CharacterSpriteMock();
            var mapMock = A.Fake<ISceneController>();
            var screen = CreateWorldScreen(player, mapMock);

            screen.PlayerMoveDirection(moveDirection);

            A.CallTo(() => mapMock.MoveMap(expectedMapMoveDirection)).MustHaveHappened(Repeated.Exactly.Once);
        }

        private static SceneControllerMock CreateMap()
        {
            var mock = new SceneControllerMock();
            return mock;
        }

        private static WorldScreen CreateWorldScreen(AbstractCharacterSprite player, ISceneController sceneController, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
                screenConstants = new ScreenConstants();
            var loaderMock = A.Fake<ISpriteLoader>();
            A.CallTo(() => loaderMock.GetSprite(A<string>.Ignored)).Returns(player);
            return new WorldScreen(sceneController, loaderMock);
        }
    }
}