namespace EM.Foundation
{

public interface IPool<T>
	where T : class
{
	int Count
	{
		get;
	}

	Result<T> GetObject();

	Result PutObject(T item);
}

}