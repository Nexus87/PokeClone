using Base;
using BattleLib.Components.BattleState.Commands;
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

            state.OnCommandStarted += CommandStartHandler;
            state.OnCommandFinished += CommandFinishedHandler;

            rules.OnDamageTaken += DamageTakenHandler;
            rules.OnConditionChanged += ConditionChangedHandler;
            rules.OnActionFailed += ActionFailedHandler;
            rules.OnStatsChanged += StatsChangedHandler;
            rules.ItemUsed += ItemUsedHandler;
            rules.MoveUsed += MoveUsedHandler;
        }

        private void MoveUsedHandler(object sender, MoveUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void ItemUsedHandler(object sender, ItemUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void StatsChangedHandler(object sender, OnStatsChangedArgs e)
        {
            throw new NotImplementedException();
        }

        private void ActionFailedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ConditionChangedHandler(object sender, OnConditionChangedArgs e)
        {
            throw new NotImplementedException();
        }

        private void DamageTakenHandler(object sender, OnDamageTakenArgs e)
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
