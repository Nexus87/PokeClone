using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;
using PokemonShared.Gui;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class PokemonMenuLineController
    {
#pragma warning disable 649
        [GuiLoaderId("main")]
        private Grid _mainContainer;
        [GuiLoaderId("icon")]
        private readonly ImageBox _icon;
        [GuiLoaderId("hpLine")]
        private readonly HpLine _hpLine;
        [GuiLoaderId("hpText")]

        private readonly HpText _hpText;
        [GuiLoaderId("name")]
        private readonly Label _nameBox;
        [GuiLoaderId("hpLabel")]
        private readonly Label _hpLabel;
        [GuiLoaderId("level")]

        private readonly Label _level;
#pragma warning restore 649

        private readonly IGuiComponent _nullComponent = new Spacer();
        //    private readonly SpriteProvider _spriteProvider;
        private Pokemon _pokemon;
        public IGuiComponent Component => _mainContainer;
        public PokemonMenuLineController(GuiFactory factory/*, SpriteProvider spriteProvider*/)
        {
            factory.LoadFromFile(@"BattleMode\Gui\PkmnMenuLine.xml", this);

            Init();
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _pokemon = pokemon;
            _hpText.SetPokemon(_pokemon);
            _hpLine.MaxHp = _pokemon.MaxHp;
            _nameBox.Text = _pokemon.Name;
            // _icon.Image = _spriteProvider.GetIcon(_pokemon.Id);
            UpdateData();
        }

        public void UpdateData()
        {
            _hpLine.Current = _pokemon.Hp;
            _level.Text = "L" + _pokemon.Level;

        }

        public void Init()
        {
            var iconDataContainer = new Grid();
            var hpLineContainer = new Grid();
            var nameLevelContainer = new Grid();
            var dataContainer = new Grid();

            _hpLabel.Text = "HP:";
            _hpLabel.TextHeight = 24;

            hpLineContainer.AddPercentRow();
            hpLineContainer.AddAutoColumn();
            hpLineContainer.AddPercentColumn();
            hpLineContainer.AddAutoColumn();
            hpLineContainer.SetComponent(_hpLabel, 0, 0);
            hpLineContainer.SetComponent(_hpLine, 0, 1);
            hpLineContainer.SetComponent(_hpText, 0, 2);


            nameLevelContainer.AddPercentRow();
            nameLevelContainer.AddPercentColumn();
            nameLevelContainer.AddAutoColumn();
            nameLevelContainer.SetComponent(_nameBox, 0, 0);
            nameLevelContainer.SetComponent(_level, 0, 1);


            dataContainer.AddPercentColumn();
            dataContainer.AddPercentRow();
            dataContainer.AddPercentRow();
            dataContainer.SetComponent(nameLevelContainer, 0, 0);
            dataContainer.SetComponent(hpLineContainer, 1, 0);

            iconDataContainer.AddPercentRow();
            iconDataContainer.AddAutoColumn();
            iconDataContainer.AddPercentColumn();
            iconDataContainer.SetComponent(_icon, 0, 0);
            iconDataContainer.SetComponent(dataContainer, 0, 1);

            _mainContainer = iconDataContainer;
        }

        // protected override void Update()
        // {
        //     _mainContainer.SetCoordinates(this);
        //     _icon.PreferredHeight = _mainContainer.Area.Height;
        //     _icon.PreferredWidth = _mainContainer.Area.Height;
        // }
    }
}