namespace Base
{

	public interface ICharakter
	{
        int Id { get; }
		string Name { get; }
		int HP { get; }
		bool IsKO();
	}
}

