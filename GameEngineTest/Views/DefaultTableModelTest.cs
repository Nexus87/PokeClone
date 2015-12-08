using GameEngine.Graphics.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class DefaultTableModelTest : IItemModelTest
    {
        DefaultTableModel<TestType> model;
        [SetUp]
        public void Setup()
        {
            model = new DefaultTableModel<TestType>();
            testModel = model;
        }

        [TestCase]
        public void SetDataTest()
        {
            var t1 = new TestType { testString = "T1" };
            var t2 = new TestType { testString = "T2" };

            Assert.AreEqual(0, model.Rows);
            Assert.AreEqual(0, model.Columns);

            Assert.IsTrue(model.SetData(t1, 1, 0));
            Assert.AreEqual(2, model.Rows);
            Assert.AreEqual(1, model.Columns);
            Assert.AreEqual(t1, model.DataAt(1, 0));
            Assert.AreEqual(null, model.DataAt(0, 0));

            Assert.IsTrue(model.SetData(t2, 1, 1));
            Assert.AreEqual(2, model.Rows);
            Assert.AreEqual(2, model.Columns);
            Assert.AreEqual(t2, model.DataAt(1, 1));

            Assert.IsTrue(model.SetData(t2, 2, 2));
            Assert.AreEqual(3, model.Rows);
            Assert.AreEqual(3, model.Columns);
            Assert.AreEqual(t2, model.DataAt(2, 2));

        }
    }
}
