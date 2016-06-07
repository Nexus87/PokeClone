﻿using Base;
using Base.Data;
using System;
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
            clients.Clear();

            foreach(var id in data.Clients)
            {
                PokemonWrapper pkmn = data.GetPokemon(id);
                if (pkmn.Pokemon == null || pkmn.Condition == StatusCondition.KO)
                    clients[id] = null;
            }

            clientsLeft = clients.Count;
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
