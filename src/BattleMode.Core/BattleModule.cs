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
    public class BattleModule : State
    {
        private Client _player;
        private GuiController _guiController;

        public string ModuleName => "BattleMode";

        public virtual void Start()
        {
            //_battleState = registry.ResolveType<IBattleStateService>();
            //_graphic = registry.ResolveType<IBattleGraphicController>();
            //_aiEntity = new AiEntity(_battleState, ai, playerId);
            //// Needs to be created since no other class depend on it.
            //registry.ResolveType<BattleEventProcessor>();


            //componentManager.AddGameComponent(_aiEntity);
            //componentManager.AddGameComponent(_battleState);
            //componentManager.AddGameComponent(registry.ResolveType<IGuiController>());
            //componentManager.Scene = _graphic.Scene;

            //_battleState.SetCharacter(_player.Id, _player.Pokemons.First());

            //registry.ResolveType<GuiManager>().Show();
        }

        //public virtual void Stop()
        //{
        //componentManager.RemoveGameComponent(_aiEntity);
        //componentManager.RemoveGameComponent(_battleState);
        //componentManager.Scene = null;

        //_battleState = null;
        //_graphic = null;
        //_aiEntity = null;
        //}

        protected override void Init()
        {
            var data = new BattleData();
            var playerId = data.PlayerId;
            var aiId = data.Clients.First(id => !id.IsPlayer);
            _player = new Client(playerId);
            var ai = new Client(aiId);
            var messageBox = new MessageBox(
                new Window((WindowRenderer) Skin.GetRendererForComponent(typeof(Window))),
                new TextArea(
                    (TextAreaRenderer) Skin.GetRendererForComponent(typeof(TextAreaRenderer)),
                    new DefaultTextSplitter()
                )
            );

            var mainMenuController = new MainMenuController(
                GuiSystem,
                new Button((ButtonRenderer) Skin.GetRendererForComponent(typeof(Button))),
                new Button((ButtonRenderer) Skin.GetRendererForComponent(typeof(Button))),
                new Button((ButtonRenderer) Skin.GetRendererForComponent(typeof(Button))),
                new Button((ButtonRenderer) Skin.GetRendererForComponent(typeof(Button)))
            );

            var moveMenuController = new MoveMenuController(GuiSystem, data, Skin);

            var pokemonMenuController = new PokemonMenuController(GuiSystem, _player, Skin);
            var itemMenuController = new ItemMenuController(GuiSystem);
            var playerPokemonDataView = new PlayerPokemonDataView(GuiSystem);
            var aiPokemonDataView = new AiPokemonDataView(GuiSystem);
            _guiController = new GuiController(Screen.Constants, GuiSystem, messageBox, mainMenuController, moveMenuController,
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