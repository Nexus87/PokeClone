using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.Pokemon.Gui;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class PlayerPokemonDataView : IPokemonDataView
    {
        private readonly GuiManager _guiManager;

#pragma warning disable 649
        [GuiLoaderId("Window")] private Grid _grid;
        [GuiLoaderId("HpLine")] private HpLine _hpLine;
        [GuiLoaderId("LevelLabel")] private Label _lvl;
        [GuiLoaderId("Name")] private Label _name;
        [GuiLoaderId("HpText")] private HpText _hpText;
#pragma warning restore 649

        public PlayerPokemonDataView(GuiManager guiManager)
        {
            _guiManager = guiManager;

            var loader = new GuiLoader(@"BattleMode\Gui\PlayerDataView.xml") {Controller = this};
            loader.Load();
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
            _guiManager.ShowWidget(_grid, -100);
        }
    }
}