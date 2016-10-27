using Base;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Layouts;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonMenuLine : AbstractGraphicComponent
    {
        private Container mainContainer;

        private readonly TextureBox icon;
        private readonly HPLine hpLine;
        private readonly HPText hpText;
        private readonly TextBox nameBox;
        private readonly TextBox hpLabel;
        private readonly TextBox level;
        private readonly TextureProvider textureProvider;

        public PokemonMenuLine(TextureBox icon, HPLine hpLine, TextBox nameBox, HPText hpText, TextBox level,
            TextBox hpLabel, TextureProvider textureProvider)
        {
            mainContainer = new Container();
            this.icon = icon;
            this.hpLine = hpLine;
            this.hpText = hpText;

            this.nameBox = nameBox;
            this.level = level;
            this.hpLabel = hpLabel;
            this.textureProvider = textureProvider;
        }

        public void SetPokemon(Pokemon pokemon)
        {
            hpText.SetPokemon(pokemon);

            hpLine.MaxHP = pokemon.MaxHP;
            hpLine.Current = pokemon.HP;

            nameBox.Text = pokemon.Name;

            level.Text = "L" + pokemon.Level;

            icon.Image = textureProvider.GetIcon(pokemon.Id);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            mainContainer.Draw(time, batch);
        }

        public override void Setup()
        {
            var iconDataContainer = new Container() {Layout = new HBoxLayout()};
            var hpLineContainer = new Container() {Layout = new HBoxLayout()};
            var nameLevelContainer = new Container() {Layout = new HBoxLayout()};
            var dataContainer = new Container() {Layout = new VBoxLayout()};

            hpLabel.Text = "HP:";
            hpLabel.HorizontalPolicy = ResizePolicy.Preferred;
            hpLabel.PreferredTextHeight = 24;


            hpText.HorizontalPolicy = ResizePolicy.Preferred;


            hpLine.VerticalPolicy = ResizePolicy.Fixed;
            hpLine.Height = 24;

            hpLineContainer.AddComponent(hpLabel);
            hpLineContainer.AddComponent(hpLine);
            hpLineContainer.AddComponent(hpText);


            level.HorizontalPolicy = ResizePolicy.Preferred;

            nameLevelContainer.AddComponent(nameBox);
            nameLevelContainer.AddComponent(level);

            dataContainer.AddComponent(nameLevelContainer);
            dataContainer.AddComponent(hpLineContainer);

            icon.VerticalPolicy = icon.HorizontalPolicy = ResizePolicy.Fixed;

            iconDataContainer.AddComponent(icon);
            iconDataContainer.AddComponent(dataContainer);

            mainContainer = iconDataContainer;

            mainContainer.Setup();
        }

        protected override void Update()
        {
            mainContainer.SetCoordinates(this);
            icon.Width = icon.Height = mainContainer.Height;
        }
    }
}