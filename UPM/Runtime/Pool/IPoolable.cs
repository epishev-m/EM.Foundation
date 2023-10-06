namespace EM.Foundation
{

public interface IPoolable
{
	bool IsRestored
	{
		get;
	}

	void Restore();
}

}