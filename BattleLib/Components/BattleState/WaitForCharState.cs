using Base;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public class WaitForCharState : AbstractState
    {
        BattleStateComponent state;
        EventCreator eventCreator;

        int clientsLeft;
        Dictionary<ClientIdentifier, Pokemon> clients = new Dictionary<ClientIdentifier, Pokemon>();

        public void Init(IEnumerable<ClientIdentifier> requestedClients)
        {
            clients.Clear();

            foreach (var c in requestedClients)
                clients[c] = null;

            clientsLeft = clients.Count;
        }
        
        public WaitForCharState(BattleStateComponent state, ClientIdentifier player, ClientIdentifier ai, EventCreator eventCreator)
        {
            this.state = state;
            this.eventCreator = eventCreator;
        }


        public override IBattleState Update(BattleData data)
        {
            if (clientsLeft != 0)
                return this;

            foreach (var c in clients)
            {
                PokemonWrapper pokemon = data.GetPkmn(c.Key);
                pokemon.Pokemon = c.Value;
                eventCreator.SetPokemon(c.Key, pokemon);
            }

            state.actionState.Init(clients.Keys);
            return state.actionState;
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
