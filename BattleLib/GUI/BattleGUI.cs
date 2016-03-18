using Base;
using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    public class BattleGUI : IGUIService
    {
        private Dialog attackFrame;
        private Dialog itemFrame;
        private Dialog mainFrame;
        private MessageBox messageBox;
        private Dialog messageFrame;
        private Dialog pkmnFrame;

        public BattleGUI(Configuration config, PokeEngine game, BattleStateComponent battleState, ClientIdentifier player, ClientIdentifier ai)
        {
            game.Services.AddService(typeof(IGUIService), this);
            BattleState = battleState;
            ID = player;
            this.ai = ai;

            mainFrame = new Dialog("border", game);
            attackFrame = new Dialog("border", game);
            itemFrame = new Dialog("border", game);
            pkmnFrame = new Dialog(game);
            messageFrame = new Dialog("border", game);

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

        private void InitAttackMenu(Configuration config, PokeEngine game)
        {
            var AttackMenu = new TableWidget<Move>(game);
            var model = new AttackModel(BattleState.GetPokemon(ID));

            AttackMenu.Model = model;

            attackFrame.AddWidget(AttackMenu);
            attackFrame.SetCoordinates(
                X: PokeEngine.ScreenWidth / 2.0f,
                Y: 2.0f * PokeEngine.ScreenHeight / 3.0f,
                width: PokeEngine.ScreenWidth - PokeEngine.ScreenWidth / 2.0f,
                height: PokeEngine.ScreenHeight - 2.0f * PokeEngine.ScreenHeight / 3.0f
            );

            AttackMenu.ItemSelected += AttackMenu_ItemSelected;
            AttackMenu.OnExitRequested += BackToMain;

            attackFrame.IsVisible = false;
            game.GUIManager.AddWidget(attackFrame);
        }

        private void InitItemMenu(Configuration config, PokeEngine game)
        {
            var ItemMenu = new TableWidget<Item>(game);
            var model = new DefaultTableModel<Item>();
            var list = new List<Item>();
            for (int i = 0; i < 20; i++)
                model.SetData(new Item { Name = "Item" + i }, i, 0);

            ItemMenu.Model = model;

            itemFrame.XPosition = 3.0f * PokeEngine.ScreenWidth / 8.0f;
            itemFrame.YPosition = 1.0f * PokeEngine.ScreenHeight / 8.0f;

            itemFrame.Width = PokeEngine.ScreenWidth - itemFrame.XPosition;
            itemFrame.Height = (2.0f * PokeEngine.ScreenHeight / 3.0f) - itemFrame.YPosition;

            ItemMenu.ItemSelected += ItemMenu_ItemSelected;
            ItemMenu.OnExitRequested += BackToMain;

            itemFrame.AddWidget(ItemMenu);
            itemFrame.IsVisible = false;

            game.GUIManager.AddWidget(itemFrame);
        }

        private void InitMainMenu(Configuration config, PokeEngine game)
        {
            var MainMenu = new TableWidget<string>(game);
            MainMenu.Model.SetData("Attack", 0, 0);
            MainMenu.Model.SetData("PKMN", 0, 1);
            MainMenu.Model.SetData("Item", 1, 0);
            MainMenu.Model.SetData("Run", 1, 1);

            mainFrame.AddWidget(MainMenu);

            mainFrame.XPosition = 0.5f * PokeEngine.ScreenWidth;
            mainFrame.YPosition = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            mainFrame.Width = PokeEngine.ScreenWidth - mainFrame.XPosition;
            mainFrame.Height = PokeEngine.ScreenHeight - mainFrame.YPosition;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
            MainMenu.OnExitRequested += delegate { game.Exit(); };

            mainFrame.IsVisible = true;
            game.GUIManager.AddWidget(mainFrame);
        }

        private void InitMessageBox(Configuration config, PokeEngine game)
        {
            messageBox = new MessageBox(config, game);

            messageFrame.AddWidget(messageBox);
            messageFrame.XPosition = 0;
            messageFrame.YPosition = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            messageFrame.Width = PokeEngine.ScreenWidth;
            messageFrame.Height = PokeEngine.ScreenHeight - messageFrame.YPosition;

            messageFrame.IsVisible = true;
            game.GUIManager.AddWidget(messageFrame);

            messageBox.OnAllLineShowed += AllLineShowedHandler;
        }

        private void AllLineShowedHandler(object sender, EventArgs e)
        {
            TextDisplayed(this, EventArgs.Empty);
        }

        private void InitPKMNMenu(Configuration config, PokeEngine game)
        {
            var PKMNMenu = new TableWidget<Pokemon>(game);
            var model = new DefaultTableModel<Pokemon>();

            PKMNMenu.Model = model;

            pkmnFrame.XPosition = 0;
            pkmnFrame.YPosition = 0;
            pkmnFrame.Width = PokeEngine.ScreenWidth;
            pkmnFrame.Height = PokeEngine.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.OnExitRequested += BackToMain;

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