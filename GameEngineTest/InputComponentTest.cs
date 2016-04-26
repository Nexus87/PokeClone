using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;
using Moq;
using GameEngine.Graphics;
using GameEngineTest.Util;
using Microsoft.Xna.Framework.Input;
namespace GameEngineTest
{
    [TestFixture]
    public class InputComponentTest
    {
        Mock<Game> gameMock = new Mock<Game>();
        readonly IReadOnlyList<Keys> keys = new List<Keys> { Keys.Up, Keys.Down, Keys.Left };
        readonly IReadOnlyDictionary<Keys, CommandKeys> map = new Dictionary<Keys, CommandKeys>
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
            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(keys.First());
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;    

            // Assert the that only the first key is detected
            component.Update();
            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Once);
        }

        [TestCase]
        public void Update_SingleRegisteredKeyPressedUpdateCalledTwice_InputHandlerGetNotificationOnce()
        {
            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(keys.First());
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;

            // Assert the that only the first key is detected
            component.Update();
            component.Update();
            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Once);
        }

        [TestCase]
        public void Update_MultipleRegistredKeysPressed_AllKeysAreGivenToInputHandler()
        {
            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(keys.ToArray());
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;


            // Assert that every test key is detected once
            component.Update();
            foreach(var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);
        }

        [TestCase]
        public void Update_MultipleRegistredKeysPressedUpdateCalledTwice_AllKeysAreGivenToInputHandlerOnce()
        {
            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(keys.ToArray());
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;


            // Assert that every test key is detected once
            component.Update();
            component.Update();
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);
        }

        [TestCase]
        public void Update_SingleUnregistredKeyIsPressed_InputHandlerIsNotCalled()
        {
            // The component should not watch out for D
            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(Keys.D);
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;


            // Make sure, that no test key was detected
            component.Update();
            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Never);
        }

        [TestCase]
        public void Update_OneRegistredAndOneUnregistredKeyArePressed_InputHandlerIsCalledOnce()
        {

            var handlerMock = new Mock<IInputHandler>();
            var managerMock = CreateInputManagerFake(Keys.D, Keys.Down);
            var component = new InputComponent(gameMock.Object, managerMock.Object, map);

            component.handler = handlerMock.Object;

            component.Update();

            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Once);
            handlerMock.Verify(o => o.HandleInput(map[Keys.Down]), Times.Once);
        }

        private KeyboardState CreateKeyboardState(params Keys[] keys)
        {
            return new KeyboardState(keys);
        }

        private Mock<InputManager> CreateInputManagerFake(KeyboardState state)
        {
            var manager = new Mock<InputManager>();

            manager.Setup(o => o.IsKeyDown(It.IsAny<Keys>()))
                .Returns<Keys>(key => state.IsKeyDown(key));
            manager.Setup(o => o.GetState())
                .Returns(state);

            return manager;
        }

        private Mock<InputManager> CreateInputManagerFake(params Keys[] keys)
        {
            return CreateInputManagerFake(CreateKeyboardState(keys));
        }
    }
}
