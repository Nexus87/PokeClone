using BattleMode.Entities.BattleState;
using GameEngine.Core;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.Pokemon.Gui;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class AiPokemonDataView : IPokemonDataView
    {
        private readonly GuiManager _guiManager;
#pragma warning disable 649
        [GuiLoaderId("Window")] private Grid _grid;
        [GuiLoaderId("HpLine")] private HpLine _hpLine;
        [GuiLoaderId("LevelLabel")] private Label _lvl;
        [GuiLoaderId("Name")] private Label _name;
#pragma warning restore 649


        public AiPokemonDataView(GuiManager _guiManager)
        {
            this._guiManager = _guiManager;
            var loader = new GuiLoader(@"BattleMode\Gui\AiDataView.xml") {Controller = this};
            loader.Load();
        }

        public int CurrentHp => _hpLine.Current;

        public void SetHp(int newHp)
        {
            _hpLine.Current = newHp;
        }

        public void SetPokemon(PokemonWrapper pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpLine.MaxHp = pokemon.MaxHP;
            _hpLine.Current = pokemon.HP;
        }

        public void Show()
        {
            _guiManager.ShowWidget(_grid, -100);
        }
    }
}