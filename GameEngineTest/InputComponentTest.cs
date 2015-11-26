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

        [SetUp]
        public void Setup()
        {
            gameMock = new Mock<Game>();
            managerMock = new Mock<InputManager>();
            component = new InputComponent(gameMock.Object, managerMock.Object);
            keys = new List<Keys> { Keys.A, Keys.B, Keys.C };
        }

        [TestCase]
        public void SingleRegisteredKeyTest()
        {
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(new Keys[]{keys.First()});

            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>())).Returns<Keys>( key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState()).Returns(keyboardState);

            handlerMock.Setup(o => o.HandleInput(keys.First()));

            component.handler = handlerMock.Object;
            component.Keys = keys;

            GameTime time = new GameTime();

            component.Update(time);
            handlerMock.Verify(o => o.HandleInput(It.IsAny<Keys>()), Times.Once);
            
            component.Update(time);

            handlerMock.Verify(o => o.HandleInput(It.IsAny<Keys>()), Times.Once);
            
        }
        
        [TestCase]
        public void MultipleRegisteredKeyTests()
        {
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(keys.ToArray());

            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>())).Returns<Keys>(key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState()).Returns(keyboardState);

            component.handler = handlerMock.Object;
            component.Keys = keys;

            foreach(var key in keys)
                handlerMock.Setup(o => o.HandleInput(key));

            GameTime time = new GameTime();

            component.Update(time);
            foreach(var key in keys)
                handlerMock.Verify(o => o.HandleInput(key), Times.Once);

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(key), Times.Once);
        }

        [TestCase]
        public void SingleUnregisteredKeyTest()
        {
            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(new Keys[] { Keys.D });

            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>())).Returns<Keys>(key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState()).Returns(keyboardState);

            component.handler = handlerMock.Object;
            component.Keys = keys;

            GameTime time = new GameTime();

            component.Update(time);
            handlerMock.Verify(o => o.HandleInput(It.IsAny<Keys>()), Times.Never);

            component.Update(time);

            handlerMock.Verify(o => o.HandleInput(It.IsAny<Keys>()), Times.Never);
        }

        [TestCase]
        public void MultipleKeysTest()
        {
            var pressedKeys = new List<Keys>(keys);
            pressedKeys.Add(Keys.D);
            pressedKeys.Add(Keys.E);

            var handlerMock = new Mock<IInputHandler>(MockBehavior.Strict);
            var keyboardState = new KeyboardState(pressedKeys.ToArray());

            managerMock.Setup(o => o.IsKeyDown(It.IsAny<Keys>())).Returns<Keys>(key => keyboardState.IsKeyDown(key));
            managerMock.Setup(o => o.GetState()).Returns(keyboardState);

            component.handler = handlerMock.Object;
            component.Keys = keys;

            foreach (var key in keys)
                handlerMock.Setup(o => o.HandleInput(key));

            GameTime time = new GameTime();

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(key), Times.Once);

            component.Update(time);
            foreach (var key in keys)
                handlerMock.Verify(o => o.HandleInput(key), Times.Once);
        }
    }
}
