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
        #region ICharakter implementation
        public bool isKO()
        {
            return false;
        }
        public string Name
        {
            get
            {
                return "TestChar";
            }
        }
        public int HP
        {
            get
            {
                return 100;
            }
        }
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
