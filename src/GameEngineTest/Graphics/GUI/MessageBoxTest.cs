using FakeItEasy;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class MessageBoxTest : IGraphicComponentTest
    {

        [TestCase]
        public void HandleKeyInput_SelectionKeyWithLinesToDisplay_CallsNextLine()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            A.CallTo(() => textContainerMock.HasNext()).Returns(true);
            var messageBox = CreateMessageBox(textContainerMock);

            messageBox.HandleKeyInput(CommandKeys.Select);

            A.CallTo(() => textContainerMock.NextLine()).MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void HandleKeyInput_SelectionKeyWihtNoLinesToDisplay_RaisesEevent()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            A.CallTo(() => textContainerMock.HasNext()).Returns(false);
            var messageBox = CreateMessageBox(textContainerMock);
            var eventRaised = false;
            messageBox.OnAllLineShowed += delegate { eventRaised = true; };

            messageBox.HandleKeyInput(CommandKeys.Select);

            Assert.True(eventRaised);
        }            

        [TestCase]
        public void ResetText_CalledAfterSettingText_TextIsEmpty()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            var messageBox = CreateMessageBox(textContainerMock);
            
            messageBox.DisplayText("TestString");
            messageBox.ResetText();

            A.CallToSet(() => textContainerMock.Text).To("").MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void DisplayText_SetSomeText_CalledIsForwardedToContainer()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            var messageBox = CreateMessageBox(textContainerMock);

            messageBox.DisplayText("TestString");

            A.CallToSet(() => textContainerMock.Text).To("TestString").MustHaveHappened(Repeated.AtLeast.Once);
        }

        private static MessageBox CreateMessageBox(ITextGraphicContainer container = null)
        {
            if (container == null)
                container = new TextGraphicContainerFake();

            return new MessageBox(container);

        }

        protected override IGraphicComponent CreateComponent()
        {
            return CreateMessageBox();
        }
    }
}
