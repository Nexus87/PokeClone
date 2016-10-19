namespace GameEngineTest.Graphics
{
    public class TestType
    {
        public TestType() { }
        public TestType(string testString)
        {
            this.testString = testString;
        }

        public string testString;
        public override string ToString()
        {
            return testString;
        }
    }
}