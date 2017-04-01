using System;
using System.Linq;
using GameEngine.Tools;
using PokemonShared.Data;
using PokemonShared.Models;

namespace PokemonShared.Service
{
    public class PokemonService
    {
        private readonly IStorage<PokemonData> _pokemonDatas;
        private readonly IStorage<MoveSetItem> _moveSetItems;
        private readonly MoveService _moveService;
        private readonly Random _random = new Random();

        public PokemonService(IStorage<PokemonData> pokemonDatas, IStorage<MoveSetItem> moveSetItems, MoveService moveService)
        {
            _pokemonDatas = pokemonDatas;
            _moveSetItems = moveSetItems;
            _moveService = moveService;
        }

        public Pokemon GetPokemon(int id, int level)
        {
            var baseData = _pokemonDatas.Single(x => x.Id == id);
            var iv = new Stats
            {
                Atk = _random.Next(16),
                Def = _random.Next(16),
                Hp = _random.Next(16),
                SpAtk = _random.Next(16),
                SpDef = _random.Next(16),
                Speed = _random.Next(16)
            };

            var stats = new Stats
            {
                Atk = baseData.BaseStats.Atk + iv.Atk,
                Hp = baseData.BaseStats.Hp + iv.Hp,
                Def = baseData.BaseStats.Def + iv.Def,
                SpAtk = baseData.BaseStats.SpAtk + iv.SpAtk,
                Speed = baseData.BaseStats.Speed + iv.Speed,
                SpDef = baseData.BaseStats.SpDef + iv.SpDef
            };

            var pokemon = new Pokemon(baseData, 1, null, stats, iv);
            var moveIds = _moveSetItems
                .Where(x => x.PokemonId == id && x.Level <= level)
                .Select(x => x.MoveId);

            var moves = _moveService.GetMoves(moveIds)
                .Reverse()
                .Take(4)
                .Reverse()
                .ToList();

            for (var i = 0; i < moves.Count; i++)
            {
                pokemon.SetMove(i, moves[i]);
            }

            return LevelUp(pokemon, level);
        }

        public Pokemon LevelUp(Pokemon source, int targetLevel)
        {
            // TODO implement
            source.Level = targetLevel;
            return source;
        }
    }
}
