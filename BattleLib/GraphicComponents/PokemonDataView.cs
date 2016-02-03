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
    class PokemonDataView : AbstractGraphicComponent
    {
        public event EventHandler OnHPUpdated = delegate { };
        public PokemonDataView(PokeEngine game)
            : base(game)
        {
            hpLine = new HPLine(game);
            name = new TextBox("MenuFont", game);
            lvl = new TextBox("MenuFont", game);
        }
        VBoxLayout layout = new VBoxLayout();
        HPLine hpLine;
        TextBox name;
        TextBox lvl;
        HpSetter hpSetter = null;

        class HpSetter
        {
            public float speed = 1.0f;
            private int currentHP;
            private int targetHP;
            private HPLine line;

            public HpSetter(HPLine line, int targetHP)
            {
                this.line = line;
                this.targetHP = targetHP;
                currentHP = line.Current;
            }
            public bool Update(GameTime time)
            {
                int nextHp = (int)(currentHP + (targetHP - currentHP) * speed * time.ElapsedGameTime.Seconds);
                if (nextHp == currentHP)
                    return false;

                currentHP = nextHp;
                line.Current = currentHP;
                return true;
            }
        }

        public void SetHP(int newHP)
        {
            hpSetter = new HpSetter(hpLine, newHP);
        }

        public void SetPokemon(PokemonWrapper pokemon)
        {
            name.Text = pokemon.Name;
            lvl.Text = "Level " + pokemon.Level;
            hpLine.MaxHP = pokemon.MaxHP;
            hpLine.Current = pokemon.HP;
        }

        public override void Setup(ContentManager content)
        {
            layout.Init(this);
            layout.AddComponent(name);
            layout.AddComponent(hpLine);
            layout.AddComponent(lvl);

            layout.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (hpSetter != null)
            {
                if (!hpSetter.Update(time))
                {
                    hpSetter = null;
                    OnHPUpdated(this, null);
                }
            }

            layout.Draw(time, batch);
        }
    }
}
