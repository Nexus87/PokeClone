using BattleMode.Entities.BattleState;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using GameEngine.Pokemon.Gui;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace BattleMode.Core.Components.GraphicComponents
{
    public interface IPokemonDataView
    {
        void SetHp(int newHp);
        void SetPokemon(PokemonWrapper pokemon);
        void Show();
    }

    public abstract class PokemonDataView : IPokemonDataView
    {
        protected PokemonDataView(HpLine line, Label nameBox, Label levelBox, Label hpBox, GuiManager guiManager) :
            this(line, nameBox, levelBox, hpBox, null, guiManager)
        {
        }

        protected PokemonDataView(HpLine line, Label nameBox, Label levelBox, Label hpBox, HpText hpTextBox, GuiManager guiManager)
        {
            Container = new Grid();

            _hpLine = line;
            _lvl = levelBox;
            _guiManager = guiManager;
            _name = nameBox;

            var lvlContainer = new Grid();
            lvlContainer.AddRow(new RowProperty {Type = ValueType.Percent, Share = 1});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Percent, Share = 1});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Auto,});
            lvlContainer.AddColumn(new ColumnProperty {Type = ValueType.Percent, Share = 1});

            _lvl.TextHeight = 24.0f;

            lvlContainer.SetComponent(new Spacer(), 0, 0);
            lvlContainer.SetComponent(_lvl, 0, 1);
            lvlContainer.SetComponent(new Spacer(), 0, 2);
            lvlContainer.SetCoordinates(0, 0, 0, _lvl.TextHeight);



            var hpLineContainer = new Grid();
            hpLineContainer.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
            hpLineContainer.AddColumn(new ColumnProperty{Type = ValueType.Auto});
            hpLineContainer.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});

            hpBox.Text = "hp:";
            hpBox.TextHeight = 24.0f;

            hpLineContainer.SetComponent(hpBox, 0, 0);
            hpLineContainer.SetComponent(_hpLine, 0, 1);


            Container.AddPercentColumn();
            Container.AddPercentRow();
            Container.AddAbsoluteRow(10f);
            Container.AddAbsoluteRow(_lvl.TextHeight);
            Container.AddAbsoluteRow(10f);
            Container.AddAbsoluteRow(hpBox.TextHeight);

            Container.SetComponent(_name, 0, 0);
            Container.SetComponent(new Spacer(), 1, 0);
            Container.SetComponent(lvlContainer, 2, 0);
            Container.SetComponent(new Spacer(), 3, 0);
            Container.SetComponent(hpLineContainer, 4, 0);

            if (hpTextBox != null)
            {
                var hpTextContainer = new Grid();

                _hpText = hpTextBox;
                _hpText.PreferredTextHeight = 24.0f;

                hpTextContainer.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
                hpTextContainer.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});
                hpTextContainer.AddColumn(new ColumnProperty{Type = ValueType.Auto});
                hpTextContainer.SetComponent(new Spacer(), 0, 0);
                hpTextContainer.SetComponent(_hpText, 0, 1);

                Container.AddRow(new RowProperty{Type = ValueType.Absolute, Height = 10f});
                Container.AddRow(new RowProperty{Type = ValueType.Absolute, Height = _hpText.PreferredTextHeight});
                Container.SetComponent(new Spacer(), 5, 0);
                Container.SetComponent(hpTextContainer, 6, 0);
            }
        }

        protected readonly HpLine _hpLine;
        protected readonly HpText _hpText;
        protected readonly Label _name;
        protected readonly Label _lvl;
        public readonly Grid Container;
        private readonly GuiManager _guiManager;


        public void SetHp(int newHp)
        {
            _hpLine.Current = newHp;
            if(_hpText != null)
                _hpText.CurrentHp = newHp;
        }

        public void SetPokemon(PokemonWrapper pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpLine.MaxHp = pokemon.MaxHP;
            _hpLine.Current = pokemon.HP;

            _hpText?.SetPokemon(pokemon.Pokemon);
        }

        public void Show()
        {
            _guiManager.ShowWidget(Container);
        }
    }
}