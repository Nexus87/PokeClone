﻿using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
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
    public class GridLayoutTest : ILayoutTest
    {
        GridLayout layout;

        [SetUp]
        public void Setup()
        {
            layout = new GridLayout(2, 2);

            testLayout = layout;
            testContainer = new Container(engineMock.Object);
            testContainer.FillContainer(4);
            testContainer.Layout = layout;
        }


        public static List<TestCaseData> PositionData = new List<TestCaseData>
        {
            // These data fit exactly in the grid
            new TestCaseData(9, 3, 3, 3, 3),
            new TestCaseData(6, 2, 3, 2, 3),
            new TestCaseData(6, 3, 2, 3, 2),
            // Now we have more object than initial cells in the grid
            // In this case, rows are ignored
            new TestCaseData(10, 5, 1, 10, 1),
            new TestCaseData(10, 1, 3, 4, 3),
            // Less objects than initial cells in the grid
            // rows are ignored
            new TestCaseData(5, 10, 1, 5, 1),
            new TestCaseData(5, 3, 5, 1, 5),
            // columns == 0 means fit grid to rows
            new TestCaseData(10, 5, 0, 5, 2),
            new TestCaseData(9, 5, 0, 5, 2),
            new TestCaseData(5, 5, 0, 5, 1),
            // rows == 0 meas fit grid to columns
            new TestCaseData(10, 0, 1, 10, 1),
            new TestCaseData(10, 0, 3, 4, 3),
            new TestCaseData(5, 0, 1, 5, 1),
            new TestCaseData(5, 0, 5, 1, 5),
            // Less objects than columns: shrink to fit
            new TestCaseData(5, 1, 10, 1, 5)
        };

        [TestCaseSource("PositionData")]
        public void PositionTest(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var batch = new SpriteBatchMock();
            var container = new Container(engineMock.Object);
            var layout = new GridLayout(gridRows, gridColumns);

            container.SetCoordinates(5.0f, 5.0f, 500.0f, 500.0f);
            container.FillContainer(componentCnt);
            container.Layout = layout;

            layout.LayoutContainer(container);
            container.Draw(batch);

            Assert.AreEqual(componentCnt, batch.Objects.Count);

            var rows = (from obj in batch.Objects select obj.Position.Y).Distinct();
            var columns = (from obj in batch.Objects select obj.Position.X).Distinct();

            Assert.AreEqual(realRows, rows.Count());
            Assert.AreEqual(realColumns, columns.Count());

            foreach (var obj in batch.Objects)
                obj.IsInConstraints(container);
        }

        [TestCaseSource("PositionData")]
        public void PropertySetterTest(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var batch = new SpriteBatchMock();
            var container = new Container(engineMock.Object);
            var layout = new GridLayout(1, 1);

            layout.Columns = gridColumns;
            layout.Rows = gridRows;

            container.SetCoordinates(5.0f, 5.0f, 500.0f, 500.0f);
            container.FillContainer(componentCnt);
            container.Layout = layout;

            layout.LayoutContainer(container);
            container.Draw(batch);

            Assert.AreEqual(componentCnt, batch.Objects.Count);

            var rows = (from obj in batch.Objects select obj.Position.Y).Distinct();
            var columns = (from obj in batch.Objects select obj.Position.X).Distinct();

            Assert.AreEqual(realRows, rows.Count());
            Assert.AreEqual(realColumns, columns.Count());

            foreach (var obj in batch.Objects)
                obj.IsInConstraints(container);
        }
        
        [TestCase]
        public void NullComponentTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var tableLayout = new GridLayout(5, 5);

            testContainer.ClearContainer();
            testContainer.SetCoordinates(0.0f, 0.0f, 250.0f, 250.0f);
            testContainer.Layout = tableLayout;
            tableLayout.LayoutContainer(testContainer);

            testContainer.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);

            spriteBatch.Objects.Clear();
            testContainer.FillContainer(1);
            testContainer.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            Assert.AreEqual(250.0f, spriteBatch.Objects[0].Size.X);
            Assert.AreEqual(250.0f, spriteBatch.Objects[0].Size.Y);
            Assert.AreEqual(0, spriteBatch.Objects[0].Position.X);
            Assert.AreEqual(0, spriteBatch.Objects[0].Position.Y);
        }

        [TestCaseSource("PositionData")]
        public void PositionOrderTest(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var batch = new SpriteBatchMock();
            var container = new Container(engineMock.Object);
            var layout = new GridLayout(gridRows, gridColumns);
            container.SetCoordinates(5.0f, 5.0f, 500.0f, 500.0f);

            float X = container.X;
            float Y = container.Y;
            float Width = container.Width / ((float) realColumns);
            float Height = container.Height / ((float) realRows);

            container.FillContainer(componentCnt);
            container.Layout = layout;

            layout.LayoutContainer(container);

            var components = container.Components;
            int rowCount = 0;
            for (int i = 0; i < components.Count; i++)
            {
                if (i % realColumns == 0 && i != 0)
                    rowCount++;

                Assert.AreEqual(X + (i % realColumns) * Width, components[i].X);
                Assert.AreEqual(Y + rowCount * Height, components[i].Y);
                Assert.AreEqual(Width, components[i].Width);
                Assert.AreEqual(Height, components[i].Height);
            }
        }
    }
}