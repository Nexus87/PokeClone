using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    class PokemonDataView : ForwardingGraphicComponent<Container>
    {
        public event EventHandler OnHPUpdated;

        public PokemonDataView(IPokeEngine game, bool player)
            : base(new Container(game), game)
        {
            var container = InnerComponent;
            var hpLineContainer = new Container(game);
            
            hpLine = new HPLine(game);
            name = new TextBox(game);
            lvl = new TextBox(game) { PreferedTextHeight = 16.0f };

            hpLineContainer.Layout = new HBoxLayout();
            hpLineContainer.AddComponent(new TextBox(game) { Text = "hp:", PreferedTextHeight = 12.0f });
            hpLineContainer.AddComponent(hpLine);

            container.AddComponent(name);
            container.AddComponent(hpLineContainer);
            container.AddComponent(lvl);

            if (player)
            {
                hpText = new TextBox(game) { PreferedTextHeight = 16.0f };
                container.AddComponent(hpText);
            }

            container.Layout = new VBoxLayout();
        }

        HPLine hpLine;
        TextBox hpText;
        TextBox name;
        TextBox lvl;

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            base.Draw(time, batch);
        }

        public void SetHP(int newHP)
        {
            var setHPAnimation = new SetHPAnimation(newHP, hpLine, hpText);
            setHPAnimation.AnimationFinished += delegate { OnHPUpdated(this, EventArgs.Empty); };

            PlayAnimation(setHPAnimation);
        }

        public void SetPokemon(PokemonWrapper pokemon)
        {
            name.Text = pokemon.Name;
            lvl.Text = "Level " + pokemon.Level;
            hpLine.MaxHP = pokemon.MaxHP;
            hpLine.Current = pokemon.HP;

            if (hpText != null)
                hpText.Text = pokemon.HP + "/  " + hpLine.MaxHP;
        }

        protected override void Update()
        {
        }

        private class SetHPAnimation : IAnimation
        {
            private int targetHP;
            private HPLine line;
            private TextBox text;

            private int currentHP;
            private Func<int, int> nextInt;

            public event EventHandler AnimationFinished;

            public SetHPAnimation(int targetHP, HPLine line, TextBox text)
            {
                this.targetHP = targetHP;
                this.line = line;
                this.text = text;
                currentHP = line.Current;

                if (line.Current < targetHP)
                    nextInt = a => a + 1;
                else
                    nextInt = a => a - 1;
            }
            public void Update(GameTime time, IGraphicComponent component)
            {
                currentHP = nextInt(currentHP);
                line.Current = currentHP;
                if(text != null)
                    text.Text = line.Current + "/  " + line.MaxHP;

                if (currentHP == targetHP)
                    AnimationFinished(this, EventArgs.Empty);
            }
        }
    }
}
