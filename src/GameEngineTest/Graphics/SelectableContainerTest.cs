using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;

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

        [TestCase]
        public void Draw_WithoutContent_DoesNotThrow()
        {
            var arrowMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock);

            Assert.DoesNotThrow(() => container.Draw());
        }

        [TestCase]
        public void Draw_SetContentProperty_ContentIsDrawn()
        {
            var arrowMock = new GraphicComponentMock();
            var contentMock = new GraphicComponentMock();
            var container = CreateContainer(arrowMock);

            container.Content = contentMock;
            container.Draw();

            Assert.True(contentMock.WasDrawn);
            
        }

        private SelectableContainer<GraphicComponentMock> CreateContainer(GraphicComponentMock arrowMock) 
        {
            return CreateContainer(arrowMock, null);
        }
        private SelectableContainer<GraphicComponentMock> CreateContainer(GraphicComponentMock arrowMock, GraphicComponentMock contentMock)
        {
            SelectableContainer<GraphicComponentMock> container;
            if (contentMock != null)
                container = new SelectableContainer<GraphicComponentMock>(arrowMock, contentMock);
            else
                container = new SelectableContainer<GraphicComponentMock>(arrowMock);

            container.SetCoordinates(10, 10, 100, 100);
            container.Setup();
            return container;
        }
    }
}
