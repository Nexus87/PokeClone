using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using Base.Data;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState
{
    [GameService(typeof(WaitForCharState))]
    public class WaitForCharState : AbstractState
    {
        private readonly IEventCreator _eventCreator;

        private int _clientsLeft;
        private Dictionary<ClientIdentifier, Pokemon> _clients = new Dictionary<ClientIdentifier, Pokemon>();

        public WaitForCharState(IEventCreator eventCreator)
        {
            _eventCreator = eventCreator;
        }

        public override BattleStates State => BattleStates.WaitForPokemon;

        public override void Init(BattleData data)
        {
            IsDone = false;

            _clients = data.Clients
                .Where(id => NeedsPokemon(data, id))
                .ToDictionary(id => id, id => (Pokemon)null);

            _clientsLeft = _clients.Count;

            IsDone = (_clientsLeft == 0);
        }

        public override void Update(BattleData data)
        {
            if (_clientsLeft != 0)
                return;

            foreach (var c in _clients)
            {
                var pokemon = data.GetPokemon(c.Key);
                pokemon.Pokemon = c.Value;
                _eventCreator.SetPokemon(c.Key, pokemon);
            }

            IsDone = true;
        }

        public override void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            if (!_clients.ContainsKey(id))
                throw new InvalidOperationException("ClientIdentifier " + id.Name + " not found.");

            if (_clients[id] != null)
                throw new InvalidOperationException("ClientIdentifer " + id.Name + " has already set its char");

            _clients[id] = pkmn;
            _clientsLeft--;
        }

        private bool NeedsPokemon(BattleData data, ClientIdentifier id)
        {
            var pkmn = data.GetPokemon(id);
            return pkmn.Pokemon == null || pkmn.Condition == StatusCondition.KO;
        }
    }
}