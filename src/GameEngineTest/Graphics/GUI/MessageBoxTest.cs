using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class MessageBoxTest : IWidgetTest
    {

        [TestCase]
        public void HandleInput_SelectionKeyWithLinesToDisplay_CallsNextLine()
        {
            var textContainerMock = new Mock<ITextGraphicContainer>();
            textContainerMock.Setup(o => o.HasNext()).Returns(true);
            var messageBox = CreateMessageBox(textContainerMock.Object);

            messageBox.HandleInput(CommandKeys.Select);

            textContainerMock.Verify(o => o.NextLine(), Times.Once);
        }

        [TestCase]
        public void HandleInput_SelectionKeyWihtNoLinesToDisplay_RaisesEevent()
        {
            var textContainerMock = new Mock<ITextGraphicContainer>();
            textContainerMock.Setup(o => o.HasNext()).Returns(false);
            var messageBox = CreateMessageBox(textContainerMock.Object);
            bool eventRaised = false;
            messageBox.OnAllLineShowed += delegate { eventRaised = true; };

            messageBox.HandleInput(CommandKeys.Select);

            Assert.True(eventRaised);
        }            

        [TestCase]
        public void ResetText_CalledAfterSettingText_TextIsEmpty()
        {
            var textContainerMock = new Mock<ITextGraphicContainer>();
            var messageBox = CreateMessageBox(textContainerMock.Object);
            
            messageBox.DisplayText("TestString");
            messageBox.ResetText();

            textContainerMock.VerifySet(o => o.Text = "", Times.Once);
        }

        [TestCase]
        public void DisplayText_SetSomeText_CalledIsForwardedToContainer()
        {
            var textContainerMock = new Mock<ITextGraphicContainer>();
            var messageBox = CreateMessageBox(textContainerMock.Object);

            messageBox.DisplayText("TestString");
            
            textContainerMock.VerifySet(o => o.Text = "TestString", Times.Once);
        }

        [TestCase(CommandKeys.Back)]
        [TestCase(CommandKeys.Down)]
        [TestCase(CommandKeys.Left)]
        [TestCase(CommandKeys.Right)]
        [TestCase(CommandKeys.Up)]
        public void HandleInput_OtherKeysThanSelect_DoesNotHandle(CommandKeys key)
        {
            var messageBox = CreateMessageBox();

            Assert.False(messageBox.HandleInput(key));
        }

        private MessageBox CreateMessageBox(ITextGraphicContainer container = null)
        {
            if (container == null)
                container = new TextGraphicContainerFake();

            return new MessageBox(container);

        }
        protected override IWidget CreateWidget()
        {
            return CreateMessageBox();
        }
    }
}
