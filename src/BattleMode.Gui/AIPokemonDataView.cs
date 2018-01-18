using BattleMode.Shared;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Gui;

namespace BattleMode.Gui
{
    public class AiPokemonDataView : IPokemonDataView
    {
        private readonly GuiSystem _guiManager;
#pragma warning disable 649
        [GuiLoaderId("Window")] private Grid _grid;
        [GuiLoaderId("HpLine")] private HpLine _hpLine;
        [GuiLoaderId("LevelLabel")] private Label _lvl;
        [GuiLoaderId("Name")] private Label _name;
#pragma warning restore 649


        public AiPokemonDataView(GuiSystem guiManager, GuiFactory factory)
        {
            _guiManager = guiManager;
            factory.LoadFromFile(@"BattleMode\Gui\AiDataView.xml", this);
        }

        public int CurrentHp => _hpLine.Current;

        public void SetHp(int newHp)
        {
            _hpLine.Current = newHp;
        }

        public void SetPokemon(PokemonEntity pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpLine.MaxHp = pokemon.MaxHp;
            _hpLine.Current = pokemon.Hp;
        }

        public void Show()
        {
            _guiManager.ShowWidget(_grid, -100);
        }
    }
}