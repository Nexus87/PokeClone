using System.Collections.Generic;
using FakeItEasy;
using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class AbstractLayoutTest
    {
        private static AbstractLayout CreateLayoutMock()
        {
            var layoutMock = A.Fake<AbstractLayout>();

            return layoutMock;
        }

        public static List<TestCaseData> ValidPropertyData = new List<TestCaseData>
        {
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 150.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 150.0f, 0),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 10),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 40),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 10),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 10),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 10),
        };

        [TestCaseSource(nameof(ValidPropertyData))]
        public void ProtectedProperties_SetLeftMargin_RespectsSetMargins(float x, float y, float width, float height, int margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(x, y, width, height);

            testObj.SetMargin(left: margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(x + margin, y, width - margin, height);
        }
            
        [TestCaseSource(nameof(ValidPropertyData))]
        public void ProtectedProperties_SetRightMargin_RespectsSetMargins(float x, float y, float width, float height, int margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(x, y, width, height);

            testObj.SetMargin(right: margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(x, y, width - margin, height);
        }

        [TestCaseSource(nameof(ValidPropertyData))]
        public void ProtectedProperties_SetTopMargin_RespectsSetMargins(float x, float y, float width, float height, int margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(x, y, width, height);

            testObj.SetMargin(top: margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(x, y + margin, width, height - margin);
        }

        
        [TestCaseSource(nameof(ValidPropertyData))]
        public void ProtectedProperties_SetBottomMargin_RespectsSetMargins(float x, float y, float width, float height, int margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(x, y, width, height);

            testObj.SetMargin(bottom: margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(x, y, width, height - margin);
        }

        [TestCaseSource(nameof(ValidPropertyData))]
        public void ProtectedProperties_SetAllMargins_RespectsSetMargins(float x, float y, float width, float height, int margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(x, y, width, height);

            testObj.SetMargin(margin, margin, margin, margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(x + margin, y + margin, width - 2*margin, height - 2*margin);
        }
        

        [TestCase]
        public void LayoutContainer_SetCoordinates_UpdateComponentsIsCalled()
        {
            var container = CreateContainer();
            var layoutMock = CreateLayoutMock();
            var testLayout = layoutMock;

            container.SetCoordinates(200, 200, 200, 200);
            testLayout.LayoutContainer(container);

            A.CallTo(layoutMock).Where(x => x.Method.Name == "UpdateComponents").MustHaveHappened(Repeated.AtLeast.Once);
        }

        private static Container CreateContainer()
        {
            return new Container();
        }
    }
}
