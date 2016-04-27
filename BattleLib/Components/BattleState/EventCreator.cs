using Base;
using Base.Data;
using BattleLib.Components.BattleState.Commands;
using BattleLib.Events;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.EventComponent;
using GameEngine.Utils;
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

        public EventCreator(BattleData data)
        {
            data.CheckNull("data");
            this.data = data;
        }

        public void Setup(IPokeEngine game)
        {
            game.CheckNull("game");

            eventDispatcher = game.Services.GetService<IEventQueue>();
            graphicService = game.Services.GetService<IBattleGraphicService>();
            guiService = game.Services.GetService<IGUIService>();
        }
        
        internal void UsingMove(PokemonWrapper source, Move move)
        {
            eventDispatcher.AddShowMessageEvent(guiService,source.Name + " uses " + move.Name);
        }

        internal void SetHP(ClientIdentifier id, int hp)
        {
            eventDispatcher.AddHPEvent(graphicService, id, hp);
        }

        internal void Effective(MoveEfficiency effect, PokemonWrapper target)
        {
            switch (effect)
            {
                case MoveEfficiency.NoEffect:
                    eventDispatcher.AddShowMessageEvent(guiService, "It doesn't affect " + target.Name + "...");
                    break;
                case MoveEfficiency.NotEffective:
                    eventDispatcher.AddShowMessageEvent(guiService, "It's not very effective...");
                    break;
                case MoveEfficiency.VeryEffective:
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
