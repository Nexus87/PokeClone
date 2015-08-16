using Base;
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
			
        #endregion

    }
}
