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

		CharacterRules Rules { get; set;}

        void initList()
        {
            var inStream = new FileStream(File, FileMode.Open);
			Data = (PKData[]) _serializer.ReadObject(inStream);
            inStream.Close();
        }

		public CharFactory (string file, CharacterRules rules)
		{
            File = file;
			Rules = rules;
			initList ();
		}
			
		public Pokemon getChar(int id)
		{
			var result = (from d in Data
			              where d.Id == id
			              select d).FirstOrDefault ();

			return result == null ? null : Rules.toPokemon (result);
			
		}
			
		public Pokemon getChar(int id, int level){
			var charakter = getChar(id);
			Rules.toLevel (charakter, level);
			return charakter;
		}

		public PKData getData()
        {
            var file = new FileStream(File, FileMode.Open);
            var ret = _serializer.ReadObject(file);
            file.Close();
			return ((PKData[]) ret)[0];
        }

		public void writeData(PKData data)
        {
            var wrapper = new [] { data };
            var file = new FileStream(File, FileMode.Create);
            _serializer.WriteObject(file, wrapper);
            file.Close();
        }

        public IEnumerable<int> getIds()
        {
            return from data in Data
                   select data.Id;
        }
	}
}

