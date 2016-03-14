using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public class BattleData
    {
        ClientIdentifier Ai { get; set; }
        ClientIdentifier Player { get; set; }

        private Dictionary<ClientIdentifier, ICommand> commands = new Dictionary<ClientIdentifier, ICommand>();
        private List<ClientIdentifier> clients = new List<ClientIdentifier>();

        public BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            this.Player = player;
            this.Ai = ai;

            clients.Add(ai);
            clients.Add(player);

            PlayerPokemon = new PokemonWrapper(player);
            AiPokemon = new PokemonWrapper(ai);
        }

        PokemonWrapper AiPokemon { get; set; }

        public IReadOnlyList<ClientIdentifier> Clients { get { return clients.AsReadOnly(); } }
        public IEnumerable<ICommand> Commands { get { return commands.Values; } }
        PokemonWrapper PlayerPokemon { get; set; }

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