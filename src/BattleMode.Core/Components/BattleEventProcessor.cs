using System;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Entities.BattleState;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Entities;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components
{
    [GameService(typeof(BattleEventProcessor))]
    public class BattleEventProcessor
    {
        private readonly IGuiEntity _guiEntity;
        private readonly IBattleGraphicController _graphicService;
        private readonly IEventQueue _queue;

        public BattleEventProcessor(IGuiEntity guiEntity, IBattleGraphicController graphicService, IEventQueue queue, IEventCreator events)
        {
            _guiEntity = guiEntity;
            _graphicService = graphicService;
            _queue = queue;

            events.CriticalDamage += CriticalDamageHandler;
            events.HPChanged += HPChangedHandler;
            events.MoveEffective += MoveEffectiveHandler;
            events.MoveUsed += MoveUsedHandler;
            events.NewTurn += NewTurnHandler;
            events.PokemonChanged += PokemonChangedHandler;
            events.StatusChanged += StatusChangedHandler;
        }

        private void StatusChangedHandler(object sender, ClientStatusChangedEventArgs e)
        {
            _queue.AddStatusEvent(_graphicService, e.Id, e.Status);
            _queue.AddShowMessageEvent(_guiEntity, "Status changed to: " + e.Status);
        }

        private void PokemonChangedHandler(object sender, ClientPokemonChangedEventArgs e)
        {
            _queue.AddSetPokemonEvent(_graphicService, e.Id, e.Pokemon);
        }

        private void NewTurnHandler(object sender, EventArgs e)
        {
            _queue.AddShowMenuEvent(_guiEntity);
        }

        private void MoveUsedHandler(object sender, MoveUsedEventArgs e)
        {
            _queue.AddShowMessageEvent(_guiEntity, e.Source.Name + " uses " + e.Move.Name);
        }

        private void MoveEffectiveHandler(object sender, MoveEffectiveEventArgs e)
        {
            switch (e.Effect)
            {
                case MoveEfficiency.NoEffect:
                    _queue.AddShowMessageEvent(_guiEntity, "It doesn't affect " + e.Target.Name + "...");
                    break;
                case MoveEfficiency.NotEffective:
                    _queue.AddShowMessageEvent(_guiEntity, "It's not very effective...");
                    break;
                case MoveEfficiency.VeryEffective:
                    _queue.AddShowMessageEvent(_guiEntity, "It's super effective!");
                    break;
            }
        }

        private void HPChangedHandler(object sender, HpChangedEventArgs e)
        {
            _queue.AddHpEvent(_graphicService, e.Id, e.Hp);
        }

        private void CriticalDamageHandler(object sender, EventArgs e)
        {
            _queue.AddShowMessageEvent(_guiEntity, "Critical hit!");
        }
    }
}
