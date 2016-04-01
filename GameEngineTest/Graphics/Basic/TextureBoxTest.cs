﻿using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngineTest.Util;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class TextureBoxTest : IGraphicComponentTest
    {
        [SetUp]
        public void Setup()
        {
            contentMock.Setup(o => o.Load<Texture2D>(It.IsAny<string>())).Returns(new Texture2D(GameEngineTest.Util.Extensions.dev, 10, 10));
        }

        [TestCase]
        public void EmptyImageTest()
        {
            var testObj = CreateEmptyBox();
            var spriteBatch = new SpriteBatchMock();

            testObj.Draw(spriteBatch);

            Assert.IsEmpty(spriteBatch.DrawnObjects);
        }

        private TextureBox CreateEmptyBox()
        {
            return new TextureBox(gameMock.Object);
        }
        protected override IGraphicComponent CreateComponent()
        {
            var box = CreateEmptyBox();
            box.Setup(contentMock.Object);
            box.Image = new Texture2D(GameEngineTest.Util.Extensions.dev, 10, 10);

            return box;
        }
    }
}
