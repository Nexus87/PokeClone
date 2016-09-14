using Base;
using Base.Data;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleLib.Components.BattleState
{
    [GameType]
    public class WaitForCharState : AbstractState
    {
        private readonly IEventCreator eventCreator;

        private int clientsLeft;
        private Dictionary<ClientIdentifier, Pokemon> clients = new Dictionary<ClientIdentifier, Pokemon>();

        public WaitForCharState(IEventCreator eventCreator)
        {
            this.eventCreator = eventCreator;
        }

        public override BattleStates State
        {
            get { return BattleStates.WaitForPokemon; }
        }

        public override void Init(BattleData data)
        {
            IsDone = false;

            clients = data.Clients
                .Where(id => NeedsPokemon(data, id))
                .ToDictionary(id => id, id => (Pokemon)null);

            clientsLeft = clients.Count;

            IsDone = (clientsLeft == 0);
        }

        public override void Update(BattleData data)
        {
            if (clientsLeft != 0)
                return;

            foreach (var c in clients)
            {
                var pokemon = data.GetPokemon(c.Key);
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

        private bool NeedsPokemon(BattleData data, ClientIdentifier id)
        {
            var pkmn = data.GetPokemon(id);
            return pkmn.Pokemon == null || pkmn.Condition == StatusCondition.KO;
        }
    }
}