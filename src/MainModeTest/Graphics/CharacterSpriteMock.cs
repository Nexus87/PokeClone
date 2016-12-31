using GameEngineTest.TestUtils;
using MainMode.Core;
using MainMode.Core.Graphics;

namespace MainModeTest.Graphics
{
    public class CharacterSpriteMock : GraphicComponentMock, ICharacterSprite
    {
        public void TurnToDirection(Direction direction)
        {
            throw new System.NotImplementedException();
        }
    }
}