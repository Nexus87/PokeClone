using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Components.GraphicComponents;
using GameEngine.GameEngineComponents;

namespace BattleLib.Components
{
    [GameService(typeof(BattleEventProcessor))]
    public class BattleEventProcessor
    {
        private readonly IGUIService guiService;
        private readonly IBattleGraphicController graphicService;
        private readonly IEventQueue queue;

        public BattleEventProcessor(IGUIService guiService, IBattleGraphicController graphicService, IEventQueue queue, IEventCreator events)
        {
            this.guiService = guiService;
            this.graphicService = graphicService;
            this.queue = queue;

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
            queue.AddStatusEvent(graphicService, e.ID, e.Status);
            queue.AddShowMessageEvent(guiService, "Status changed to: " + e.Status);
        }

        private void PokemonChangedHandler(object sender, ClientPokemonChangedEventArgs e)
        {
            queue.AddSetPokemonEvent(graphicService, e.ID, e.Pokemon);
        }

        private void NewTurnHandler(object sender, EventArgs e)
        {
            queue.AddShowMenuEvent(guiService);
        }

        private void MoveUsedHandler(object sender, MoveUsedEventArgs e)
        {
            queue.AddShowMessageEvent(guiService, e.Source.Name + " uses " + e.Move.Name);
        }

        private void MoveEffectiveHandler(object sender, MoveEffectiveEventArgs e)
        {
            switch (e.Effect)
            {
                case MoveEfficiency.NoEffect:
                    queue.AddShowMessageEvent(guiService, "It doesn't affect " + e.Target.Name + "...");
                    break;
                case MoveEfficiency.NotEffective:
                    queue.AddShowMessageEvent(guiService, "It's not very effective...");
                    break;
                case MoveEfficiency.VeryEffective:
                    queue.AddShowMessageEvent(guiService, "It's super effective!");
                    break;
            }
        }

        private void HPChangedHandler(object sender, HPChangedEventArgs e)
        {
            queue.AddHPEvent(graphicService, e.ID, e.HP);
        }

        private void CriticalDamageHandler(object sender, EventArgs e)
        {
            queue.AddShowMessageEvent(guiService, "Critical hit!");
        }
    }
}
