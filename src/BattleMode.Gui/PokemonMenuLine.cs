using Base;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameType]
    public class PokemonMenuLine : AbstractGuiComponent
    {
        private Grid _mainContainer;

        private readonly ImageBox _icon;
        private readonly HpLine _hpLine;
        private readonly HpText _hpText;
        private readonly Label _nameBox;
        private readonly Label _hpLabel;
        private readonly Label _level;
        private readonly SpriteProvider _spriteProvider;

        public PokemonMenuLine(ImageBox icon, HpLine hpLine, Label nameBox, HpText hpText, Label level,
            Label hpLabel, SpriteProvider spriteProvider)
        {
            _mainContainer = new Grid();
            _icon = icon;
            _hpLine = hpLine;
            _hpText = hpText;

            _nameBox = nameBox;
            _level = level;
            _hpLabel = hpLabel;
            _spriteProvider = spriteProvider;

            Init();
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _hpText.SetPokemon(pokemon);

            _hpLine.MaxHp = pokemon.MaxHP;
            _hpLine.Current = pokemon.HP;

            _nameBox.Text = pokemon.Name;

            _level.Text = "L" + pokemon.Level;

            _icon.Image = _spriteProvider.GetIcon(pokemon.Id);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _mainContainer.Draw(time, batch);
        }

        public void Init()
        {
            var iconDataContainer = new Grid();
            var hpLineContainer = new Grid();
            var nameLevelContainer = new Grid();
            var dataContainer = new Grid();

            _hpLabel.Text = "HP:";
            _hpLabel.TextSize = 24;

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

        protected override void Update()
        {
            _mainContainer.SetCoordinates(this);
            _icon.PreferredHeight = _mainContainer.Area.Height;
            _icon.PreferredWidth = _mainContainer.Area.Height;
        }
    }
}