using GameEngine;
using GameEngine.EventComponent;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.EventComponent
{
    [TestFixture]
    public class EventComponentTest
    {
        private EventQueue eventComponent;

        [SetUp]
        public void Setup()
        {
            eventComponent = new EventQueue();
        }


        [TestCase]
        public void OrderTest()
        {
            var eventMock1 = new Mock<IEvent>();
            var eventMock2 = new Mock<IEvent>();
            var eventMock3 = new Mock<IEvent>();

            eventMock1.Setup(o => o.Dispatch()).Callback(delegate { eventMock1.Raise(o => o.OnEventProcessed += null, eventMock1.Object, null); });
            eventMock2.Setup(o => o.Dispatch()).Callback(delegate { eventMock2.Raise(o => o.OnEventProcessed += null, eventMock2.Object, null); });
            eventMock3.Setup(o => o.Dispatch()).Callback(delegate { eventMock3.Raise(o => o.OnEventProcessed += null, eventMock3.Object, null); });

            eventComponent.AddEvent(eventMock1.Object);
            eventComponent.AddEvent(eventMock2.Object);
            eventComponent.AddEvent(eventMock3.Object);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Never);
            eventMock3.Verify(o => o.Dispatch(), Times.Never);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Once);
            eventMock3.Verify(o => o.Dispatch(), Times.Never);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Once);
            eventMock3.Verify(o => o.Dispatch(), Times.Once);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Once);
            eventMock3.Verify(o => o.Dispatch(), Times.Once);
        }

        [TestCase]
        public void BlockingTest()
        {
            var eventMock1 = new Mock<IEvent>();
            var eventMock2 = new Mock<IEvent>();

            eventComponent.AddEvent(eventMock1.Object);
            eventComponent.AddEvent(eventMock2.Object);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Never);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Never);

            eventMock1.Raise(o => o.OnEventProcessed += null, eventMock1.Object, null);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            eventMock2.Verify(o => o.Dispatch(), Times.Once);

        }
    }
}
