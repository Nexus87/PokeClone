using System;
using FakeItEasy;
using GameEngine.Entities;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.EventComponent
{
    [TestFixture]
    public class EventComponentTest
    {
        private EventQueue _eventComponent;

        [SetUp]
        public void Setup()
        {
            _eventComponent = new EventQueue();
        }


        private class TestEvent : IEvent
        {
            public bool DispatchCalled;
            public Action Callback;
            public event EventHandler EventProcessed = delegate { };

            public void Dispatch()
            {
                Callback?.Invoke();

                EventProcessed(this, EventArgs.Empty);
                DispatchCalled = true;
            }
        }

        [TestCase]
        public void Update_WithThreeEvent_IsDispatchedInFIFOOrder()
        {
            var counter = 0;
            var event1Number = 0;
            var event2Number = 0;
            var event3Number = 0;

            var eventMock1 = new TestEvent();
            var eventMock2 = new TestEvent();
            var eventMock3 = new TestEvent();

            eventMock1.Callback = () => { event1Number = counter; counter++; };
            eventMock2.Callback = () => { event2Number = counter; counter++; };
            eventMock3.Callback = () => { event3Number = counter; counter++; };

            _eventComponent.AddEvent(eventMock1);
            _eventComponent.AddEvent(eventMock2);
            _eventComponent.AddEvent(eventMock3);

            while (UndispatchedEventsLeft(eventMock1, eventMock2, eventMock3))
                _eventComponent.Update(new GameTime());

            Assert.AreEqual(0, event1Number);
            Assert.AreEqual(1, event2Number);
            Assert.AreEqual(2, event3Number);
        }

        private bool UndispatchedEventsLeft(params TestEvent[] events)
        {
            var areAllEventsDispatched = true;
            foreach (var ev in events)
                areAllEventsDispatched &= ev.DispatchCalled;

            return !areAllEventsDispatched;
        }

        [TestCase]
        public void Update_AppendTwoEvents_BlocksUntilFirstEventIsDone()
        {
            var eventMock1 = A.Fake<IEvent>();
            var eventMock2 = new TestEvent();

            _eventComponent.AddEvent(eventMock1);
            _eventComponent.AddEvent(eventMock2);

            _eventComponent.Update(new GameTime());

            A.CallTo(() => eventMock1.Dispatch()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.False(eventMock2.DispatchCalled);

            _eventComponent.Update(new GameTime());

            Assert.False(eventMock2.DispatchCalled);

            eventMock1.EventProcessed += Raise.With(eventMock1, EventArgs.Empty);

            _eventComponent.Update(new GameTime());

            Assert.True(eventMock2.DispatchCalled);

        }
    }
}
