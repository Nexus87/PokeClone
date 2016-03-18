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
using Microsoft.Xna.Framework.Input;
namespace GameEngineTest
{
    [TestFixture]
    public class InputComponentTest
    {
        InputComponent component;
        Mock<Game> gameMock;
        Mock<InputManager> managerMock;
        List<Keys> keys;
        IReadOnlyDictionary<Keys, CommandKeys> map;
        [SetUp]
        public void Setup()
        {
            gameMock = new Mock<Game>();
            managerMock = new Mock<InputManager>();
            var config = new Configuration();
            map = config.KeyMap;

            component = new InputComponent(gameMock.Object, managerMock.Object, config);
            keys = new List<Keys> { Keys.Up, Keys.Down, Keys.Left };
        }

        [TestCase]
        public void SingleRegisteredKeyTest()
        {
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(new Keys[]{keys.First()});

            // Only the first key is pressed
            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>()) )
                .Returns<Keys>( key => keyboardState.IsKeyDown(key));

            managerMock.Setup(o => o.GetState()).Returns(keyboardState);
            handlerMock.Setup(o => o.HandleInput(map[keys.First()]))
                .Returns(true);

            component.handler = handlerMock.Object;    

            GameTime time = new GameTime();
            // Assert the that only the first key is detected
            component.Update(time);
            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Once);
            
            component.Update(time);

            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Once);
            
        }
        
        [TestCase]
        public void MultipleRegisteredKeyTests()
        {
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(keys.ToArray());

            // All test keys are pressed
            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>()))
                .Returns<Keys>(key => keyboardState.IsKeyDown(key));

            managerMock.Setup(o => o.GetState()).Returns(keyboardState);

            component.handler = handlerMock.Object;

            foreach(var key in keys)
                handlerMock.Setup(o => o.HandleInput(map[key])).Returns(true);

            GameTime time = new GameTime();

            // Assert that every test key is detected once
            component.Update(time);
            foreach(var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);
        }

        [TestCase]
        public void SingleUnregisteredKeyTest()
        {
            // The component should not watch out for D
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(new Keys[] { Keys.D });

            handlerMock.Setup(o => o.HandleInput(It.IsAny<CommandKeys>()))
                .Returns(true);
            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>()))
                .Returns<Keys>(key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState())
                .Returns(keyboardState);

            component.handler = handlerMock.Object;

            GameTime time = new GameTime();

            // Make sure, that no test key was detected
            component.Update(time);
            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Never);

            component.Update(time);

            handlerMock.Verify(o => o.HandleInput(It.IsAny<CommandKeys>()), Times.Never);
        }

        [TestCase]
        public void MultipleKeysTest()
        {
            // The input component should not listen for these two but for the other ones
            var pressedKeys = new List<Keys>(keys);
            pressedKeys.Add(Keys.D);
            pressedKeys.Add(Keys.E);

            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(pressedKeys.ToArray());

            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>()))
                .Returns<Keys>(key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState())
                .Returns(keyboardState);

            component.handler = handlerMock.Object;

            foreach (var key in keys)
                handlerMock.Setup(o => o.HandleInput(map[key])).Returns(true);

            GameTime time = new GameTime();

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(map[key]), Times.Once);
        }
    }
}
