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
    class AttackDone 
    {
        string target;
        string source;

        bool successful;
        string reason;

        bool damageDone;
        int damage;

        string additionalMessage;

        bool specialEffect;
        List<string> effects;

        bool statusChanged;
        StatusCondition newCondition;
    }

    class Record
    {
        string actionString;
        CommandType type;
        bool successful;
        string reason;

        List<AttackDone> done;

    }

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

        public void Setup(Game game, IBattleRules rules, ExecuteState state)
        {
            eventDispatcher = game.Services.GetService<IEventQueue>();
            graphicService = game.Services.GetService<IBattleGraphicService>();
            guiService = game.Services.GetService<IGUIService>();

            state.OnCommandStarted += CommandStartHandler;
            state.OnCommandFinished += CommandFinishedHandler;

            rules.ChangeUsed += ChangeUsedHandler;
            rules.ItemUsed += ItemUsedHandler;
            rules.MoveUsed += MoveUsedHandler;
        }

        private void ChangeUsedHandler(object sender, ChangeUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void MoveUsedHandler(object sender, MoveUsedArgs e)
        {
            var message = e.source.IsPlayer ? "" : "Enemy " + data.GetPkmn(e.source).Name + " uses " + e.move.Data.Name;

            eventDispatcher.AddEvent(new ShowMessageEvent(guiService, message));

            var effectsByTarget = e.effects.GroupBy( effect => effect.target);

            foreach (var t in effectsByTarget)
            {
                var sorted = t.OrderBy(effect => !effect.damage).ThenBy(effect => !effect.stateChanged);
                foreach (var s in sorted)
                    HandleEffect(s);
            }
        }

        private void HandleEffect(MoveEffect s)
        {
            if (s.damage)
            {
                eventDispatcher.AddEvent(new SetHPEvent(graphicService, s.target.Identifier.IsPlayer, s.target.HP));
                
                if (s.critical)
                    eventDispatcher.AddEvent(new ShowMessageEvent(guiService, "Critical Hit"));
                
                if (s.effective != MoveEfficency.normal)
                    eventDispatcher.AddEvent(new ShowMessageEvent(guiService, s.effective == MoveEfficency.veryEffective ?
                        "Very effective" : "Not very effective"));
                return;
            }

            if (s.stateChanged)
            {
                eventDispatcher.AddEvent(new ShowMessageEvent(guiService, s.target.Name + "' s" + s.state +
                    (s.lowered ? " was lowered" : "rises")));
                return;
            }

            if (s.conditionChanged)
            {
                eventDispatcher.AddEvent(new ShowMessageEvent(guiService, s.target + " is " + s.target.Condition));
                return;
            }
        }

        private void ItemUsedHandler(object sender, ItemUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void CommandFinishedHandler(object sender, ExecutionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CommandStartHandler(object sender, ExecutionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
