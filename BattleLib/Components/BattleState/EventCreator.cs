using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    class StateChangedEvent : BattleEvent
    {

        public event EventHandler OnEventProcessed;

        public void Dispatch()
        {
            throw new NotImplementedException();
        }
    }

    public class EventCreator
    {
        private BattleEventDispatcher eventDispatcher;
        private BattleData data;
        private PokemonWrapper playerPkmn;
        private PokemonWrapper aiPkmn;

        public EventCreator(BattleData data)
        {
            playerPkmn = data.PlayerPkmn;
            aiPkmn = data.AIPkmn;

            playerPkmn.OnConditionChanged += OnConditionChanged;
            playerPkmn.OnPokemonChanged += OnPokemonChanged;
            playerPkmn.OnStateChanged += OnStateChanged;

            aiPkmn.OnConditionChanged += OnConditionChanged;
            aiPkmn.OnPokemonChanged += OnPokemonChanged;
            aiPkmn.OnStateChanged += OnStateChanged;
        }

        void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnPokemonChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnConditionChanged(object sender, ConditionChangedEventArgs e)
        {
            
        }
    }
}
