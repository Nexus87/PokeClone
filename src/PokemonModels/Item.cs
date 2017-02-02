using System;

namespace Pokemon.Models
{
    public class Item
    {
        public Item()
        {
            StackSize = 1;
        }
        public String Name { get; set; }
        public int StackSize { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
