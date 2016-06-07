using Base;
using Base.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public class WaitForCharState : AbstractState
    {
        IEventCreator eventCreator;

        int clientsLeft;
        Dictionary<ClientIdentifier, Pokemon> clients = new Dictionary<ClientIdentifier, Pokemon>();

        public override void Init(BattleData data)
        {
            IsDone = false;

            clients =  data.Clients
                .Where( id => NeedsPokemon(data, id))
                .ToDictionary( id => id, id => (Pokemon) null);

            clientsLeft = clients.Count;

            if (clientsLeft == 0)
                IsDone = true;
        }

        private bool NeedsPokemon(BattleData data, ClientIdentifier id)
        {
             PokemonWrapper pkmn = data.GetPokemon(id);
             return pkmn.Pokemon == null || pkmn.Condition == StatusCondition.KO;
        }
        
        public WaitForCharState(IEventCreator eventCreator)
        {
            this.eventCreator = eventCreator;
        }


        public override void Update(BattleData data)
        {
            if (clientsLeft != 0)
                return;

            foreach (var c in clients)
            {
                PokemonWrapper pokemon = data.GetPokemon(c.Key);
                pokemon.Pokemon = c.Value;
                eventCreator.SetPokemon(c.Key, pokemon);
            }

            IsDone = true;
        }

        public override void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            if (!clients.ContainsKey(id))
                throw new InvalidOperationException("ClientIdentifier " + id.Name + " not found.");

            if (clients[id] != null)
                throw new InvalidOperationException("ClientIdentifer " + id.Name + " has already set its char");

            clients[id] = pkmn;
            clientsLeft--;
        }

        public override BattleStates State
        {
            get { return BattleStates.WaitForPokemon; }
        }
    }
}
