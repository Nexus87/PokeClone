namespace PokemonShared.Models
{
    public class Item
    {
        public Item()
        {
            StackSize = 1;
        }
        public string Name { get; set; }
        public int StackSize { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
