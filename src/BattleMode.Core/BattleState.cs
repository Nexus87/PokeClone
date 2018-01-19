using System.Linq;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.GameStates;

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

            var mainMenuController = new MainMenuController(GuiSystem.Factory, MessageBus);
            var moveMenuController = new MoveMenuController(data, GuiSystem.Factory, MessageBus);

            var pokemonMenuController = new PokemonMenuController(_player, GuiSystem.Factory, MessageBus);
            var itemMenuController = new ItemMenuController(GuiSystem.Factory, MessageBus);
            var playerPokemonDataView = new PokemonDataView(GuiSystem.Factory, @"BattleMode\Gui\PlayerDataView.xml", MessageBus);
            var aiPokemonDataView = new PokemonDataView(GuiSystem.Factory, @"BattleMode\Gui\AiDataView.xml", MessageBus);
            _guiController = new GuiController(GuiSystem, mainMenuController, moveMenuController,
                pokemonMenuController, itemMenuController, playerPokemonDataView, aiPokemonDataView, MessageBus, data);

            MessageBus.SendAction(new SetGuiVisibleAction(true));
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