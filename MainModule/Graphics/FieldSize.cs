namespace MainModule.Graphics
{
    public class FieldSize
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public FieldSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        protected bool Equals(FieldSize other)
        {
            return Height == other.Height && Width == other.Width;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FieldSize) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Height * 397) ^ Width;
            }
        }
    }
}