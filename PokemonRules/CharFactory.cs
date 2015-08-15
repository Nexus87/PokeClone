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

namespace PokemonRules {
	
	public class CharFactory
	{

		readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(PKData[]));
        string File{ get; set; }
		PKData[] _data;
		PKData[] Data {
            get { 
                if (_data == null)
                    initList();
                return _data;
            } 
        }

		CharacterRules Rules { get; set;}

        void initList()
        {
            var inStream = new FileStream(File, FileMode.Open);
			_data = (PKData[])_serializer.ReadObject(inStream);
            inStream.Close();
        }
		public CharFactory (string file, CharacterRules rules)
		{
            File = file;
			Rules = rules;
		}

        public Pokemon getChar(string name)
        {
            var result = (from d in Data
                         where d.name.Equals(name)
                         select d).FirstOrDefault();
            
            return toChar(result);
        }

		public Pokemon getChar(int id)
		{
			var result = (from d in Data
			              where d.id == id
			              select d).FirstOrDefault ();
			
			return toChar (result);
		}

		Pokemon toChar(PKData data)
        {
            if (data == null)
                return null;


			Stats iv = Rules.generateIV ();
			Stats stats = new Stats () {
				HP = data.baseStats.HP + iv.HP,
				Atk = data.baseStats.Atk + iv.Atk,
				Def = data.baseStats.Def + iv.Def,
				SpAtk = data.baseStats.SpAtk + iv.SpAtk,
				SpDef = data.baseStats.SpDef + iv.SpDef,
				Speed = data.baseStats.Speed + iv.Speed
			};
			var builder = new PokemonBuilder(data);
			builder.setIV (iv).setStats (stats);
			return builder.build ();
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
			
	}
}

