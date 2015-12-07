using GameEngine.Graphics.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    public class TestType
    {
        public string testString;
        public override string ToString()
        {
            return testString + "!";
        }
    }

    public abstract class IItemModelTest
    {
        public IItemModel<TestType> testModel;

        [TestCase]
        public void SetGetTest()
        {
            var t = new TestType { testString = "Test" };
            Assert.True(testModel.SetData(t, 0, 0));

            Assert.AreEqual(t, testModel.DataAt(0, 0));
            Assert.AreEqual(t.testString + "!", testModel.DataStringAt(0, 0));

        }

        [TestCase]
        public void DataChangedEventTest()
        {
            DataChangedArgs<TestType> eventArgs = null;
            var t = new TestType { testString = "Test" };

            testModel.DataChanged += (obj, args) => { eventArgs = args; };

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.row);
            Assert.AreEqual(0, eventArgs.column);
            Assert.AreEqual(t, eventArgs.newData);

            eventArgs = null;

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.IsNull(eventArgs);
        }
    }
}
