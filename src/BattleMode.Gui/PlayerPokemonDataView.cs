using BattleMode.Shared;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Gui;

namespace BattleMode.Gui
{
    public class PlayerPokemonDataView : IPokemonDataView
    {
        private readonly GuiSystem _guiManager;
        private readonly IMessageBus _messageBus;


#pragma warning disable 649
        [GuiLoaderId("Window")] private Grid _grid;
        [GuiLoaderId("HpLine")] private HpLine _hpLine;
        [GuiLoaderId("LevelLabel")] private Label _lvl;
        [GuiLoaderId("Name")] private Label _name;
        [GuiLoaderId("HpText")] private HpText _hpText;
#pragma warning restore 649

        public PlayerPokemonDataView(GuiSystem guiManager, GuiFactory factory, IMessageBus messageBus)
        {
            _guiManager = guiManager;
            _messageBus = messageBus;

            factory.LoadFromFile(@"BattleMode\Gui\PlayerDataView.xml", this);
        }

        public int CurrentHp => _hpLine.Current;

        public void SetHp(int newHp)
        {
            _hpLine.Current = _hpText.CurrentHp = newHp;
        }

        public void SetPokemon(PokemonEntity pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpText.MaxHp = _hpLine.MaxHp = pokemon.MaxHp;
            SetHp(pokemon.Hp);

        }

        public void Show()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction { Widget = _grid, IsVisble = true, Priority = -100 });

        }
    }
}