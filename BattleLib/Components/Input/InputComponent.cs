using BattleLib.Components.BattleState;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public class MenuChangedArgs : EventArgs
    {
        public MenuType MenuType { get; set; }
    }

    public class InputComponent : GameComponent
    {
        public event EventHandler<MenuChangedArgs> OnMenuChanged;
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        KeyboardState oldState;
        Dictionary<MenuType, IMenuController> states = new Dictionary<MenuType, IMenuController>();

        public void SetMenu(MenuType type)
        {
            UpdateState(type);
        }

        IMenuController currentState;
        ClientIdentifier playerId;
        BattleStateComponent battleState;

        public InputComponent(BattleStateComponent stateComponent, ClientIdentifier id, Game game) : base(game)
        {
            playerId = id;
            battleState = stateComponent;

            currentState = new NullController();
            AddState(currentState);
        }

        public void AddState(IMenuController state)
        {
            states.Add(state.Type, state);
            state.OnSelectionChanged += State_OnSelectionChanged;
            state.OnItemSelection += State_OnItemSelection;
            state.OnMoveSelected += State_OnMoveSelected;
            state.OnPKMNSelected += State_OnPKMNSelected;
        }

        void State_OnSelectionChanged(object sender, SelectionEventArgs e)
        {
            if (OnSelectionChanged != null)
                OnSelectionChanged(this, e);

        }

        void State_OnPKMNSelected(object sender, PKMNSelectedEventArgs e)
        {
            battleState.SetCharacter(playerId, e.SelectedPKMN);
        }

        void State_OnMoveSelected(object sender, MoveSelectedEventArgs e)
        {
            battleState.SetMove(playerId, e.SelectedMove);
        }

        void State_OnItemSelection(object sender, ItemSelectedEventArgs e)
        {
            battleState.SetItem(playerId, e.SelectedItem);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            var newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
                currentState.HandleDirection(Direction.Left);
            if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
                currentState.HandleDirection(Direction.Right);
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
                currentState.HandleDirection(Direction.Up);
            if (newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
                currentState.HandleDirection(Direction.Down);
            if (newState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
                UpdateState(currentState.Select());
            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                UpdateState(currentState.Back());
            oldState = newState;
        }

        void UpdateState(MenuType type)
        {
            if (type == currentState.Type)
                return;

            IMenuController newState;
            if (!states.TryGetValue(type, out newState))
                throw new InvalidOperationException("State '" + type + "' is unkown.");
            
            // TODO: The following code depends on the behavior of MenuComponent and MenuGraphics:
            // If we call Setup befor firing the OnMenuChanged event, OnSelectionChanged events
            // will change the old state.

            currentState.TearDown();
            if (OnMenuChanged != null)
                OnMenuChanged(this, new MenuChangedArgs { MenuType = type });

            currentState = newState;
            currentState.Setup();
        }
    }
}
