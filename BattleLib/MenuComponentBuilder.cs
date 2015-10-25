using Base;
using BattleLib.Components.BattleState;
using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using BattleLib.GraphicComponents;
using BattleLib.GraphicComponents.MenuView;
using Microsoft.Xna.Framework;

namespace BattleLib
{
    public class MenuComponentBuilder
    {
        public MenuComponent Component { get; private set; }
        public MenuGraphics Graphics { get; private set; }
        public InputComponent Input { get; private set; }

        ModelFactory factory = new ModelFactory();

        public MenuComponentBuilder(MenuComponent component, MenuGraphics graphics, InputComponent input)
        {
            Init(component, graphics, input);
        }

        private void Init(MenuComponent component, MenuGraphics graphics, InputComponent input)
        {
            Component = component;
            Graphics = graphics;
            Input = input;

            Input.OnMenuChanged += Component.OnMenuChanged;
            Input.OnMenuChanged += Graphics.OnMenuChange;
        }

        public MenuComponentBuilder(Game game, BattleStateComponent battleState)
        {
            Init(new MenuComponent(), 
                new MenuGraphics(), 
                new InputComponent(battleState, new ClientIdentifier(), game));
        }

        public void BuildDefaultMenu(BattleGraphics graphics)
        {
            AddDefaultPkmnMenu(graphics);
            AddDefaultAttackMenu();
            AddDefaultItemMenu();
            AddDefaultMainMenu();
        }

        public void AddDefaultMainMenu()
        {
            var model = factory.GetModel(MenuType.Main);
            var controller = new MainMenuController();
            var state = new MainMenuState(model, controller);

            AddMenu(model, state, controller);
        }

        public void AddDefaultAttackMenu()
        {
            var model = factory.GetModel(MenuType.Attack);
            var controller = new AttackMenuController((DefaultModel<Move>) model);
            var state = new AttackMenu(model, controller);

            AddMenu(model, state, controller);
        }

        public void AddDefaultItemMenu()
        {
            var model = factory.GetModel(MenuType.Item);
            var controller = new ItemMenuController((DefaultModel<Item>)model);
            var state = new ItemMenuState(model, controller);

            AddMenu(model, state, controller);
        }

        public void AddDefaultPkmnMenu(BattleGraphics graphics)
        {
            var model = factory.GetModel(MenuType.PKMN);
            var controller = new PokemonMenuController((DefaultModel<Pokemon>)model);
            var state = new CharacterMenuState(model, controller, graphics);

            AddMenu(model, state, controller);
        }

        public void AddMenu(IMenuModel model, IMenuState state, IMenuController controller)
        {
            Component.AddModel(model);
            Graphics.Add(model.Type, state);
            Input.AddState(controller);
        }
    }
}
