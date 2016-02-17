﻿using Base;
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
    public class BattleGUI : IGUIService
    {
        public BattleStateComponent BattleState { get; set; }
        public ClientIdentifier ID { get; set; }

        private Dialog mainFrame;
        private Dialog attackFrame;
        private Dialog itemFrame;
        private Dialog pkmnFrame;
        private Dialog messageFrame;

        private MessageBox messageBox;

        public BattleGUI(Configuration config, PokeEngine game)
        {
            game.Services.AddService(typeof(IGUIService), this);

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

        private void InitMessageBox(Configuration config, PokeEngine game)
        {
            messageBox = new MessageBox(config, game);

            messageFrame.AddWidget(messageBox);
            messageFrame.X = 0;
            messageFrame.Y = 2.0f * PokeEngine.ScreenHeight / 3.0f;
            messageFrame.Width = PokeEngine.ScreenWidth;
            messageFrame.Height = PokeEngine.ScreenHeight - messageFrame.Y;

            messageFrame.IsVisible = true;
            game.GUIManager.AddWidget(messageFrame);
        }

        private void InitPKMNMenu(Configuration config, PokeEngine game)
        {
            var PKMNMenu = new TableWidget<Pokemon>(config, game);
            var model = new DefaultListModel<Pokemon>();

            PKData data = new PKData();
            model.SetData(new Pokemon(data, data.BaseStats) { Name = "PKMN1" }, 0);
            model.SetData(new Pokemon(data, data.BaseStats) { Name = "PKMN2" }, 1);

            PKMNMenu.Model = model;
            
            pkmnFrame.X = 0;
            pkmnFrame.Y = 0;
            pkmnFrame.Width = PokeEngine.ScreenWidth;
            pkmnFrame.Height = PokeEngine.ScreenHeight;

            PKMNMenu.ItemSelected += PKMNMenu_ItemSelected;
            PKMNMenu.OnExitRequested += BackToMain;

            pkmnFrame.AddWidget(PKMNMenu);
            pkmnFrame.IsVisible = false;

            game.GUIManager.AddWidget(pkmnFrame);
        }

        void BackToMain(object sender, EventArgs e)
        {
            itemFrame.IsVisible = false;
            attackFrame.IsVisible = false;
            pkmnFrame.IsVisible = false;

            mainFrame.IsVisible = true;
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            BattleState.SetCharacter(ID, e.SelectedData);
            pkmnFrame.IsVisible = false;
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
            itemFrame.IsVisible = false;

            game.GUIManager.AddWidget(itemFrame);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            BattleState.SetItem(ID, e.SelectedData);
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

            attackFrame.IsVisible = false;
            game.GUIManager.AddWidget(attackFrame);
        }

        private void AttackMenu_ItemSelected(object sender, SelectionEventArgs<Move> e)
        {
            BattleState.SetMove(ID, e.SelectedData);
            attackFrame.IsVisible = false;
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

            mainFrame.IsVisible = true;
            game.GUIManager.AddWidget(mainFrame);
        }

        void MainMenu_ItemSelected(object sender, SelectionEventArgs<string> e)
        {
            switch (e.SelectedData)
            {
                case "Attack":
                    mainFrame.IsVisible = false;
                    attackFrame.IsVisible = true;
                    break;
                case "PKMN":
                    mainFrame.IsVisible = false;
                    pkmnFrame.IsVisible = true;;
                    break;
                case "Item":
                    mainFrame.IsVisible = false;
                    itemFrame.IsVisible = true;
                    break;
            }
        }

        public void ShowMenu()
        {
            mainFrame.IsVisible = true;
        }

        public void SetText(string Text)
        {
            messageBox.DisplayText(Text); 
        }
    }
}