using System.Collections.ObjectModel;
using BattleMode.Entities.Actions;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Gui.Actions;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class MoveMenuController
    {

        public MoveMenuController(GuiFactory factory, IMessageBus messageBus, Entity player, Entity ai)
        {

            factory.LoadFromFile(@"BattleMode\Gui\MoveMenu.xml", this);
            _listView.Model = new ObservableCollection<Move>();

            _listView.ListCellFactory = value =>
            {
                var button = new Button
                {
                    Text = value?.Name ?? "--------",
                    Enabled = value != null
                };
                button.OnPressed += () =>
                {
                    messageBus.SendSetCommandAction(new MoveCommand(value, player, ai), player, this);
                };
                return button;
            };

            _window.SetInputListener(CommandKeys.Back, () => messageBus.SendAction(new ShowMainMenuAction()));
        }

        public void SetPokemon(Pokemon pokemon)
        {
            _listView.Model.Clear();
            foreach (var sourceMove in pokemon.Moves)
                _listView.Model.Add(sourceMove);
        }
        public void Show(IMessageBus messageBus)
        {
            _listView.SelectCell(0);
            messageBus.SendAction(new SetGuiComponentVisibleAction(_window, true));
        }

        public void Close(IMessageBus messageBus)
        {
            messageBus.SendAction(new SetGuiComponentVisibleAction(_window, false));
        }

#pragma warning disable 649

        [GuiLoaderId("Window")] private readonly Window _window;

        [GuiLoaderId("ListView")] private readonly ListView<Move> _listView;

#pragma warning restore 649
    }
}