using Base.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Move
    {
        public override string ToString()
        {
            return Data.Name;
        }

        public MoveData Data { get; private set; }
        public int RemainingPP { get; set; }
        public Move(MoveData data)
        {
            if (data == null) throw new ArgumentNullException("data", "Argument should not be null");

            Data = data;
            RemainingPP = data.PP;
        }
    }
}
