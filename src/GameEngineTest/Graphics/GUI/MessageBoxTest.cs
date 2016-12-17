using FakeItEasy;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class MessageBoxTest : IWidgetTest
    {

        [TestCase]
        public void HandleInput_SelectionKeyWithLinesToDisplay_CallsNextLine()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            A.CallTo(() => textContainerMock.HasNext()).Returns(true);
            var messageBox = CreateMessageBox(textContainerMock);

            messageBox.HandleInput(CommandKeys.Select);

            A.CallTo(() => textContainerMock.NextLine()).MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void HandleInput_SelectionKeyWihtNoLinesToDisplay_RaisesEevent()
        {
            var textContainerMock = A.Fake<ITextGraphicContainer>();
            A.CallTo(() => textContainerMock.HasNext()).Returns(false);
            var messageBox = CreateMessageBox(textContainerMock);
            var eventRaised = false;
            messageBox.OnAllLineShowed += delegate { eventRaised = true; };

            messageBox.HandleInput(CommandKeys.Select);

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
