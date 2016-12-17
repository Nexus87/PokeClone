using System;
using BattleLib.Components.BattleState;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace BattleLib.Components.GraphicComponents
{
    // TODO come up with a better solution for this

    public class PokemonDataView : ForwardingGraphicComponent<Grid>
    {
        public event EventHandler HpUpdated;

        public PokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox) :
            this(line, nameBox, levelBox, hpBox, null)
        {
        }

        public PokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox, HPText hpTextBox)
            : base(new Grid())
        {
            var container = InnerComponent;

            _hpLine = line;
            _lvl = levelBox;
            _name = nameBox;

            var lvlContainer = new Grid();
            lvlContainer.AddRow(new RowProperty {Type = ValueType.Percent, Share = 1});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Percent, Share = 1});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Auto,});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Percent, Share = 1});

            _lvl.PreferredTextHeight = 24.0f;

            lvlContainer.SetComponent(new NullGraphicObject(), 0, 0);
            lvlContainer.SetComponent(_lvl, 0, 1);
            lvlContainer.SetComponent(new NullGraphicObject(), 0, 2);
            lvlContainer.SetCoordinates(0, 0, 0, _lvl.PreferredTextHeight);



            var hpLineContainer = new Grid();
            hpLineContainer.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
            hpLineContainer.AddColumn(new ColumnProperty{Type = ValueType.Auto});
            hpLineContainer.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});

            hpBox.Text = "hp:";
            hpBox.PreferredTextHeight = 24.0f;

            hpLineContainer.SetComponent(hpBox, 0, 0);
            hpLineContainer.SetComponent(_hpLine, 0, 1);


            container.AddPercentColumn();
            container.AddPercentRow();
            container.AddAbsoluteRow(10f);
            container.AddAbsoluteRow(_lvl.PreferredTextHeight);
            container.AddAbsoluteRow(10f);
            container.AddAbsoluteRow(hpBox.PreferredTextHeight);

            container.SetComponent(_name, 0, 0);
            container.SetComponent(new NullGraphicObject(), 1, 0);
            container.SetComponent(lvlContainer, 2, 0);
            container.SetComponent(new NullGraphicObject(), 3, 0);
            container.SetComponent(hpLineContainer, 4, 0);

            if (hpTextBox != null)
            {
                var hpTextContainer = new Grid();

                _hpText = hpTextBox;
                _hpText.PreferredTextHeight = 24.0f;

                hpTextContainer.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
                hpTextContainer.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});
                hpTextContainer.AddColumn(new ColumnProperty{Type = ValueType.Auto});
                hpTextContainer.SetComponent(new NullGraphicObject(), 0, 0);
                hpTextContainer.SetComponent(_hpText, 0, 1);

                container.AddRow(new RowProperty{Type = ValueType.Absolute, Height = 10f});
                container.AddRow(new RowProperty{Type = ValueType.Absolute, Height = _hpText.PreferredTextHeight});
                container.SetComponent(new NullGraphicObject(), 5, 0);
                container.SetComponent(hpTextContainer, 6, 0);
            }
        }

        private readonly HpLine _hpLine;
        private readonly HPText _hpText;
        private readonly TextBox _name;
        private readonly TextBox _lvl;


        public void SetHp(int newHp)
        {
            var setHpAnimation = new SetHpAnimation(newHp, _hpLine, _hpText);
            setHpAnimation.AnimationFinished += delegate { HpUpdated?.Invoke(this, EventArgs.Empty); };

            PlayAnimation(setHpAnimation);
        }

        public void SetPokemon(PokemonWrapper pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpLine.MaxHp = pokemon.MaxHP;
            _hpLine.Current = pokemon.HP;

            _hpText?.SetPokemon(pokemon.Pokemon);
        }

        protected override void Update()
        {
        }

        private class SetHpAnimation : IAnimation
        {
            private readonly int _targetHp;
            private readonly HpLine _line;
            private readonly HPText _text;

            private int _currentHp;
            private readonly Func<int, int> _nextInt;

            public event EventHandler AnimationFinished;

            public SetHpAnimation(int targetHp, HpLine line, HPText text)
            {
                _targetHp = targetHp;
                _line = line;
                _text = text;
                _currentHp = line.Current;

                if (line.Current < targetHp)
                    _nextInt = a => a + 1;
                else
                    _nextInt = a => a - 1;
            }

            public void Update(GameTime time, IGraphicComponent component)
            {
                if (_currentHp == _targetHp)
                {
                    AnimationFinished?.Invoke(this, EventArgs.Empty);
                    return;
                }
                _currentHp = _nextInt(_currentHp);
                _line.Current = _currentHp;
                _text?.SetHP(_line.Current);
            }
        }
    }
}