using Base;
namespace BattleLibTest
{
	class TestChar : ICharacter
    {
        public TestChar() { HP = 100; }
        #region ICharakter implementation
        public bool IsKO()
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


        public int Id
        {
            get { return 1; }
        }
    }
}
