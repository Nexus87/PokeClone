﻿using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.Util;
using Microsoft.Xna.Framework.Content;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class TableLayoutTest : ILayoutTest
    {
        TableLayout layout;

        [SetUp]
        public void Setup()
        {
            layout = new TableLayout(2, 2);
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    layout.SetComponent(i, j, new GraphicComponentMock());
            }

            testLayout = layout;
        }

        [TestCase]
        public void GrowingLayoutTest()
        {
            Assert.AreEqual(2, layout.Rows);
            Assert.AreEqual(2, layout.Columns);

            IGraphicComponent[,] component = new IGraphicComponent[2, 2];
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    component[i, j] = layout.GetComponent(i, j);
            }

            layout.SetComponent(2, 1, new Mock<IGraphicComponent>(gameMock.Object).Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(2, layout.Columns);

            layout.SetComponent(2, 3, new Mock<IGraphicComponent>(gameMock.Object).Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(4, layout.Columns);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                   Assert.AreEqual(component[i, j], layout.GetComponent(i, j));
            }
        }
        
        [TestCase]
        public void NullComponentTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var tableLayout = new TableLayout(5, 5);
            var component = new Mock<IGraphicComponent>(gameMock.Object);
            var serviceMock = new Mock<IServiceProvider>();
            var contentMock = new Mock<ContentManager>(serviceMock.Object);
            component.SetCoordinates(0.0f, 00.0f, 250.0f, 250.0f);

            tableLayout.Init(component.Object);
            tableLayout.Setup(contentMock.Object);
            tableLayout.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);

            spriteBatch.Objects.Clear();
            tableLayout.SetComponent(4, 4, new GraphicComponentMock());
            tableLayout.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            Assert.AreEqual(50.0f, spriteBatch.Objects[0].Size.X);
            Assert.AreEqual(50.0f, spriteBatch.Objects[0].Size.Y);
            Assert.AreEqual(4*50.0f, spriteBatch.Objects[0].Position.X);
            Assert.AreEqual(4 * 50.0f, spriteBatch.Objects[0].Position.Y);
        }

        [TestCase]
        public void ResizeTest()
        {
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);

            layout.Resize(3, 2);
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(3, layout.Rows);

            layout.Resize(3, 3);
            Assert.AreEqual(3, layout.Columns);
            Assert.AreEqual(3, layout.Rows);

            layout.Resize(2, 2);
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);
        }

        [TestCase]
        public void InvalidResizeTest()
        {
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);

            Assert.Throws<ArgumentException>(delegate { layout.Resize(-1, 2); });
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);

            Assert.Throws<ArgumentException>(delegate { layout.Resize(2, -1); });
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);
            
        }

        [TestCase]
        public void ResizeDrawTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var component = new Mock<IGraphicComponent>(gameMock.Object);
            component.SetCoordinates(0.0f, 00.0f, 180.0f, 180.0f);

            layout.Init(component.Object);
            Assert.AreEqual(2, layout.Columns);
            Assert.AreEqual(2, layout.Rows);
            layout.Draw(spriteBatch);
            spriteBatch.Objects.Clear();

            layout.Resize(3, 3);
            Assert.AreEqual(3, layout.Columns);
            Assert.AreEqual(3, layout.Rows);

            layout.Draw(spriteBatch);

            Assert.AreEqual(4, spriteBatch.Objects.Count);
            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(0.0f, 0.0f, 120.0f, 120.0f);

            layout.Resize(1, 1);
            Assert.AreEqual(1, layout.Columns);
            Assert.AreEqual(1, layout.Rows);
            spriteBatch.Objects.Clear();

            layout.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(0.0f, 0.0f, 180.0f, 180.0f);
        }
    }
}
