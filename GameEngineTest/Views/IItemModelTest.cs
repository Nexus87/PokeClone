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
        public TestType() { }
        public TestType(string testString)
        {
            this.testString = testString;
        }

        public string testString;
        public override string ToString()
        {
            return testString;
        }
    }

    public abstract class IItemModelTest
    {
        protected abstract ITableModel<TestType> GetModel();

        [TestCase]
        public void DataAt_SetData_ReturnsSameValue()
        {
            var testModel = GetModel();
            var t = new TestType { testString = "Test" };
            Assert.True(testModel.SetData(t, 0, 0));

            Assert.AreEqual(t, testModel.DataAt(0, 0));

        }

        [TestCase]
        public void SetData_NewValue_DataChangedEventRaised()
        {
            var testModel = GetModel();
            DataChangedEventArgs<TestType> eventArgs = null;
            var t = new TestType { testString = "Test" };

            testModel.DataChanged += (obj, args) => { eventArgs = args; };
            testModel.SetData(t, 0, 0);
            
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.Row);
            Assert.AreEqual(0, eventArgs.Column);
            Assert.AreEqual(t, eventArgs.NewData);
        }

    }
}
