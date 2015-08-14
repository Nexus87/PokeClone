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
using System;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace PokemonRules
{
	public class CharFactory
	{
        //TODO make this class private!
        [DataContract]
        public class PrimitiveData
        {
            [DataMember]
            public string name;
            [DataMember]
            public int id;
            [DataMember]
            public int hp;
            [DataMember]
            public int atk;
            [DataMember]
            public int def;
            [DataMember]
            public int spatk;
            [DataMember]
            public int spdef;
            [DataMember]
            public int init;
            [DataMember]
            public PokemonType type1;
            [DataMember]
            public PokemonType type2;

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is PrimitiveData))
                    return false;

                PrimitiveData other = (PrimitiveData)obj;
                return name.Equals(other.name) &&
                    id == other.id &&
                    hp == other.hp &&
                    atk == other.atk &&
                    def == other.def &&
                    spatk == other.spatk &&
                    spdef == other.spdef &&
                    init == other.init &&
                    type1 == other.type1 &&
                    type2 == other.type2;
            }
        }

        DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(PrimitiveData[]));
        string File{ get; set; }
        PrimitiveData[] _data;
        PrimitiveData[] Data {
            get { 
                if (_data == null)
                    initList();
                return _data;
            } 
        }

        private void initList()
        {
            var inStream = new FileStream(File, FileMode.Open);
            _data = (PrimitiveData[])_serializer.ReadObject(inStream);
            inStream.Close();
        }
		public CharFactory (string file)
		{
            File = file;
		}

        public PokemonV1 getChar(string name)
        {
            var result = (from d in Data
                         where d.name.Equals(name)
                         select d).FirstOrDefault();
            
            return toChar(result);
        }

        private PokemonV1 toChar(PrimitiveData data)
        {
            if (data == null)
                return null;

            return new PokemonV1(data.hp, data.name)
            {
                Attack = data.atk,
                Defense = data.def,
                SpAtk = data.spatk,
                SpDef = data.spdef,
                Speed = data.init,
                Type1 = data.type1,
                Type2 = data.type2
            };
        }

        public PrimitiveData getData()
        {
            var file = new FileStream(File, FileMode.Open);
            var ret = _serializer.ReadObject(file);
            file.Close();
            return ((PrimitiveData[]) ret)[0];
        }

        public void writeData(PrimitiveData data)
        {
            var wrapper = new PrimitiveData[] { data };
            var file = new FileStream(File, FileMode.Create);
            _serializer.WriteObject(file, wrapper);
            file.Close();
        }
			
	}
}

