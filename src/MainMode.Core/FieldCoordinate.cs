namespace MainMode.Core
{
    public struct FieldCoordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public FieldCoordinate(int X, int Y) : this()
        {
            this.X = X;
            this.Y = Y;
        }
    }
}