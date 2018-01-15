//using System;
//using BattleMode.Graphic;
//using BattleMode.Shared;
//using GameEngine.Entities;
//using PokemonShared.Models;

//namespace BattleMode.Core.Components
//{
//    internal class SetStatusEvent : IEvent
//    {
//        private readonly StatusCondition _condition;
//        private readonly IBattleGraphicController _graphic;
//        private readonly ClientIdentifier _id;

//        public SetStatusEvent(IBattleGraphicController graphic, ClientIdentifier id, StatusCondition condition)
//        {
//            _graphic = graphic;
//            _id = id;
//            _condition = condition;
//        }

//        public event EventHandler EventProcessed;

//        public void Dispatch()
//        {
//            _graphic.SetPokemonStatus(_id, _condition);
//            EventProcessed?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}