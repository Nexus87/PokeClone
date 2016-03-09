using Base;
using BattleLib.Components.BattleState.Commands;
using BattleLib.Events;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public class EventCreator
    {
        
        private IEventQueue eventDispatcher;
        private IBattleGraphicService graphicService;
        private IGUIService guiService;

        private BattleData data;
        private PokemonWrapper playerPkmn;
        private PokemonWrapper aiPkmn;

        public EventCreator(BattleData data)
        {
            this.data = data;
            playerPkmn = data.PlayerPkmn;
            aiPkmn = data.AIPkmn;
        }

        public void Setup(Game game)
        {
            eventDispatcher = game.Services.GetService<IEventQueue>();
            graphicService = game.Services.GetService<IBattleGraphicService>();
            guiService = game.Services.GetService<IGUIService>();
        }
        
        internal void UsingMove(PokemonWrapper source, Move move)
        {
            eventDispatcher.AddShowMessageEvent(guiService,source.Name + " uses " + move.Data.Name);
        }

        internal void SetHP(ClientIdentifier id, int hp)
        {
            eventDispatcher.AddHPEvent(graphicService, id, hp);
        }

        internal void Effective(MoveEfficency effect, PokemonWrapper target)
        {
            switch (effect)
            {
                case MoveEfficency.noEffect:
                    eventDispatcher.AddShowMessageEvent(guiService, "It doesn't affect " + target.Name + "...");
                    break;
                case MoveEfficency.notEffective:
                    eventDispatcher.AddShowMessageEvent(guiService, "It's not very effective...");
                    break;
                case MoveEfficency.veryEffective:
                    eventDispatcher.AddShowMessageEvent(guiService, "It's super effective!");
                    break;
            }
        }

        internal void Critical()
        {
            eventDispatcher.AddShowMessageEvent(guiService, "Critical hit!");
        }

        internal void SetStatus(StatusCondition condition)
        {
            
            throw new NotImplementedException();
        }

        internal void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            eventDispatcher.AddSetPokemonEvent(graphicService, id, pokemon);
        }

        internal void NewTurn()
        {
            eventDispatcher.AddShowMenuEvent(guiService);
        }
    }
}
