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
    public class BattleGUI : AbstractGraphicComponent, IWidget
    {
        public BattleStateComponent BattleState { get; set; }
        public ClientIdentifier ID { get; set; }

        private TableWidget<string> MainMenu;
        private TableWidget<Move> AttackMenu;
        private TableWidget<Item> ItemMenu;
        private TableWidget<Pokemon> PKMNMenu;

        private Frame mainFrame = new Frame("border");
        private Frame attackFrame = new Frame("border");
        private Frame itemFrame = new Frame("border");

        private IGraphicComponent currentFrame;
        private IWidget currentMenu;
        private IWidget messageBox;

        public BattleGUI(Configuration config)
        {
            MainMenu = new TableWidget<string>(config);
            AttackMenu = new TableWidget<Move>(config);
            ItemMenu = new TableWidget<Item>(config);
            PKMNMenu = new TableWidget<Pokemon>(config);

            InitMainMenu();
            InitAttackMenu();
            InitItemMenu();
            InitPKMNMenu();
        }

        private void InitPKMNMenu()
        {
            var model = new DefaultListModel<Pokemon>();
            var list = new List<Pokemon>();
            PKData data = new PKData();
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN1"});
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN2" });

            //PKMNMenu.Model = model;
            PKMNMenu.X = 0;
            PKMNMenu.Y = 0;
            PKMNMenu.Width = Engine.ScreenWidth;
            PKMNMenu.Height = Engine.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            BattleState.SetCharacter(ID, e.SelectedData);
            currentFrame = null;
            currentMenu = messageBox;
        }

        private void InitItemMenu()
        {
            var model = new DefaultListModel<Item>();
            var list = new List<Item>();
            for (int i = 0; i < 20; i++)
                model.SetData(new Item { Name = "Item" + i }, i);

            //ItemMenu.Model = model;

            itemFrame.AddContent(ItemMenu);

            itemFrame.X = 3.0f * Engine.ScreenWidth / 8.0f;
            itemFrame.Y = 1.0f * Engine.ScreenHeight / 8.0f;

            itemFrame.Width = Engine.ScreenWidth - itemFrame.X;
            itemFrame.Height = (2.0f * Engine.ScreenHeight / 3.0f) - itemFrame.Y;

            ItemMenu.ItemSelected += ItemMenu_ItemSelected;
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, e.SelectedData);
            currentFrame = null;
            currentMenu = messageBox;
        }

        private void InitAttackMenu()
        {
            var model = new AttackModel();
            var list = new List<Move>();

            model.SetData(new Move(new MoveData { Name = "Move1" }), 0);
            model.SetData(new Move(new MoveData { Name = "Move2" }), 1);
            //AttackMenu.Model = model;

            attackFrame.AddContent(AttackMenu);
            attackFrame.X = Engine.ScreenWidth / 2.0f;
            attackFrame.Y = 2.0f * Engine.ScreenHeight / 3.0f;

            attackFrame.Width = Engine.ScreenWidth - attackFrame.X;
            attackFrame.Height = Engine.ScreenHeight - attackFrame.Y;

            AttackMenu.ItemSelected += AttackMenu_ItemSelected;
        }

        private void AttackMenu_ItemSelected(object sender, SelectionEventArgs<Move> e)
        {
            BattleState.SetMove(ID, e.SelectedData);
            currentFrame = null;
            currentMenu = messageBox;
        }

        private void InitMainMenu()
        {
            var model = new DefaultTableModel<string>();
            model.SetData("Attack", 0, 0);
            model.SetData("PKMN", 0, 1);
            model.SetData("Item", 1, 0);
            model.SetData("Run", 1, 1);

            //MainMenu.Model = model;
            mainFrame.AddContent(MainMenu);

            mainFrame.X = 0.5f * Engine.ScreenWidth;
            mainFrame.Y = 2.0f * Engine.ScreenHeight / 3.0f;
            mainFrame.Width = Engine.ScreenWidth - mainFrame.X;
            mainFrame.Height = Engine.ScreenHeight - mainFrame.Y;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
        }

        void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            switch (e.SelectedData)
            {
                case "Attack":
                    currentFrame = attackFrame;
                    currentMenu = AttackMenu;
                    break;
                case "PKMN":
                    currentFrame = PKMNMenu;
                    currentMenu = PKMNMenu;
                    break;
                case "Item":
                    currentFrame = itemFrame;
                    currentMenu = ItemMenu;
                    break;
            }
        }
        public void HandleInput(Keys key)
        {
            currentMenu.HandleInput(key);
        }

        public override void Setup(ContentManager content)
        {
            
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}