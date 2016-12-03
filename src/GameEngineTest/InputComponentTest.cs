using GameEngine;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using GameEngine.GameEngineComponents;

namespace GameEngineTest
{
#if TRAVIS
    [Ignore("Not working with TravisCI")]
#endif
    [TestFixture]
    public class InputComponentTest
    {
        private readonly IReadOnlyList<Keys> _keys = new List<Keys> { Keys.Up, Keys.Down, Keys.Left };
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _map = new Dictionary<Keys, CommandKeys>
        {
            {Keys.Escape, CommandKeys.Back},
            {Keys.Down, CommandKeys.Down},
            {Keys.Left, CommandKeys.Left},
            {Keys.Right, CommandKeys.Right},
            {Keys.Enter, CommandKeys.Select},
            {Keys.Up, CommandKeys.Up}
        };

        [TestCase]
        public void Update_SingleRegisteredKeyPressed_InputHandlerGetNotification()
        {
            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(_keys.First());
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};

            // Assert the that only the first key is detected
            component.Update();
            A.CallTo(() => handlerMock.HandleInput(A<CommandKeys>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_SingleRegisteredKeyPressedUpdateCalledTwice_InputHandlerGetNotificationOnce()
        {
            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(_keys.First());
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};

            // Assert the that only the first key is detected
            component.Update();
            component.Update();
            A.CallTo(() => handlerMock.HandleInput(A<CommandKeys>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_MultipleRegistredKeysPressed_AllKeysAreGivenToInputHandler()
        {
            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(_keys.ToArray());
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};

            // Assert that every test key is detected once
            component.Update();
            foreach (var key in _keys)
                A.CallTo(() => handlerMock.HandleInput(_map[key])).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_MultipleRegistredKeysPressedUpdateCalledTwice_AllKeysAreGivenToInputHandlerOnce()
        {
            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(_keys.ToArray());
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};



            // Assert that every test key is detected once
            component.Update();
            component.Update();
            foreach (var key in _keys)
                A.CallTo(() => handlerMock.HandleInput(_map[key])).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_SingleUnregistredKeyIsPressed_InputHandlerIsNotCalled()
        {
            // The component should not watch out for D
            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(Keys.D);
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};

            // Make sure, that no test key was detected
            component.Update();
            A.CallTo(() => handlerMock.HandleInput(A<CommandKeys>.Ignored)).MustNotHaveHappened();
        }

        [TestCase]
        public void Update_OneRegistredAndOneUnregistredKeyArePressed_InputHandlerIsCalledOnce()
        {

            var handlerMock = A.Fake<IInputHandler>();
            var managerMock = CreateInputManagerFake(Keys.D, Keys.Down);
            var component = new InputComponent(managerMock, _map) {Handler = handlerMock};
            component.Update();

            A.CallTo(() => handlerMock.HandleInput(A<CommandKeys>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => handlerMock.HandleInput(_map[Keys.Down])).MustHaveHappened(Repeated.Exactly.Once);
        }

        private static KeyboardState CreateKeyboardState(params Keys[] keys)
        {
            return new KeyboardState(keys);
        }

        private static IKeyboardManager CreateInputManagerFake(KeyboardState state)
        {
            return new KeyboardManagerStub(state);
        }

        private static IKeyboardManager CreateInputManagerFake(params Keys[] keys)
        {
            return CreateInputManagerFake(CreateKeyboardState(keys));
        }
    }
}
