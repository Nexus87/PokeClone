namespace MainModule.Graphics
{
    public class FieldSize
    {
        private int Height { get; set; }
        private int Width { get; set; }

        public FieldSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private bool Equals(FieldSize other)
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