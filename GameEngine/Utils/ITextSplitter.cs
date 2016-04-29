namespace GameEngine.Utils
{
    public interface ITextSplitter
    {
        string GetString(int index);
        int Count { get; }
        void SplitText(int charsPerLine, string text);
    }
}
