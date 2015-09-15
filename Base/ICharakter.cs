namespace Base
{

	public interface ICharacter
	{
        int Id { get; }
		string Name { get; }
		int HP { get; }
		bool IsKO();
	}
}

