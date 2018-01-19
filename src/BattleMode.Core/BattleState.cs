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

            var mainMenuController = new MainMenuController(GuiSystem);
            var moveMenuController = new MoveMenuController(GuiSystem, data, GuiSystem.Factory);

            var pokemonMenuController = new PokemonMenuController(GuiSystem, _player, GuiSystem.Factory);
            var itemMenuController = new ItemMenuController(GuiSystem, GuiSystem.Factory);
            var playerPokemonDataView = new PlayerPokemonDataView(GuiSystem, GuiSystem.Factory);
            var aiPokemonDataView = new AiPokemonDataView(GuiSystem);
            _guiController = new GuiController(GuiSystem, mainMenuController, moveMenuController,
                pokemonMenuController, itemMenuController, playerPokemonDataView, aiPokemonDataView, data);
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