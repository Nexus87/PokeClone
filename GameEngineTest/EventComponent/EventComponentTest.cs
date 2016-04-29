using GameEngine;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;

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


        private class TestEvent : IEvent
        {
            public bool DispatchCalled = false;
            public Action Callback = null;
            public event EventHandler OnEventProcessed = delegate { };

            public void Dispatch()
            {
                if (Callback != null)
                    Callback();

                OnEventProcessed(this, EventArgs.Empty);
                DispatchCalled = true;
            }
        }

        [TestCase]
        public void Update_WithThreeEvent_IsDispatchedInFIFOOrder()
        {
            int counter = 0;
            int event1Number = 0;
            int event2Number = 0;
            int event3Number = 0;

            var eventMock1 = new TestEvent();
            var eventMock2 = new TestEvent();
            var eventMock3 = new TestEvent();

            eventMock1.Callback = () => { event1Number = counter; counter++; };
            eventMock2.Callback = () => { event2Number = counter; counter++; };
            eventMock3.Callback = () => { event3Number = counter; counter++; };

            eventComponent.AddEvent(eventMock1);
            eventComponent.AddEvent(eventMock2);
            eventComponent.AddEvent(eventMock3);

            while (UndispatchedEventsLeft(eventMock1, eventMock2, eventMock3))
                eventComponent.Update(new GameTime());

            Assert.AreEqual(0, event1Number);
            Assert.AreEqual(1, event2Number);
            Assert.AreEqual(2, event3Number);
        }

        private bool UndispatchedEventsLeft(params TestEvent[] events)
        {
            bool areAllEventsDispatched = true;
            foreach (var ev in events)
                areAllEventsDispatched &= ev.DispatchCalled;

            return !areAllEventsDispatched;
        }

        [TestCase]
        public void Update_AppendTwoEvents_BlocksUntilFirstEventIsDone()
        {
            var eventMock1 = new Mock<IEvent>();
            var eventMock2 = new TestEvent();

            eventComponent.AddEvent(eventMock1.Object);
            eventComponent.AddEvent(eventMock2);

            eventComponent.Update(new GameTime());

            eventMock1.Verify(o => o.Dispatch(), Times.Once);
            Assert.False(eventMock2.DispatchCalled);

            eventComponent.Update(new GameTime());

            Assert.False(eventMock2.DispatchCalled);

            eventMock1.Raise(o => o.OnEventProcessed += null, eventMock1.Object, null);

            eventComponent.Update(new GameTime());

            Assert.True(eventMock2.DispatchCalled);

        }
    }
}
