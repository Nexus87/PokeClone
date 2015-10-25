using System;

namespace Base
{
    public class Item
    {
        public String Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
