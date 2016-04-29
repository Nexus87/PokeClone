using Base;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using System;

namespace BattleLib.GraphicComponents
{
    public class BattleGUI : IGUIService
    {
        private Dialog attackFrame;
        private Dialog itemFrame;
        private Dialog mainFrame;
        private MessageBox messageBox;
        private Dialog messageFrame;
        private Dialog pkmnFrame;

        public BattleGUI(Configuration config, IPokeEngine game, BattleStateComponent battleState, ClientIdentifier player, ClientIdentifier ai)
        {
            game.Services.AddService(typeof(IGUIService), this);
            BattleState = battleState;
            ID = player;
            this.ai = ai;

            mainFrame = new Dialog(game.DefaultBorderTexture, game);
            attackFrame = new Dialog(game.DefaultBorderTexture, game);
            itemFrame = new Dialog(game.DefaultBorderTexture, game);
            pkmnFrame = new Dialog(game);
            messageFrame = new Dialog(game.DefaultBorderTexture, game);

            InitMessageBox(config, game);

            InitMainMenu(config, game);
            InitAttackMenu(config, game);
            InitItemMenu(config, game);
            InitPKMNMenu(config, game);

        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private ClientIdentifier ai;

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
            attackFrame.IsVisible = false;
        }

        private void BackToMain(object sender, EventArgs e)
        {
            itemFrame.IsVisible = false;
            attackFrame.IsVisible = false;
            pkmnFrame.IsVisible = false;

            mainFrame.IsVisible = true;
        }

        private void InitAttackMenu(Configuration config, IPokeEngine game)
        {
            
            
            var model = new AttackModel(BattleState.GetPokemon(ID));
            
            var tableView = new TableView<Move>(
                model, 
                new DefaultTableRenderer<Move>(game) { DefaultString = "------" }, 
                new AttackTableSelectionModel(model), 
                game
                );

            var AttackMenu = new TableWidget<Move>(null, null, tableView, game);

            attackFrame.AddWidget(AttackMenu);
            attackFrame.SetCoordinates(
                X: game.ScreenWidth / 2.0f,
                Y: 2.0f * game.ScreenHeight / 3.0f,
                width: game.ScreenWidth - game.ScreenWidth / 2.0f,
                height: game.ScreenHeight - 2.0f * game.ScreenHeight / 3.0f
            );

            AttackMenu.ItemSelected += AttackMenu_ItemSelected;
            AttackMenu.OnExitRequested += BackToMain;
            AttackMenu.OnExitRequested += delegate { ResetTableWidget(AttackMenu); };

            attackFrame.IsVisible = false;
            game.GUIManager.AddWidget(attackFrame);
        }

        private void InitItemMenu(Configuration config, IPokeEngine game)
        {
            var model = new DefaultTableModel<Item>();
            var ItemMenu = new TableWidget<Item>(8, null, model, game);
            
            for (int i = 0; i < 20; i++)
                model.SetData(new Item { Name = "Item" + i }, i, 0);

            itemFrame.XPosition = 3.0f * game.ScreenWidth / 8.0f;
            itemFrame.YPosition = 1.0f * game.ScreenHeight / 8.0f;

            itemFrame.Width = game.ScreenWidth - itemFrame.XPosition;
            itemFrame.Height = (2.0f * game.ScreenHeight / 3.0f) - itemFrame.YPosition;

            ItemMenu.ItemSelected += ItemMenu_ItemSelected;
            ItemMenu.OnExitRequested += BackToMain;
            ItemMenu.OnExitRequested += delegate { ResetTableWidget(ItemMenu); };

            itemFrame.AddWidget(ItemMenu);
            itemFrame.IsVisible = false;

            game.GUIManager.AddWidget(itemFrame);
        }

        private static void ResetTableWidget<T>(TableWidget<T> widget)
        {
            widget.SelectCell(0, 0);
        }

        private void InitMainMenu(Configuration config, IPokeEngine game)
        {
            var MainMenu = new TableWidget<string>(game);
            MainMenu.Model.SetData("Attack", 0, 0);
            MainMenu.Model.SetData("PKMN", 0, 1);
            MainMenu.Model.SetData("Item", 1, 0);
            MainMenu.Model.SetData("Run", 1, 1);

            mainFrame.AddWidget(MainMenu);

            mainFrame.XPosition = 0.5f * game.ScreenWidth;
            mainFrame.YPosition = 2.0f * game.ScreenHeight / 3.0f;
            mainFrame.Width = game.ScreenWidth - mainFrame.XPosition;
            mainFrame.Height = game.ScreenHeight - mainFrame.YPosition;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
            MainMenu.OnExitRequested += delegate { game.Exit(); };

            mainFrame.IsVisible = true;
            game.GUIManager.AddWidget(mainFrame);
        }

        private void InitMessageBox(Configuration config, IPokeEngine game)
        {
            messageBox = new MessageBox(config, game);

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

        private void InitPKMNMenu(Configuration config, IPokeEngine game)
        {
            var model = new DefaultTableModel<Pokemon>();
            var PKMNMenu = new TableWidget<Pokemon>(model, game);

            pkmnFrame.XPosition = 0;
            pkmnFrame.YPosition = 0;
            pkmnFrame.Width = game.ScreenWidth;
            pkmnFrame.Height = game.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.OnExitRequested += BackToMain;
            PKMNMenu.OnExitRequested += delegate { ResetTableWidget(PKMNMenu); };

            pkmnFrame.AddWidget(PKMNMenu);
            pkmnFrame.IsVisible = false;

            game.GUIManager.AddWidget(pkmnFrame);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, ID, e.SelectedData);
            itemFrame.IsVisible = false;
        }

        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            switch (e.SelectedData)
            {
                case "Attack":
                    mainFrame.IsVisible = false;
                    attackFrame.IsVisible = true;
                    break;

                case "PKMN":
                    mainFrame.IsVisible = false;
                    pkmnFrame.IsVisible = true; ;
                    break;

                case "Item":
                    mainFrame.IsVisible = false;
                    itemFrame.IsVisible = true;
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