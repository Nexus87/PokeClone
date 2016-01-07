using GameEngine.Graphics.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class DefaultSelectionHandlerTest
    {
        private DefaultSelectionHandler testObj;
        private Mock<IItemView> viewMock;
        private int startColumn = 0;
        private int startRow = 0;

        [SetUp]
        public void Setup()
        {
            testObj = new DefaultSelectionHandler();

            viewMock = new Mock<IItemView>();
            
            viewMock.SetupGet(o => o.Columns).Returns(10);
            viewMock.SetupGet(o => o.Rows).Returns(10);
            viewMock.SetupGet(o => o.ViewportColumns).Returns(2);
            viewMock.SetupGet(o => o.ViewportRows).Returns(2);
            viewMock.SetupGet(o => o.ViewportStartColumn).Returns(startColumn);
            viewMock.SetupGet(o => o.ViewportStartRow).Returns(startRow);
            viewMock.SetupSet(o => o.ViewportStartColumn = It.IsAny<int>()).Callback<int>(i => startColumn = i);
            viewMock.SetupSet(o => o.ViewportStartRow = It.IsAny<int>()).Callback<int>(i => startRow = i);

            testObj.Init(viewMock.Object);
        }

        [TestCase]
        public void InitialSelectionTest()
        {

        }
    }
}
