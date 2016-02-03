using Base;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    public class BattleGUI : GameEngine.GUI
    {
        public BattleStateComponent BattleState { get; set; }
        public ClientIdentifier ID { get; set; }

        private TableWidget<string> MainMenu;
        private TableWidget<Move> AttackMenu;
        private TableWidget<Item> ItemMenu;
        private TableWidget<Pokemon> PKMNMenu;

        private Frame mainFrame;
        private Frame attackFrame;
        private Frame itemFrame;

        private IGraphicComponent currentFrame;
        private IWidget currentWidget;
        private MessageBox messageBox;

        public BattleGUI(Configuration config, PokeEngine game)
            : base(game)
        {
            mainFrame = new Frame("border", game);
            attackFrame = new Frame("border", game);
            itemFrame = new Frame("border", game);
            MainMenu = new TableWidget<string>(config, game);
            AttackMenu = new TableWidget<Move>(config, game);
            ItemMenu = new TableWidget<Item>(config, game);
            PKMNMenu = new TableWidget<Pokemon>(config, game);
            messageBox = new MessageBox(config, game);

            InitMainMenu();
            InitAttackMenu();
            InitItemMenu();
            InitPKMNMenu();
            InitMessageBox();

            currentFrame = mainFrame;
            currentWidget = MainMenu;
        }

        private void InitMessageBox()
        {
            messageBox.X = 0;
            messageBox.Y = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            messageBox.Width = PokeEngine.ScreenWidth;
            messageBox.Height = PokeEngine.ScreenHeight - messageBox.Y;
        }

        private void InitPKMNMenu()
        {
            var model = new DefaultListModel<Pokemon>();
            var list = new List<Pokemon>();
            PKData data = new PKData();
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN1"});
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN2" });

            PKMNMenu.Model = model;
            PKMNMenu.X = 0;
            PKMNMenu.Y = 0;
            PKMNMenu.Width = PokeEngine.ScreenWidth;
            PKMNMenu.Height = PokeEngine.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.OnExitRequested += BackToMain;
        }

        void BackToMain(object sender, EventArgs e)
        {
            currentFrame = mainFrame;
            currentWidget = MainMenu;
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            BattleState.SetCharacter(ID, e.SelectedData);
            currentFrame = null;
            currentWidget = messageBox;
        }

        private void InitItemMenu()
        {
            var model = new DefaultListModel<Item>();
            var list = new List<Item>();
            for (int i = 0; i < 20; i++)
                model.SetData(new Item { Name = "Item" + i }, i);

            ItemMenu.Model = model;

            itemFrame.AddContent(ItemMenu);

            itemFrame.X = 3.0f * PokeEngine.ScreenWidth / 8.0f;
            itemFrame.Y = 1.0f * PokeEngine.ScreenHeight / 8.0f;

            itemFrame.Width = PokeEngine.ScreenWidth - itemFrame.X;
            itemFrame.Height = (2.0f * PokeEngine.ScreenHeight / 3.0f) - itemFrame.Y;

            ItemMenu.ItemSelected += ItemMenu_ItemSelected;
            ItemMenu.OnExitRequested += BackToMain;
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, e.SelectedData);
            currentFrame = null;
            currentWidget = messageBox;
        }

        private void InitAttackMenu()
        {
            var model = new AttackModel();
            var list = new List<Move>();

            model.SetData(new Move(new MoveData { Name = "Move1" }), 0);
            model.SetData(new Move(new MoveData { Name = "Move2" }), 1);
            AttackMenu.Model = model;

            attackFrame.AddContent(AttackMenu);
            attackFrame.X = PokeEngine.ScreenWidth / 2.0f;
            attackFrame.Y = 2.0f * PokeEngine.ScreenHeight / 3.0f;

            attackFrame.Width = PokeEngine.ScreenWidth - attackFrame.X;
            attackFrame.Height = PokeEngine.ScreenHeight - attackFrame.Y;

            AttackMenu.ItemSelected += AttackMenu_ItemSelected;
            AttackMenu.OnExitRequested += BackToMain;
        }

        private void AttackMenu_ItemSelected(object sender, SelectionEventArgs<Move> e)
        {
            BattleState.SetMove(ID, e.SelectedData);
            currentFrame = null;
            currentWidget = messageBox;
        }

        private void InitMainMenu()
        {
            MainMenu.SetData("Attack", 0, 0);
            MainMenu.SetData("PKMN", 0, 1);
            MainMenu.SetData("Item", 1, 0);
            MainMenu.SetData("Run", 1, 1);

            mainFrame.AddContent(MainMenu);

            mainFrame.X = 0.5f * PokeEngine.ScreenWidth;
            mainFrame.Y = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            mainFrame.Width = PokeEngine.ScreenWidth - mainFrame.X;
            mainFrame.Height = PokeEngine.ScreenHeight - mainFrame.Y;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
            MainMenu.OnExitRequested += delegate { PokeEngine.ExitProgram(); };
        }

        void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            switch (e.SelectedData)
            {
                case "Attack":
                    currentFrame = attackFrame;
                    currentWidget = AttackMenu;
                    break;
                case "PKMN":
                    currentFrame = PKMNMenu;
                    currentWidget = PKMNMenu;
                    break;
                case "Item":
                    currentFrame = itemFrame;
                    currentWidget = ItemMenu;
                    break;
            }
        }
        public override bool HandleInput(Keys key)
        {
            return currentWidget.HandleInput(key);
        }

        public override void Setup(ContentManager content)
        {
            mainFrame.Setup(content);
            attackFrame.Setup(content);
            itemFrame.Setup(content);
            PKMNMenu.Setup(content);
            messageBox.Setup(content);
        }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            messageBox.Draw(time, batch);
            currentFrame.Draw(time, batch);
        }
    }
}