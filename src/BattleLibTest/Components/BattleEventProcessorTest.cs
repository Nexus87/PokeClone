using Base.Data;
using BattleLib;
using BattleLib.Components;
using BattleLib.Components.BattleState;
using Moq;
using NUnit.Framework;
using BattleLib.Components.GraphicComponents;

namespace BattleLibTest.Components
{
    [TestFixture]
    public class BattleEventProcessorTest
    {
        private EventQueueFake eventQueue;
        private Mock<IGUIService> guiMock;
        private Mock<IBattleGraphicController> graphicMock;
        private BattleEventFake eventMock;

        [SetUp]
        public void Setup()
        {
            eventQueue = new EventQueueFake();
            guiMock = new Mock<IGUIService>();
            eventMock = new BattleEventFake();
            graphicMock = new Mock<IBattleGraphicController>();
        }

        [TestCase]
        public void HandleCriticalChangeEvent_AfterDispatchedEvent_GUIMessageIsSent()
        {
            CreateEventProcessor();

            eventMock.RaiseCritcalDamageEvent();
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase(MoveEfficiency.NoEffect)]
        [TestCase(MoveEfficiency.NotEffective)]
        [TestCase(MoveEfficiency.VeryEffective)]
        public void HandleMoveEffectiveEvent_AfterDispatchedEvent_GUIMessageIsSent(MoveEfficiency efficient)
        {
            CreateEventProcessor();

            eventMock.RaiseMoveEffectiveEvent(efficient);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase]
        public void HandleMoveEffectiveEvent_NormalEfficiency_NoGUIMessageIsSent()
        {
            CreateEventProcessor();

            eventMock.RaiseMoveEffectiveEvent(MoveEfficiency.Normal);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Never);
        }

        [TestCase]
        public void NewTurnHandler_NormalEvent_ShowGUIIsCalled()
        {
            CreateEventProcessor();

            eventMock.RaiseNewTurnEvent();
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.ShowMenu(), Times.Once);
        }

        [TestCase]
        public void HPChangedHandler_NormalEvent_GraphicComponentSetHPIsCalled()
        {
            CreateEventProcessor();

            eventMock.RaiseHPChangedEvent(10);
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetHP(It.IsAny<ClientIdentifier>(), 10), Times.Once);
        }

        [TestCase]
        public void PokemonChangedHandler_NormalEvent_GraphicComponentSetPokemonIsCalled()
        {
            CreateEventProcessor();

            eventMock.RaisePokemonChangedEvent();
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetPokemon(It.IsAny<ClientIdentifier>(), It.IsAny<PokemonWrapper>()), Times.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GraphicComponentCalled()
        {
            CreateEventProcessor();

            eventMock.RaiseStatusChanged(StatusCondition.KO);
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetPokemonStatus(It.IsAny<ClientIdentifier>(), StatusCondition.KO), Times.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GUIComponentCalled()
        {
            CreateEventProcessor();

            eventMock.RaiseStatusChanged(StatusCondition.KO);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase]
        public void MoveUsedHandler_NormalEvent_GUIComponentCalled()
        {
            CreateEventProcessor();

            eventMock.RaiseMoveUsed();
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        private BattleEventProcessor CreateEventProcessor()
        {
             return new BattleEventProcessor(guiMock.Object, graphicMock.Object, eventQueue, eventMock);
        }
    }
}
