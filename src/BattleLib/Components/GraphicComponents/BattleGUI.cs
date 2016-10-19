﻿using System;
using System.Linq;
using Base;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameService(typeof(IGUIService))]
    public class BattleGUI : IGUIService
    {
        private readonly MessageBox messageBox;
        private readonly Dialog messageFrame;

        public BattleGUI(ScreenConstants screen, GUIManager manager, 
            Dialog messageFrame, MessageBox messageBox, 
            MainMenuWidget mainWidget,
            MoveMenuWidget moveWidget, PokemonMenuWidget pokemonWidget,
            ItemMenuWidget itemWidget, IBattleStateService battleState,
            BattleData data) :
            this(screen, manager, messageFrame, messageBox, (IMenuWidget<MainMenuEntries>)mainWidget, moveWidget, pokemonWidget, itemWidget, battleState, data)
        {}
        
            internal BattleGUI(ScreenConstants screen, GUIManager manager, 
            Dialog messageFrame, MessageBox messageBox, 
            IMenuWidget<MainMenuEntries> mainWidget, 
            IMenuWidget<Move> moveWidget, IMenuWidget<Pokemon> pokemonWidget, 
            IMenuWidget<Item> itemWidget, IBattleStateService battleState, 
            BattleData data)
        {
            this.battleState = battleState;
            playerId = data.PlayerId;
            ai = data.Clients.First(id => !id.IsPlayer);
            this.moveWidget = moveWidget;
            this.itemWidget = itemWidget;
            this.mainWidget = mainWidget;
            this.pokemonWidget = pokemonWidget;

            this.messageBox = messageBox;
            this.messageFrame = messageFrame;

            InitMessageBox(screen, manager);

            InitMainMenu(screen, manager);
            InitAttackMenu(screen, manager);
            InitItemMenu(screen, manager);
            InitPKMNMenu(screen, manager);

        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private readonly ClientIdentifier ai;
        private readonly IMenuWidget<Move> moveWidget;
        private readonly IMenuWidget<Item> itemWidget;
        private readonly IMenuWidget<Pokemon> pokemonWidget;

        private readonly IBattleStateService battleState;
        private readonly ClientIdentifier playerId;

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
            battleState.SetMove(playerId, ai, e.SelectedData);
            moveWidget.IsVisible = false;
        }

        private void BackToMain(object sender, EventArgs e)
        {
            itemWidget.IsVisible = false;
            moveWidget.IsVisible = false;
            pokemonWidget.IsVisible = false;

            mainWidget.IsVisible = true;
        }

        private void InitAttackMenu(ScreenConstants screen, GUIManager manager)
        {
            moveWidget.SetCoordinates(
                X: screen.ScreenWidth / 2.0f,
                Y: 2.0f * screen.ScreenHeight / 3.0f,
                width: screen.ScreenWidth - screen.ScreenWidth / 2.0f,
                height: screen.ScreenHeight - 2.0f * screen.ScreenHeight / 3.0f
            );

            moveWidget.ItemSelected += AttackMenu_ItemSelected;
            moveWidget.ExitRequested += BackToMain;
            moveWidget.ExitRequested += delegate { moveWidget.ResetSelection(); };

            moveWidget.IsVisible = false;
            manager.AddWidget(moveWidget);
        }

        private void InitItemMenu(ScreenConstants screen, GUIManager manager)
        {
            itemWidget.XPosition = 3.0f * screen.ScreenWidth / 8.0f;
            itemWidget.YPosition = 1.0f * screen.ScreenHeight / 8.0f;

            itemWidget.Width = screen.ScreenWidth - itemWidget.XPosition;
            itemWidget.Height = (2.0f * screen.ScreenHeight / 3.0f) - itemWidget.YPosition;

            itemWidget.ItemSelected += ItemMenu_ItemSelected;
            itemWidget.ExitRequested += BackToMain;
            itemWidget.ExitRequested += delegate { itemWidget.ResetSelection(); };

            itemWidget.IsVisible = false;

            manager.AddWidget(itemWidget);
        }

        private void InitMainMenu(ScreenConstants screen, GUIManager manager)
        {
            mainWidget.XPosition = 0.5f * screen.ScreenWidth;
            mainWidget.YPosition = 2.0f * screen.ScreenHeight / 3.0f;
            mainWidget.Width = screen.ScreenWidth - mainWidget.XPosition;
            mainWidget.Height = screen.ScreenHeight - mainWidget.YPosition;

            mainWidget.ItemSelected += MainMenu_ItemSelected;
            //mainWidget.ExitRequested += delegate { manager.Exit(); };

            mainWidget.IsVisible = true;
            manager.AddWidget(mainWidget);
        }

        private void InitMessageBox(ScreenConstants screen, GUIManager manager)
        {
            messageFrame.AddWidget(messageBox);
            messageFrame.XPosition = 0;
            messageFrame.YPosition = 2.0f * screen.ScreenHeight / 3.0f;
            messageFrame.Width = screen.ScreenWidth;
            messageFrame.Height = screen.ScreenHeight - messageFrame.YPosition;

            messageFrame.IsVisible = true;
            manager.AddWidget(messageFrame);

            messageBox.OnAllLineShowed += AllLineShowedHandler;
        }

        private void AllLineShowedHandler(object sender, EventArgs e)
        {
            TextDisplayed(this, EventArgs.Empty);
        }

        private void InitPKMNMenu(ScreenConstants screen, GUIManager manager)
        {
            pokemonWidget.XPosition = 0;
            pokemonWidget.YPosition = 0;
            pokemonWidget.Width = screen.ScreenWidth;
            pokemonWidget.Height = 2.0f * screen.ScreenHeight / 3.0f;

            pokemonWidget.ItemSelected += PKMNMenu_ItemSelected;
            pokemonWidget.ExitRequested += BackToMain;
            pokemonWidget.ExitRequested += delegate { pokemonWidget.ResetSelection(); };

            pokemonWidget.IsVisible = false;

            manager.AddWidget(pokemonWidget);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            battleState.SetItem(playerId, playerId, e.SelectedData);
            itemWidget.IsVisible = false;
        }

        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<MainMenuEntries> e)
        {
            switch (e.SelectedData)
            {
                case MainMenuEntries.Attack:
                    mainWidget.IsVisible = false;
                    moveWidget.IsVisible = true;
                    break;

                case MainMenuEntries.PKMN:
                    mainWidget.IsVisible = false;
                    pokemonWidget.IsVisible = true; ;
                    break;

                case MainMenuEntries.Item:
                    mainWidget.IsVisible = false;
                    itemWidget.IsVisible = true;
                    break;
            }
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            battleState.SetCharacter(playerId, e.SelectedData);
            pokemonWidget.IsVisible = false;
        }

        public IMenuWidget<MainMenuEntries> mainWidget { get; set; }
    }
}