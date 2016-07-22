using Base;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;

namespace BattleLib.GraphicComponents
{
    public class BattleGUI : IGUIService
    {
        private Dialog mainFrame;
        private MessageBox messageBox;
        private Dialog messageFrame;
        private Dialog pkmnFrame;

        public BattleGUI(IPokeEngine game, IMenuWidget<Move> moveWidget, IMenuWidget<Item> itemWidget, GraphicComponentFactory factory, BattleStateComponent battleState, ClientIdentifier player, ClientIdentifier ai)
        {
            BattleState = battleState;
            ID = player;
            this.ai = ai;
            this.moveWidget = moveWidget;
            this.itemWidget = itemWidget;
            mainFrame = factory.CreateGraphicComponent<Dialog>();
            pkmnFrame = new Dialog();
            messageFrame = factory.CreateGraphicComponent<Dialog>();

            InitMessageBox(factory, game);

            InitMainMenu(factory, game);
            InitAttackMenu(factory, game);
            InitItemMenu(factory, game);
            InitPKMNMenu(factory, game);

        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private ClientIdentifier ai;
        private IMenuWidget<Move> moveWidget;
        private IMenuWidget<Item> itemWidget;

        public BattleStateComponent BattleState { get; private set; }
        public ClientIdentifier ID { get; set; }

        public void SetText(string Text)
        {
            messageBox.DisplayText(Text);
        }

        public void ShowMenu()
        {
            messageBox.ResetText();
            BackToMain(null, null);
            MenuShowed(this, EventArgs.Empty);
        }

        private void AttackMenu_ItemSelected(object sender, SelectionEventArgs<Move> e)
        {
            BattleState.SetMove(ID, ai, e.SelectedData);
            moveWidget.IsVisible = false;
        }

        private void BackToMain(object sender, EventArgs e)
        {
            itemWidget.IsVisible = false;
            moveWidget.IsVisible = false;
            pkmnFrame.IsVisible = false;

            mainFrame.IsVisible = true;
        }

        private void InitAttackMenu(GraphicComponentFactory factory, IPokeEngine game)
        {
            moveWidget.SetCoordinates(
                X: game.ScreenWidth / 2.0f,
                Y: 2.0f * game.ScreenHeight / 3.0f,
                width: game.ScreenWidth - game.ScreenWidth / 2.0f,
                height: game.ScreenHeight - 2.0f * game.ScreenHeight / 3.0f
            );

            moveWidget.ItemSelected += AttackMenu_ItemSelected;
            moveWidget.ExitRequested += BackToMain;
            moveWidget.ExitRequested += delegate { moveWidget.ResetSelection(); };

            moveWidget.IsVisible = false;
            game.GUIManager.AddWidget(moveWidget);
        }

        private void InitItemMenu(GraphicComponentFactory factory, IPokeEngine game)
        {
            itemWidget.XPosition = 3.0f * game.ScreenWidth / 8.0f;
            itemWidget.YPosition = 1.0f * game.ScreenHeight / 8.0f;

            itemWidget.Width = game.ScreenWidth - itemWidget.XPosition;
            itemWidget.Height = (2.0f * game.ScreenHeight / 3.0f) - itemWidget.YPosition;

            itemWidget.ItemSelected += ItemMenu_ItemSelected;
            itemWidget.ExitRequested += BackToMain;
            itemWidget.ExitRequested += delegate { itemWidget.ResetSelection(); };

            itemWidget.IsVisible = false;

            game.GUIManager.AddWidget(itemWidget);
        }

        private static void ResetTableWidget<T>(TableWidget<T> widget)
        {
            widget.SelectCell(0, 0);
        }

        private void InitMainMenu(GraphicComponentFactory factory, IPokeEngine game)
        {
            var MainMenu = factory.CreateTableWidget<string>();
            MainMenu.Model.SetDataAt("Attack", 0, 0);
            MainMenu.Model.SetDataAt("PKMN", 0, 1);
            MainMenu.Model.SetDataAt("Item", 1, 0);
            MainMenu.Model.SetDataAt("Run", 1, 1);

            mainFrame.AddWidget(MainMenu);

            mainFrame.XPosition = 0.5f * game.ScreenWidth;
            mainFrame.YPosition = 2.0f * game.ScreenHeight / 3.0f;
            mainFrame.Width = game.ScreenWidth - mainFrame.XPosition;
            mainFrame.Height = game.ScreenHeight - mainFrame.YPosition;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
            MainMenu.ExitRequested += delegate { game.Exit(); };

            mainFrame.IsVisible = true;
            game.GUIManager.AddWidget(mainFrame);
        }

        private void InitMessageBox(GraphicComponentFactory factory, IPokeEngine game)
        {
            messageBox = factory.CreateGraphicComponent<MessageBox>();

            messageFrame.AddWidget(messageBox);
            messageFrame.XPosition = 0;
            messageFrame.YPosition = 2.0f * game.ScreenHeight / 3.0f;
            messageFrame.Width = game.ScreenWidth;
            messageFrame.Height = game.ScreenHeight - messageFrame.YPosition;

            messageFrame.IsVisible = true;
            game.GUIManager.AddWidget(messageFrame);

            messageBox.OnAllLineShowed += AllLineShowedHandler;
        }

        private void AllLineShowedHandler(object sender, EventArgs e)
        {
            TextDisplayed(this, EventArgs.Empty);
        }

        private void InitPKMNMenu(GraphicComponentFactory factory, IPokeEngine game)
        {
            var model = new DefaultTableModel<Pokemon>();
            var PKMNMenu = factory.CreateTableWidget(factory.CreateTableView(model: model));

            pkmnFrame.XPosition = 0;
            pkmnFrame.YPosition = 0;
            pkmnFrame.Width = game.ScreenWidth;
            pkmnFrame.Height = game.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.ExitRequested += BackToMain;
            PKMNMenu.ExitRequested += delegate { ResetTableWidget(PKMNMenu); };

            pkmnFrame.AddWidget(PKMNMenu);
            pkmnFrame.IsVisible = false;

            game.GUIManager.AddWidget(pkmnFrame);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, ID, e.SelectedData);
            itemWidget.IsVisible = false;
        }

        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            switch (e.SelectedData)
            {
                case "Attack":
                    mainFrame.IsVisible = false;
                    moveWidget.IsVisible = true;
                    break;

                case "PKMN":
                    mainFrame.IsVisible = false;
                    pkmnFrame.IsVisible = true; ;
                    break;

                case "Item":
                    mainFrame.IsVisible = false;
                    itemWidget.IsVisible = true;
                    break;
            }
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            BattleState.SetCharacter(ID, e.SelectedData);
            pkmnFrame.IsVisible = false;
        }
    }
}