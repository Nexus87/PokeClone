using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using Base.Data;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState.States
{
    [GameService(typeof(WaitForPokemonState))]
    public class WaitForPokemonState : AbstractState
    {
        private readonly IEventCreator _eventCreator;

        private Dictionary<ClientIdentifier, Pokemon> _clients = new Dictionary<ClientIdentifier, Pokemon>();

        public WaitForPokemonState(IEventCreator eventCreator)
        {
            _eventCreator = eventCreator;
        }

        public override BattleStates State => BattleStates.WaitForPokemon;

        public override void Init(BattleData data)
        {
            _clients = data.Clients
                .Where(id => NeedsPokemon(data, id))
                .ToDictionary(id => id, id => (Pokemon)null);

            IsDone = !_clients.Any();
        }

        public override void Update(BattleData data)
        {
            if (_clients.Any(x => x.Value == null))
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
        }

        private static bool NeedsPokemon(BattleData data, ClientIdentifier id)
        {
            var pkmn = data.GetPokemon(id);
            return pkmn.Pokemon == null || pkmn.Condition == StatusCondition.KO;
        }
    }
}