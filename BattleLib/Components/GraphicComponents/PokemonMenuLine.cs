using Base;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonMenuLine : AbstractGraphicComponent
    {
        Container mainContainer;
        TextureProvider provider;

        TextureBox icon;
        private HPLine hpLine;
        private HPText hpText;
        private TextBox nameBox;
        private TextBox hpLabel;

        public PokemonMenuLine(TextureBox icon, HPLine hpLine, TextBox nameBox, HPText hpText, TextBox level, TextBox hpLabel, TextureProvider textureProvider)
        {
            mainContainer = new Container();
            this.provider = textureProvider;
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

            var iconDataContainer = new Container() { Layout = new HBoxLayout() };
            var hpLineContainer = new Container() { Layout = new HBoxLayout() };
            var nameLevelContainer = new Container() { Layout = new HBoxLayout() };
            var dataContainer = new Container() { Layout = new VBoxLayout() };

            hpLabel.Text = "HP:";
            hpLabel.HorizontalPolicy = ResizePolicy.Preferred;

            hpText.SetPokemon(pokemon);
            hpText.HorizontalPolicy = ResizePolicy.Preferred;

            hpLine.MaxHP = pokemon.MaxHP;
            hpLine.Current = pokemon.HP;

            hpLineContainer.AddComponent(hpLabel);
            hpLineContainer.AddComponent(hpLine);
            hpLineContainer.AddComponent(hpText);

            nameBox.Text = pokemon.Name;
            
            level.Text = "L" + pokemon.Level;
            level.HorizontalPolicy = ResizePolicy.Preferred;

            nameLevelContainer.AddComponent(nameBox);
            nameLevelContainer.AddComponent(level);

            dataContainer.AddComponent(nameLevelContainer);
            dataContainer.AddComponent(hpLineContainer);

            icon.VerticalPolicy = icon.HorizontalPolicy = ResizePolicy.Fixed;

            iconDataContainer.AddComponent(icon);
            iconDataContainer.AddComponent(dataContainer);

            mainContainer = iconDataContainer;

            icon.Image = textureProvider.GetIcon(pokemon.Id);

        }
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            mainContainer.Draw(time, batch);
        }

        public override void Setup()
        {
            mainContainer.Setup();
        }

        protected override void Update()
        {
            mainContainer.SetCoordinates(this);
            icon.Width = icon.Height = mainContainer.Height;
        }

        private TextBox level;
        private TextureProvider textureProvider;
    }
}
