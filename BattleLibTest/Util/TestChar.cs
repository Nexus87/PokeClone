using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleLib;
namespace BattleLibTest
{
    class TestChar : ICharakter
    {
        public TestChar() { HP = 100; }
        #region ICharakter implementation
        public bool isKO()
        {
            return HP == 0;
        }
        public string Name
        {
            get
            {
                return "TestChar";
            }
        }
        public int HP { get; set; }

        public string Status
        {
            get
            {
                return "Status";
            }
        }
        #endregion

    }
}
