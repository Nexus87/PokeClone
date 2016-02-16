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

        private Dialog mainFrame;
        private Dialog attackFrame;
        private Dialog itemFrame;
        private Dialog pkmnFrame;
        private Dialog messageFrame;

        private IWidget currentFrame;
        private MessageBox messageBox;

        public BattleGUI(Configuration config, PokeEngine game) : 
            base(game)
        {
            mainFrame = new Dialog("border", game);
            attackFrame = new Dialog("border", game);
            itemFrame = new Dialog("border", game);
            pkmnFrame = new Dialog(game);
            messageFrame = new Dialog("border", game);

            messageBox = new MessageBox(config, game);

            InitMainMenu(config, game);
            InitAttackMenu(config, game);
            InitItemMenu(config, game);
            InitPKMNMenu(config, game);
            InitMessageBox();

            currentFrame = mainFrame;
        }

        private void InitMessageBox()
        {
            messageFrame.AddWidget(messageBox);
            messageFrame.X = 0;
            messageFrame.Y = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            messageFrame.Width = PokeEngine.ScreenWidth;
            messageFrame.Height = PokeEngine.ScreenHeight - messageFrame.Y;
        }

        private void InitPKMNMenu(Configuration config, PokeEngine game)
        {
            var PKMNMenu = new TableWidget<Pokemon>(config, game);
            var model = new DefaultListModel<Pokemon>();

            var list = new List<Pokemon>();
            PKData data = new PKData();
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN1"});
            list.Add(new Pokemon(data, data.BaseStats) { Name = "PKMN2" });

            PKMNMenu.Model = model;
            pkmnFrame.X = 0;
            pkmnFrame.Y = 0;
            pkmnFrame.Width = PokeEngine.ScreenWidth;
            pkmnFrame.Height = PokeEngine.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.OnExitRequested += BackToMain;

            pkmnFrame.AddWidget(PKMNMenu);
        }

        void BackToMain(object sender, EventArgs e)
        {
            currentFrame = mainFrame;
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            BattleState.SetCharacter(ID, e.SelectedData);
            currentFrame = messageFrame;
        }

        private void InitItemMenu(Configuration config, PokeEngine game)
        {

            var ItemMenu = new TableWidget<Item>(config, game);
            var model = new DefaultListModel<Item>();
            var list = new List<Item>();
            for (int i = 0; i < 20; i++)
                model.SetData(new Item { Name = "Item" + i }, i);

            ItemMenu.Model = model;

            itemFrame.X = 3.0f * PokeEngine.ScreenWidth / 8.0f;
            itemFrame.Y = 1.0f * PokeEngine.ScreenHeight / 8.0f;

            itemFrame.Width = PokeEngine.ScreenWidth - itemFrame.X;
            itemFrame.Height = (2.0f * PokeEngine.ScreenHeight / 3.0f) - itemFrame.Y;

            ItemMenu.ItemSelected += ItemMenu_ItemSelected;
            ItemMenu.OnExitRequested += BackToMain;

            itemFrame.AddWidget(ItemMenu);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, e.SelectedData);
            currentFrame = messageBox;
            HideDialog();
        }

        private void HideDialog()
        {
            mainFrame.IsVisible = false;
            pkmnFrame.IsVisible = false;
            attackFrame.IsVisible = false;
            itemFrame.IsVisible = false;
        }

        private void InitAttackMenu(Configuration config, PokeEngine game)
        {
            var AttackMenu = new TableWidget<Move>(config, game);
            var model = new AttackModel();
            var list = new List<Move>();

            model.SetData(new Move(new MoveData { Name = "Move1" }), 0);
            model.SetData(new Move(new MoveData { Name = "Move2" }), 1);
            AttackMenu.Model = model;

            attackFrame.AddWidget(AttackMenu);
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
            currentFrame = messageBox;
        }

        private void InitMainMenu(Configuration config, PokeEngine game)
        {
            var MainMenu = new TableWidget<string>(config, game);
            MainMenu.SetData("Attack", 0, 0);
            MainMenu.SetData("PKMN", 0, 1);
            MainMenu.SetData("Item", 1, 0);
            MainMenu.SetData("Run", 1, 1);

            mainFrame.AddWidget(MainMenu);

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
                    break;
                case "PKMN":
                    currentFrame = pkmnFrame;
                    break;
                case "Item":
                    currentFrame = itemFrame;
                    break;
            }
        }
        public override bool HandleInput(Keys key)
        {
            return currentFrame.HandleInput(key);
        }

        public override void Setup(ContentManager content)
        {
            mainFrame.Setup(content);
            attackFrame.Setup(content);
            itemFrame.Setup(content);
            pkmnFrame.Setup(content);
            messageFrame.Setup(content);
        }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            messageFrame.Draw(time, batch);
            if(currentFrame != messageBox)
                currentFrame.Draw(time, batch);
        }
    }
}