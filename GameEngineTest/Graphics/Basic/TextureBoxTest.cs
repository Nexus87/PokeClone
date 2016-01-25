﻿using GameEngine.Graphics.Basic;
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
            var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            contentMock.Setup(o => o.Load<Texture2D>(It.IsAny<string>())).Returns(new Texture2D(dev, 10, 10));
            var box = new TextureBox();
            box.Setup(contentMock.Object);
            box.Image = new Texture2D(dev, 10, 10);
            testObj = box;
        }
        // No special tests needed here

        [TestCase]
        public void EmptyImageTest()
        {
            testObj = new TextureBox();
            var spriteBatch = new SpriteBatchMock();

            testObj.Draw(spriteBatch);

            Assert.IsEmpty(spriteBatch.Objects);
        }
    }
}
