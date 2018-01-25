using System.Linq;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Gui;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class PokemonDataView
    {
        private readonly IMessageBus _messageBus;
        private readonly Entity _entity;


#pragma warning disable 649
        [GuiLoaderId("Window")] private Grid _grid;
        [GuiLoaderId("HpLine")] private HpLine _hpLine;
        [GuiLoaderId("LevelLabel")] private Label _lvl;
        [GuiLoaderId("Name")] private Label _name;
        [GuiLoaderId("HpText")] private HpText _hpText;
#pragma warning restore 649

        public PokemonDataView(GuiFactory factory, string xmlPath, IMessageBus messageBus, Entity entity)
        {
            _messageBus = messageBus;
            _entity = entity;
            factory.LoadFromFile(xmlPath, this);
        }

        public int CurrentHp => _hpLine.Current;

        public void Update(IEntityManager entityManager) 
        {
            var component = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(_entity).First();
            if(component.Pokemon.Hp != _hpLine.Current)
            {
                SetHp(component.Pokemon.Hp);
            }
        }
        private void SetHp(int newHp)
        {
            _hpLine.Current = newHp;
            if (_hpText != null)
                _hpText.CurrentHp = newHp;
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _name.Text = pokemon.Name;
            _lvl.Text = ":L" + pokemon.Level;
            _hpLine.MaxHp = pokemon.MaxHp;
            if (_hpText != null)
                _hpText.MaxHp = pokemon.MaxHp;

            SetHp(pokemon.Hp);

        }

        public void Show()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_grid, true, -100));

        }
    }
}