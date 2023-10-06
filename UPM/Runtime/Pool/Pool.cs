namespace EM.Foundation
{

using System.Collections.Concurrent;

public class Pool<T> : IPool<T>
	where T : class
{
	private readonly ConcurrentBag<T> _instances = new();

	#region IPool

	public int Count => _instances.Count;

	public Result<T> GetObject()
	{
		if (_instances.TryTake(out var item))
		{
			return new SuccessResult<T>(item);
		}

		return new ErrorResult<T>(PoolStringResources.PoolIsEmpty(this));
	}

	public Result PutObject(T obj)
	{
		if (obj == null)
		{
			return new ErrorResult(PoolStringResources.PutNullObject(this));
		}

		if (obj is IPoolable {IsRestored: false} poolItem)
		{
			poolItem.Restore();
		}

		_instances.Add(obj);

		return new SuccessResult();
	}

	#endregion
}

}