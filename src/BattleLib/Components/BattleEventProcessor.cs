using System;
using BattleMode.Components.BattleState;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core.GameEngineComponents;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components
{
    [GameService(typeof(BattleEventProcessor))]
    public class BattleEventProcessor
    {
        private readonly IGUIService _guiService;
        private readonly IBattleGraphicController _graphicService;
        private readonly IEventQueue _queue;

        public BattleEventProcessor(IGUIService guiService, IBattleGraphicController graphicService, IEventQueue queue, IEventCreator events)
        {
            _guiService = guiService;
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
            _queue.AddShowMessageEvent(_guiService, "Status changed to: " + e.Status);
        }

        private void PokemonChangedHandler(object sender, ClientPokemonChangedEventArgs e)
        {
            _queue.AddSetPokemonEvent(_graphicService, e.Id, e.Pokemon);
        }

        private void NewTurnHandler(object sender, EventArgs e)
        {
            _queue.AddShowMenuEvent(_guiService);
        }

        private void MoveUsedHandler(object sender, MoveUsedEventArgs e)
        {
            _queue.AddShowMessageEvent(_guiService, e.Source.Name + " uses " + e.Move.Name);
        }

        private void MoveEffectiveHandler(object sender, MoveEffectiveEventArgs e)
        {
            switch (e.Effect)
            {
                case MoveEfficiency.NoEffect:
                    _queue.AddShowMessageEvent(_guiService, "It doesn't affect " + e.Target.Name + "...");
                    break;
                case MoveEfficiency.NotEffective:
                    _queue.AddShowMessageEvent(_guiService, "It's not very effective...");
                    break;
                case MoveEfficiency.VeryEffective:
                    _queue.AddShowMessageEvent(_guiService, "It's super effective!");
                    break;
            }
        }

        private void HPChangedHandler(object sender, HpChangedEventArgs e)
        {
            _queue.AddHpEvent(_graphicService, e.Id, e.Hp);
        }

        private void CriticalDamageHandler(object sender, EventArgs e)
        {
            _queue.AddShowMessageEvent(_guiService, "Critical hit!");
        }
    }
}
