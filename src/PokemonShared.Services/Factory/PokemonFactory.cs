// CharFactory.cs
// 
// Author: Nexxuz0 <>
// 
// Copyright (c) 2015 Nexxuz0
// 
// This program is free software; you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation; either version 2 of the License, or (at your option) any later
// version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// this program; if not, write to the Free Software Foundation, Inc., 59 Temple
// Place, Suite 330, Boston, MA 02111-1307 USA

using System.Collections.Generic;
using PokemonShared.Models;
using PokemonShared.Services.Rules;

namespace PokemonShared.Services.Factory
{
    public class PokemonFactory
    {
        private readonly IPokemonRepository repository;
        private readonly IPokemonRules rules;

        public PokemonFactory(IPokemonRepository repository, IPokemonRules rules)
        {
            this.rules = rules;
            this.repository = repository;
        }

        public IEnumerable<int> Ids { get { return repository.Ids; } }

        public Pokemon GetPokemon(int id)
        {
            return GetPokemon(id, 1);
        }

        public Pokemon GetPokemon(int id, int level)
        {
            var charakter = rules.FromPokemonData(repository.GetPokemonData(id));
            rules.ToLevel(charakter, level);

            return charakter;
        }
    }
}