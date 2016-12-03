using FakeItEasy;
using GameEngine.Utils;
using MainModule;
using MainModule.Graphics;
using NUnit.Framework;

namespace MainModuleTest.Graphics
{
    [TestFixture]
    public class GameStateComponentTest
    {
        private const int SpriteId = 0;

        [TestCase(1, 1, Direction.Right)]
        [TestCase(0, 0, Direction.Up)]
        public void MoveDirection_BlockedField_NoMoveIsDone(int startX, int startY, Direction direction)
        {
            var stateComponent = CreateGameStateComponent();
            stateComponent.SetMap(CreateMap());
            stateComponent.PlaceSprite(SpriteId, new FieldCoordinate(startX, startY));

            stateComponent.Move(SpriteId, direction);

            Assert.AreEqual(new FieldCoordinate(startX, startY), stateComponent.GetPosition(SpriteId));
        }

        [TestCase(0, 0, 1, 0, Direction.Right)]
        public void MoveDirection_ValidField_SpritePositionAsExpected(int startX, int startY, int expectedX, int expectedY, Direction direction)
        {
            var stateComponent = CreateGameStateComponent();
            stateComponent.SetMap(CreateValidMap());
            stateComponent.PlaceSprite(SpriteId, new FieldCoordinate(startX, startY));

            stateComponent.Move(SpriteId, direction);

            Assert.AreEqual(new FieldCoordinate(expectedX, expectedY), stateComponent.GetPosition(SpriteId));
        }

        private static Map CreateValidMap()
        {
            var table = new Table<Tile>
            {
                [0, 0] = new Tile("", true),
                [1, 0] = new Tile("", true),
                [0, 1] = new Tile("", true),
                [1, 1] = new Tile("", true),
                [2, 0] = new Tile("", true),
                [2, 1] = new Tile("", true),
                [2, 2] = new Tile("", true),
                [0, 2] = new Tile("", true),
                [1, 2] = new Tile("", true)
            };

            return new Map(table, new TilePosition(0, 0));
        }

        private static Map CreateMap()
        {
            var table = new Table<Tile>
            {
                [0, 0] = new Tile("", true),
                [1, 0] = new Tile("", false),
                [0, 1] = new Tile("", false),
                [1, 1] = new Tile("", true),
                [2, 0] = new Tile("", false),
                [2, 1] = new Tile("", false),
                [2, 2] = new Tile("", false),
                [0, 2] = new Tile("", false),
                [1, 2] = new Tile("", false)
            };

            return new Map(table, new TilePosition(0, 0));
        }

        private static GameStateComponent CreateGameStateComponent()
        {
            var screenContollerMock = A.Fake<IWorldScreenController>();
            return new GameStateComponent(screenContollerMock);
        }
    }
}
