using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class SelectableContainerTest
    {
        [TestCase]
        public void Draw_Unselected_ArrowIsNotDrawn()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock, contentMock);

            container.Unselect();
            container.Draw();

            Assert.False(arrowMock.WasDrawn);
        }

        [TestCase]
        public void Draw_NormalSetup_ContentIsDrawn()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock, contentMock);

            container.Draw();

            Assert.True(contentMock.WasDrawn);
        }

        [TestCase]
        public void Draw_Select_ArrowIsDrawn()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock, contentMock);

            container.Select();
            container.Draw();

            Assert.True(arrowMock.WasDrawn);
        }

        [TestCase]
        public void PreferredHeight_ContentWithPreferredHeight_EqualsContentsPreferredHeight()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock() { PreferredHeight = 10 };
            var container = CreateContainer(arrowMock, contentMock);

            Assert.AreEqual(10, container.PreferredHeight, 10e-9);
        }

        [TestCase]
        public void PreferredHeight_ComponentsPreferredHeightChanged_EqualsContentsNewPreferredHeight()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock, contentMock);

            contentMock.PreferredHeight = 10;
            contentMock.RaisePreferredSizeChanged();

            Assert.AreEqual(10, container.PreferredHeight, 10e-9);
        }
        private SelectableContainer CreateContainer(GraphicComponentMock arrowMock, GraphicComponentMock contentMock)
        {
            var container = new SelectableContainer(arrowMock, contentMock);
            container.SetCoordinates(10, 10, 100, 100);
            return container;
        }
    }
}
