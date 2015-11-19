using Base;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    public class BattleGUI : AbstractGraphicComponent, IWidget
    {
        public event EventHandler GetFocus;
        public event EventHandler<VisibiltyChangeArgs> VisiblityChanged;
        public bool IsVisible { get; set; }

        private TableWidget<string> MainMenu;
        private TableWidget<Move> AttackMenu;
        private TableWidget<Item> ItemMenu;
        private TableWidget<Pokemon> PKMNMenu;

        private Frame mainFrame = new Frame("border");
        private Frame attackFrame = new Frame("border");
        private Frame itemFrame = new Frame("border");

        private IWidget currentFrame;
        private IWidget currentMenu;

        public BattleGUI(Configuration config)
        {
            MainMenu = new TableWidget<string>(config);
            AttackMenu = new TableWidget<Move>(config);
            ItemMenu = new TableWidget<Item>(config);
            PKMNMenu = new TableWidget<Pokemon>(config);

            InitMainMenu();
            InitAttackMenu();
        }

        private void InitAttackMenu()
        {
            var model = new AttackModel();
            var list = new List<Move>();
            list.Add(new Move(new MoveData { Name = "Move1" }));
            list.Add(new Move(new MoveData { Name = "Move2" }));

            model.Items = list;
            AttackMenu.Model = model;

            attackFrame.AddContent(AttackMenu);
        }

        private void InitMainMenu()
        {
            var model = new DefaultTableModel<string>();
            model.Items = new string[,] { { "Attack", "PKMN" }, { "Item", "Run" } };

            MainMenu.Model = model;
            mainFrame.AddContent(MainMenu);

            mainFrame.X = 0.5f * Engine.ScreenWidth;
            mainFrame.Y = 2.0f * Engine.ScreenHeight / 3.0f;
            mainFrame.Width = Engine.ScreenWidth - mainFrame.X;
            mainFrame.Height = Engine.ScreenHeight - mainFrame.Y;

            MainMenu.ItemSelected += MainMenu_ItemSelected;
        }

        void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            throw new NotImplementedException();
        }
        public void HandleInput(Keys key)
        {
            currentMenu.HandleInput(key);
        }

        public override void Setup(ContentManager content)
        {
            
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}