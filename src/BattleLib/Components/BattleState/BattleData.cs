using System;
using System.Collections.Generic;
using BattleMode.Core.Components.BattleState.Commands;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.BattleState
{
    [GameService(typeof(BattleData))]
    public class BattleData
    {
        private ClientIdentifier Ai { get; set; }
        private ClientIdentifier Player { get; set; }

        public ClientIdentifier PlayerId { get { return Player; } }

        private readonly Dictionary<ClientIdentifier, ICommand> commands = new Dictionary<ClientIdentifier, ICommand>();
        private readonly List<ClientIdentifier> clients = new List<ClientIdentifier>();

        public BattleData() :
            this(new ClientIdentifier { IsPlayer = true, Name = "Player" }, new ClientIdentifier { IsPlayer = false, Name = "Ai" })
        {}
        internal BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            Player = player;
            Ai = ai;

            clients.Add(Ai);
            clients.Add(Player);

            PlayerPokemon = new PokemonWrapper(Player);
            AiPokemon = new PokemonWrapper(Ai);
        }

        private PokemonWrapper AiPokemon { get; set; }

        public IReadOnlyList<ClientIdentifier> Clients { get { return clients.AsReadOnly(); } }
        public IEnumerable<ICommand> Commands { get { return commands.Values; } }
        private PokemonWrapper PlayerPokemon { get; set; }

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return id == Player ? PlayerPokemon : AiPokemon;
        }

        public void ClearCommands()
        {
            commands.Clear();
        }
        public void SetCommand(ClientIdentifier id, ICommand command)
        {
            if (!clients.Contains(id))
                throw new InvalidOperationException("ID " + id.Name + "is unknown");

            commands[id] = command;
        }
    }
}