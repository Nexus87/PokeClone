using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Gui;
using PokemonShared.Models;
using PokemonShared.Service;

namespace BattleMode.Gui
{
    public class PokemonMenuLineController
    {
        private readonly SpriteProvider _spriteProvider;
#pragma warning disable 649
        [GuiLoaderId("main")]
        private Panel _mainContainer;
        [GuiLoaderId("icon")]
        private readonly ImageBox _icon;
        [GuiLoaderId("hpLine")]
        private readonly HpLine _hpLine;
        [GuiLoaderId("hpText")]

        private readonly HpText _hpText;
        [GuiLoaderId("name")]
        private readonly Label _nameBox;

        [GuiLoaderId("level")]

        private readonly Label _level;
#pragma warning restore 649

        private Pokemon _pokemon;
        public IGuiComponent Component => _mainContainer;
        public PokemonMenuLineController(GuiFactory factory, SpriteProvider spriteProvider)
        {
            _spriteProvider = spriteProvider;
            factory.LoadFromFile(@"BattleMode\Gui\PkmnMenuLine.xml", this);
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _pokemon = pokemon;
            _hpText.SetPokemon(_pokemon);
            _hpLine.MaxHp = _pokemon.MaxHp;
            _nameBox.Text = _pokemon.Name;
            _icon.Image = _spriteProvider.GetIcon(_pokemon.Id);
            UpdateData();
        }

        public void UpdateData()
        {
            _hpLine.Current = _pokemon.Hp;
            _level.Text = "L" + _pokemon.Level;

        }
    }
}