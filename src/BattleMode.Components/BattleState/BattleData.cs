using System;
using System.Collections.Generic;
using BattleMode.Components.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Components.BattleState
{
    [GameService(typeof(BattleData))]
    public class BattleData
    {
        private ClientIdentifier Ai { get; }
        private ClientIdentifier Player { get; }

        public ClientIdentifier PlayerId => Player;

        private readonly Dictionary<ClientIdentifier, ICommand> _commands = new Dictionary<ClientIdentifier, ICommand>();
        private readonly List<ClientIdentifier> _clients = new List<ClientIdentifier>();

        public BattleData() :
            this(new ClientIdentifier { IsPlayer = true, Name = "Player" }, new ClientIdentifier { IsPlayer = false, Name = "Ai" })
        {}

        internal BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            Player = player;
            Ai = ai;

            _clients.Add(Ai);
            _clients.Add(Player);

            PlayerPokemon = new PokemonWrapper(Player);
            AiPokemon = new PokemonWrapper(Ai);
        }

        private PokemonWrapper AiPokemon { get; }

        public IReadOnlyList<ClientIdentifier> Clients => _clients.AsReadOnly();
        public IEnumerable<ICommand> Commands => _commands.Values;
        private PokemonWrapper PlayerPokemon { get; }

        public PokemonWrapper GetPokemon(ClientIdentifier id)
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