using Base.Data;
using BattleMode.Core;
using BattleMode.Core.Components;
using BattleMode.Core.Components.BattleState;
using BattleMode.Core.Components.GraphicComponents;
using FakeItEasy;
using NUnit.Framework;

namespace BattleModeTest.Components
{
    [TestFixture]
    public class BattleEventProcessorTest
    {
        private EventQueueFake _eventQueue;
        private IGUIService _guiMock;
        private IBattleGraphicController _graphicMock;
        private BattleEventFake _eventMock;

        [SetUp]
        public void Setup()
        {
            _eventQueue = new EventQueueFake();
            _guiMock = A.Fake<IGUIService>();
            _eventMock = new BattleEventFake();
            _graphicMock = A.Fake<IBattleGraphicController>();
        }

        [TestCase]
        public void HandleCriticalChangeEvent_AfterDispatchedEvent_GUIMessageIsSent()
        {
            CreateEventProcessor();

            _eventMock.RaiseCritcalDamageEvent();
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.SetText(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase(MoveEfficiency.NoEffect)]
        [TestCase(MoveEfficiency.NotEffective)]
        [TestCase(MoveEfficiency.VeryEffective)]
        public void HandleMoveEffectiveEvent_AfterDispatchedEvent_GUIMessageIsSent(MoveEfficiency efficient)
        {
            CreateEventProcessor();

            _eventMock.RaiseMoveEffectiveEvent(efficient);
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.SetText(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void HandleMoveEffectiveEvent_NormalEfficiency_NoGUIMessageIsSent()
        {
            CreateEventProcessor();

            _eventMock.RaiseMoveEffectiveEvent(MoveEfficiency.Normal);
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.SetText(A<string>.Ignored)).MustNotHaveHappened();
        }

        [TestCase]
        public void NewTurnHandler_NormalEvent_ShowGUIIsCalled()
        {
            CreateEventProcessor();

            _eventMock.RaiseNewTurnEvent();
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.ShowMenu()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void HPChangedHandler_NormalEvent_GraphicComponentSetHPIsCalled()
        {
            CreateEventProcessor();

            _eventMock.RaiseHPChangedEvent();
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _graphicMock.SetHp(A<ClientIdentifier>.Ignored, 10)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void PokemonChangedHandler_NormalEvent_GraphicComponentSetPokemonIsCalled()
        {
            CreateEventProcessor();

            _eventMock.RaisePokemonChangedEvent();
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _graphicMock.SetPokemon(A<ClientIdentifier>.Ignored, A<PokemonWrapper>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GraphicComponentCalled()
        {
            CreateEventProcessor();

            _eventMock.RaiseStatusChanged(StatusCondition.KO);
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _graphicMock.SetPokemonStatus(A<ClientIdentifier>.Ignored, StatusCondition.KO))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GUIComponentCalled()
        {
            CreateEventProcessor();

            _eventMock.RaiseStatusChanged(StatusCondition.KO);
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.SetText(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void MoveUsedHandler_NormalEvent_GUIComponentCalled()
        {
            CreateEventProcessor();

            _eventMock.RaiseMoveUsed();
            _eventQueue.DispatchAllEvents();

            A.CallTo(() => _guiMock.SetText(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        private void CreateEventProcessor() => new BattleEventProcessor(_guiMock, _graphicMock, _eventQueue, _eventMock);
    }
}