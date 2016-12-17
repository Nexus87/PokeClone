using Base;
using GameEngine;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonMenuLine : AbstractGraphicComponent
    {
        private Grid _mainContainer;

        private readonly TextureBox _icon;
        private readonly HpLine _hpLine;
        private readonly HpText _hpText;
        private readonly TextBox _nameBox;
        private readonly TextBox _hpLabel;
        private readonly TextBox _level;
        private readonly TextureProvider _textureProvider;

        public PokemonMenuLine(TextureBox icon, HpLine hpLine, TextBox nameBox, HpText hpText, TextBox level,
            TextBox hpLabel, TextureProvider textureProvider)
        {
            _mainContainer = new Grid();
            _icon = icon;
            _hpLine = hpLine;
            _hpText = hpText;

            _nameBox = nameBox;
            _level = level;
            _hpLabel = hpLabel;
            _textureProvider = textureProvider;
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _hpText.SetPokemon(pokemon);

            _hpLine.MaxHp = pokemon.MaxHP;
            _hpLine.Current = pokemon.HP;

            _nameBox.Text = pokemon.Name;

            _level.Text = "L" + pokemon.Level;

            _icon.Image = _textureProvider.GetIcon(pokemon.Id);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _mainContainer.Draw(time, batch);
        }

        public override void Setup()
        {
            var iconDataContainer = new Grid();
            var hpLineContainer = new Grid();
            var nameLevelContainer = new Grid();
            var dataContainer = new Grid();

            _hpLabel.Text = "HP:";
            _hpLabel.PreferredTextHeight = 24;

            hpLineContainer.AddPercentRow();
            hpLineContainer.AddAutoColumn();
            hpLineContainer.AddPercentColumn();
            hpLineContainer.AddAutoColumn();
            hpLineContainer.SetComponent(_hpLabel, 0, 0);
            hpLineContainer.SetComponent(_hpLine, 0, 1);
            hpLineContainer.SetComponent(_hpText, 0, 2);


            _level.HorizontalPolicy = ResizePolicy.Preferred;

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

            _icon.VerticalPolicy = _icon.HorizontalPolicy = ResizePolicy.Fixed;

            dataContainer.AddPercentRow();
            dataContainer.AddAutoColumn();
            dataContainer.AddPercentColumn();
            iconDataContainer.SetComponent(_icon, 0, 0);
            iconDataContainer.SetComponent(dataContainer, 0, 1);

            _mainContainer = iconDataContainer;

            _mainContainer.Setup();
        }

        protected override void Update()
        {
            _mainContainer.SetCoordinates(this);
            _icon.SetCoordinates(_icon.Area.X, _icon.Area.Y, _mainContainer.Area.Height, _mainContainer.Area.Height);
        }
    }
}