using System;
using BattleLib.Components.BattleState;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.Layouts;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    // TODO come up with a better solution for this

    public class PokemonDataView : ForwardingGraphicComponent<Container>
    {
        public event EventHandler OnHPUpdated;

        public PokemonDataView(HPLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox) :
            this(line, nameBox, levelBox, hpBox, null)
        {}
        public PokemonDataView(HPLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox, HPText hpTextBox)
            : base(new Container())
        {
            var container = InnerComponent;
            var hpLineContainer = new Container();
            var lvlContainer = new Container();

            hpLine = line;
            
            name = nameBox;

            lvl = levelBox;
            lvl.PreferredTextHeight = 24.0f;
            lvl.HorizontalPolicy = ResizePolicy.Preferred;
            lvlContainer.AddComponent(new NullGraphicObject());
            lvlContainer.AddComponent(lvl);
            lvlContainer.AddComponent(new NullGraphicObject());
            lvlContainer.Layout = new HBoxLayout();
            lvlContainer.VerticalPolicy = ResizePolicy.Fixed;
            lvlContainer.SetCoordinates(0, 0, 0, lvl.PreferredTextHeight);

            hpBox.Text = "hp:";
            hpBox.PreferredTextHeight = 24.0f;
            hpBox.HorizontalPolicy = ResizePolicy.Preferred;

            hpLineContainer.VerticalPolicy = ResizePolicy.Fixed;
            hpLineContainer.SetCoordinates(0, 0, 0, hpBox.PreferredTextHeight);
            hpLineContainer.Layout = new HBoxLayout();
            hpLineContainer.AddComponent(hpBox);
            hpLineContainer.AddComponent(hpLine);

            container.AddComponent(name);
            container.AddComponent(lvlContainer);
            container.AddComponent(hpLineContainer);

            if (hpTextBox != null)
            {
                var hpTextContainer = new Container();
                hpText = hpTextBox;
                hpText.PreferredTextHeight = 24.0f;
                hpText.HorizontalPolicy = ResizePolicy.Preferred;
                hpTextContainer.AddComponent(new NullGraphicObject());
                hpTextContainer.AddComponent(hpText);
                hpTextContainer.Layout = new HBoxLayout();
                container.AddComponent(hpTextContainer);
            }

            container.Layout = new VBoxLayout { Spacing = 10f };
        }

        private HPLine hpLine;
        private HPText hpText;
        private TextBox name;
        private TextBox lvl;

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
            lvl.Text = ":L" + pokemon.Level;
            hpLine.MaxHP = pokemon.MaxHP;
            hpLine.Current = pokemon.HP;

            if (hpText != null)
            {
                hpText.SetPokemon(pokemon.Pokemon);
            }
        }

        protected override void Update()
        {
        }

        private class SetHPAnimation : IAnimation
        {
            private readonly int targetHP;
            private readonly HPLine line;
            private readonly HPText text;

            private int currentHP;
            private readonly Func<int, int> nextInt;

            public event EventHandler AnimationFinished;

            public SetHPAnimation(int targetHP, HPLine line, HPText text)
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
                if (currentHP == targetHP)
                {
                    AnimationFinished(this, EventArgs.Empty);
                    return;
                }
                currentHP = nextInt(currentHP);
                line.Current = currentHP;
                if(text != null)
                    text.SetHP(line.Current);
            }
        }
    }
}
