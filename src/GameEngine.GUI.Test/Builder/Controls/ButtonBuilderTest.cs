using System;
using System.Xml.Linq;
using FakeItEasy;
using GameEngine.GUI.Builder.Controls;
using GameEngine.GUI.Renderers;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Builder.Controls
{
    [TestFixture]
    public class ButtonBuilderTest
    {
        public const float DefaultFontSize = 12.0f;
        [TestCase("<Button Text=\"Text\" TextHeight=\"16.0\" />", "Text", 16.0f)]
        [TestCase("<Button TextHeight=\"16.0\" />", "", 16.0f)]
        [TestCase("<Button />", "", DefaultFontSize)]
        [TestCase("<Button Text=\"Text\" TextHeight=\"16.0\" Row=\"2\"/>", "Text", 16.0f)]
        public void BuildFromNode_ValidXML_IsAsExpected(string xml, string expectedText, float expectedTextHeight)
        {
            var xmlDocument = PrepareXmlDocument(xml);
            var buttonBuilder = CreateButtonBuilder();

            var button = buttonBuilder.BuildButtonFromNode(xmlDocument.Root);

            Assert.AreEqual(expectedText, button.Text);
            Assert.AreEqual(expectedTextHeight, button.TextHeight);
        }

        [TestCase("<Button ButtonPressed=\"Action\"/>")]
        public void WireUpController_WithDummyController_ControllerIsConnctedToButtonEvent(string xml)
        {
            var xmlDocument = PrepareXmlDocument(xml);
            var buttonBuilder = CreateButtonBuilder();
            var controller = new DummyController();

            var button = buttonBuilder.BuildButtonFromNode(xmlDocument.Root);
            buttonBuilder.WireUpController(xmlDocument.Root, button, controller);

            button.OnButtonPressed();

            Assert.True(controller.ActionTriggered);

        }
        private class DummyController
        {
            public void Action(object sender, EventArgs args)
            {
                ActionTriggered = true;
            }

            public bool ActionTriggered { get; private set; }
        }

        private static ButtonBuilder CreateButtonBuilder()
        {
            var skinFake = A.Fake<ISkin>();
            A.CallTo(() => skinFake.BuildButtonRenderer()).Returns( A.Fake<IButtonRenderer>());
            A.CallTo(() => skinFake.DefaultTextHeight).Returns(DefaultFontSize);

            var buttonBuilder = new ButtonBuilder(skinFake);
            return buttonBuilder;
        }

        private static XDocument PrepareXmlDocument(string xml)
        {
            return XDocument.Parse(xml);
        }
    }
}