//
//  CharFactory.cs
//
//  Author:
//       Nexxuz0 <>
//
//  Copyright (c) 2015 Nexxuz0
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//

using System.Linq;
using System.Runtime.Serialization.Json;
using System.IO;
using Base;
using System.Collections.Generic;

namespace PokemonRules {
	
	public class CharFactory
	{

		readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(PKData[]));
        string File{ get; set; }

		PKData[] Data{ get; set; }

		ICharacterRules Rules { get; set;}

        void initList()
        {
            using (var inStream = new FileStream(File, FileMode.Open))
            {
                Data = (PKData[])_serializer.ReadObject(inStream);
            }
        }

		public CharFactory (string file, ICharacterRules rules)
		{
            File = file;
			Rules = rules;
			initList ();
		}
			
		public Pokemon GetChar(int id)
		{
			var result = (from d in Data
			              where d.Id == id
			              select d).FirstOrDefault ();

			return result == null ? null : Rules.ToPokemon (result);
			
		}
			
		public Pokemon GetChar(int id, int level){
			var charakter = GetChar(id);
			Rules.ToLevel (charakter, level);
			return charakter;
		}

		public void WriteData(PKData data)
        {
            var wrapper = new [] { data };
            using (var file = new FileStream(File, FileMode.Create))
            {
                _serializer.WriteObject(file, wrapper);
            }
        }

        public IEnumerable<int> Ids
        {
            get
            {
                return from data in Data
                       select data.Id;
            }
        }
	}
}

