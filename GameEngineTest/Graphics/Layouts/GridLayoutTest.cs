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
        protected override ILayout CreateLayout()
        {
            return new GridLayout(2, 2);
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
        public void LayoutContainer_SetRowsColumnsInConstructor_ExpectedNumberOfRowsAndColumns(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var container = CreateContainer();
            var layout = new GridLayout(gridRows, gridColumns);
            var components = container.SetupContainer(componentCnt);

            layout.LayoutContainer(container);

            var rows = DistinctYPositions(components);
            var columns = DistinctXPositions(components);

            Assert.AreEqual(realRows, rows);
            Assert.AreEqual(realColumns, columns);
        }

        [TestCaseSource("PositionData")]
        public void LayoutContainer_SetRowsColumnsProperty_ExpectedNumberOfRowsAndColumns(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var container = CreateContainer();
            var layout = new GridLayout(1, 1);
            var components = container.SetupContainer(componentCnt);
            
            layout.Columns = gridColumns;
            layout.Rows = gridRows;
            layout.LayoutContainer(container);

            var rows = DistinctYPositions(components); 
            var columns = DistinctXPositions(components); 

            Assert.AreEqual(realRows, rows);
            Assert.AreEqual(realColumns, columns);

        }

        private int DistinctXPositions(List<GraphicComponentMock> components)
        {
            return (from obj in components select obj.XPosition).Distinct().Count();
        }

        private int DistinctYPositions(List<GraphicComponentMock> components)
        {
            return (from obj in components select obj.YPosition).Distinct().Count();
        }
        

        [TestCaseSource("PositionData")]
        public void LayoutContainer_NormalSetup_ComponentsAreCorrectOrdered(int componentCnt, int gridRows, int gridColumns, int realRows, int realColumns)
        {
            var container = CreateContainer();
            var layout = new GridLayout(gridRows, gridColumns);

            float X = container.XPosition;
            float Y = container.YPosition;
            float Width = container.Width / ((float) realColumns);
            float Height = container.Height / ((float) realRows);

            container.SetupContainer(componentCnt);
            container.Layout = layout;

            layout.LayoutContainer(container);

            var components = container.Components;
            for (int i = 0; i < components.Count; i++)
            {
                int row = ToRow(i, realRows, realColumns);
                int column = ToColumn(i, realRows, realColumns);

                Assert.AreEqual(X + column * Width, components[i].XPosition);
                Assert.AreEqual(Y + row * Height, components[i].YPosition);
                Assert.AreEqual(Width, components[i].Width);
                Assert.AreEqual(Height, components[i].Height);
            }
        }

        private int ToRow(int index, int rows, int columns)
        {
            return (int)Math.Floor((double) index / columns);
        }

        private int ToColumn(int index, int rows, int columns)
        {
            return index % columns;
        }

        private Container CreateContainer()
        {
            var container = new Container(engineMock.Object);
            container.SetCoordinates(5.0f, 5.0f, 500.0f, 500.0f);

            return container;
        }
    }
}
