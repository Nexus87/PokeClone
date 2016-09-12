namespace MainModule
{
    public class FieldSize
    {
        private int height;
        private int width;

        public FieldSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        protected bool Equals(FieldSize other)
        {
            return height == other.height && width == other.width;
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
                return (height * 397) ^ width;
            }
        }
    }
}