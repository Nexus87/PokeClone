using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngineTest.TestUtils;

namespace GameEngineTest.Graphics
{
    public class TestableMultilineTextBox : MultlineTextBox
    {

        public TestableMultilineTextBox(int lines, ITextSplitter splitter) : base(null, splitter, lines) 
        { 
        }

        public ICollection ComponentStrings()
        {
            return (from c in CreatedComponents select c.Text).ToList();
        }

        public List<TextGraphicComponentMock> CreatedComponents = new List<TextGraphicComponentMock>();

        protected override ITextGraphicComponent CreateTextComponent(ISpriteFont font)
        {
            var component = new TextGraphicComponentMock();
            CreatedComponents.Add(component);

            return component;
        }
    }
}