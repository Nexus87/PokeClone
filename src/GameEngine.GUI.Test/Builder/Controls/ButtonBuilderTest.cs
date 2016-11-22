﻿using System.Xml.Linq;
using GameEngine.GUI.Builder.Controls;
using GameEngine.GUI.Renderers;
using Moq;
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

        private static ButtonBuilder CreateButtonBuilder()
        {
            var skinFake = new Mock<ISkin>();
            skinFake.Setup(s => s.BuildButtonRenderer()).Returns(new Mock<IButtonRenderer>().SetupAllProperties().Object);
            skinFake.Setup(s => s.DefaultTextHeight).Returns(DefaultFontSize);

            var buttonBuilder = new ButtonBuilder(skinFake.Object);
            return buttonBuilder;
        }

        private static XDocument PrepareXmlDocument(string xml)
        {
            return XDocument.Parse(xml);
        }
    }
}