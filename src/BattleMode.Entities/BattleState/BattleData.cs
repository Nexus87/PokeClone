using System;
using System.Collections.Generic;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.TypeRegistry;
using Pokemon.Services.Rules;

namespace BattleMode.Entities.BattleState
{
    [GameService(typeof(BattleData))]
    public class BattleData
    {
        private ClientIdentifier Ai { get; }
        private ClientIdentifier Player { get; }

        public ClientIdentifier PlayerId => Player;

        private readonly Dictionary<ClientIdentifier, ICommand> _commands =
            new Dictionary<ClientIdentifier, ICommand>();

        private readonly List<ClientIdentifier> _clients = new List<ClientIdentifier>();

        public BattleData() :
            this(new ClientIdentifier {IsPlayer = true, Name = "Player"},
                new ClientIdentifier {IsPlayer = false, Name = "Ai"})
        {
        }

        internal BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            Player = player;
            Ai = ai;

            _clients.Add(Ai);
            _clients.Add(Player);

            PlayerPokemon = new PokemonEntity();
            AiPokemon = new PokemonEntity();
        }

        private PokemonEntity AiPokemon { get; }

        public IReadOnlyList<ClientIdentifier> Clients => _clients.AsReadOnly();

        public IEnumerable<ICommand> Commands => _commands.Values;
        private PokemonEntity PlayerPokemon { get; }

        public PokemonEntity GetPokemon(ClientIdentifier id)
        {
            return Equals(id, Player) ? PlayerPokemon : AiPokemon;
        }

        public void ClearCommands()
        {
            _commands.Clear();
        }

        public void SetCommand(ClientIdentifier id, ICommand command)
        {
            if (!_clients.Contains(id))
                throw new InvalidOperationException("ID " + id.Name + "is unknown");

            _commands[id] = command;
        }
    }
}