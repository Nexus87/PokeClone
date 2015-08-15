namespace Base
{

	public interface ICharakter
	{
		string Name { get; }
		int HP { get; }
		bool isKO();
	}
}

