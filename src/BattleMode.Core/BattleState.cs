using System.Linq;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core.GameStates;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;

namespace BattleMode.Core
{
    public class BattleState : State
    {
        private Client _player;
        private GuiController _guiController;

        public string ModuleName => "BattleMode";

        protected override void Init()
        {
            var data = new BattleData();
            var playerId = data.PlayerId;
            var aiId = data.Clients.First(id => !id.IsPlayer);
            _player = new Client(playerId);
            var ai = new Client(aiId);

            var mainMenuController = new MainMenuController(GuiSystem, MessageBus);
            var moveMenuController = new MoveMenuController(GuiSystem, data, GuiSystem.Factory, MessageBus);

            var pokemonMenuController = new PokemonMenuController(GuiSystem, _player, GuiSystem.Factory, MessageBus);
            var itemMenuController = new ItemMenuController(GuiSystem, GuiSystem.Factory, MessageBus);
            var playerPokemonDataView = new PlayerPokemonDataView(GuiSystem, GuiSystem.Factory, MessageBus);
            var aiPokemonDataView = new AiPokemonDataView(GuiSystem, MessageBus);
            _guiController = new GuiController(GuiSystem, mainMenuController, moveMenuController,
                pokemonMenuController, itemMenuController, playerPokemonDataView, aiPokemonDataView, MessageBus, data);
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }

        public override void Stop()
        {
        }
    }
}