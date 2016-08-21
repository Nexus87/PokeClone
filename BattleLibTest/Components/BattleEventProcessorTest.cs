using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest.Components
{
    public class EventQueueFake : IEventQueue
    {
        List<IEvent> events = new List<IEvent>();
        public void DispatchAllEvents()
        {
            foreach (var ev in events)
                ev.Dispatch();

            events.Clear();
        }
        public void AddEvent(IEvent newEvent)
        {
            events.Add(newEvent);
        }

        public void Initialize()
        {
        }

        public void Update(GameTime time)
        {
        }
    }

    public class BattleEventFake : IEventCreator
    {
        private ClientIdentifier id = new ClientIdentifier();
        private PokemonWrapper pokemon;

        public event EventHandler CriticalDamage;
        public event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        public event EventHandler NewTurn;
        public event EventHandler<HPChangedEventArgs> HPChanged;
        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        public event EventHandler<MoveUsedEventArgs> MoveUsed;

        public BattleEventFake()
        {
            var pkmn = new Pokemon(new PokemonData { Name = "name" }, new Stats());
            pokemon = new PokemonWrapper(id) { Pokemon = pkmn };
        }
        public void RaiseCritcalDamageEvent()
        {
            CriticalDamage(this, EventArgs.Empty);
        }

        public void RaiseMoveEffectiveEvent(MoveEfficiency effect)
        {
            MoveEffective(this, new MoveEffectiveEventArgs(effect, pokemon));
        }

        public void RaiseNewTurnEvent()
        {
            NewTurn(this, EventArgs.Empty);
        }

        public void RaiseHPChangedEvent(int hp = 10)
        {
            HPChanged(this, new HPChangedEventArgs(id, hp));
        }

        public void RaisePokemonChangedEvent()
        {
            PokemonChanged(this, new ClientPokemonChangedEventArgs(id, pokemon));
        }

        public void RaiseStatusChanged(StatusCondition condition)
        {
            StatusChanged(this, new ClientStatusChangedEventArgs(id, condition));
        }

        public void RaiseMoveUsed()
        {
            var move = new Move(new MoveData(){Name = "Name"});
            MoveUsed(this, new MoveUsedEventArgs(move, pokemon));
        }



        public void Critical()
        {
            throw new NotImplementedException();
        }

        public void Effective(MoveEfficiency effect, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }

        public void SetNewTurn()
        {
            throw new NotImplementedException();
        }

        public void SetHP(ClientIdentifier id, int hp)
        {
            throw new NotImplementedException();
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(PokemonWrapper pokemon, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public void UsingMove(PokemonWrapper source, Move move)
        {
            throw new NotImplementedException();
        }

        public void SwitchPokemon(PokemonWrapper pokemon)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class BattleEventProcessorTest
    {
        EventQueueFake eventQueue;
        Mock<IGUIService> guiMock;
        Mock<IBattleGraphicController> graphicMock;
        BattleEventFake eventMock;

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
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseCritcalDamageEvent();
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase(MoveEfficiency.NoEffect)]
        [TestCase(MoveEfficiency.NotEffective)]
        [TestCase(MoveEfficiency.VeryEffective)]
        public void HandleMoveEffectiveEvent_AfterDispatchedEvent_GUIMessageIsSent(MoveEfficiency efficient)
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseMoveEffectiveEvent(efficient);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase]
        public void HandleMoveEffectiveEvent_NormalEfficiency_NoGUIMessageIsSent()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseMoveEffectiveEvent(MoveEfficiency.Normal);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Never);
        }

        [TestCase]
        public void NewTurnHandler_NormalEvent_ShowGUIIsCalled()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseNewTurnEvent();
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.ShowMenu(), Times.Once);
        }

        [TestCase]
        public void HPChangedHandler_NormalEvent_GraphicComponentSetHPIsCalled()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseHPChangedEvent(10);
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetHP(It.IsAny<ClientIdentifier>(), 10), Times.Once);
        }

        [TestCase]
        public void PokemonChangedHandler_NormalEvent_GraphicComponentSetPokemonIsCalled()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaisePokemonChangedEvent();
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetPokemon(It.IsAny<ClientIdentifier>(), It.IsAny<PokemonWrapper>()), Times.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GraphicComponentCalled()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseStatusChanged(StatusCondition.KO);
            eventQueue.DispatchAllEvents();

            graphicMock.Verify(o => o.SetPokemonStatus(It.IsAny<ClientIdentifier>(), StatusCondition.KO), Times.Once);
        }

        [TestCase]
        public void StatusChangedHandler_NormalEvent_GUIComponentCalled()
        {
            var eventProcessor = CreateEventProcessor();

            eventMock.RaiseStatusChanged(StatusCondition.KO);
            eventQueue.DispatchAllEvents();

            guiMock.Verify(o => o.SetText(It.IsAny<string>()), Times.Once);
        }

        [TestCase]
        public void MoveUsedHandler_NormalEvent_GUIComponentCalled()
        {
            var eventProcessor = CreateEventProcessor();

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
