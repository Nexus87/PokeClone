﻿using Base;
using BattleLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public class BattleData
    {
        internal event EventHandler<PkmnChangedArgs> OnPkmnChange = (a, b) => { };

        private Pokemon playerPkmn;
        private Pokemon aiPkmn;

        public ClientIdentifier player;
        public ClientIdentifier ai;

        public Pokemon PlayerPkmn
        {
            get { return playerPkmn; }
            set
            {
                playerPkmn = value;
                OnPkmnChange(this, new PkmnChangedArgs { id = player });
            }
        }

        public Pokemon AIPkmn
        {
            get { return aiPkmn; }
            set
            {
                aiPkmn = value;
                OnPkmnChange(this, new PkmnChangedArgs { id = ai });
            }
        }

        public IClientCommand playerCommand;
        public IClientCommand aiCommand;
    }
}
