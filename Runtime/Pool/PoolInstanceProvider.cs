namespace EM.Foundation
{

using System.Collections.Concurrent;

public class PoolInstanceProvider<T> : IPool<T>
	where T : class
{
	private readonly ConcurrentBag<T> _instances = new();

	private readonly IInstanceProvider<T> _instanceProvider;

	#region IPool

	public int Count => _instances.Count;

	public Result<T> GetObject()
	{
		if (_instances.TryTake(out var item))
		{
			return new SuccessResult<T>(item);
		}

		var result = _instanceProvider.GetInstance();

		if (result.Failure)
		{
			return new PoolErrorResult<T>(PoolStringResources.NoObjectsAvailable(),
				PoolStringResources.NoObjectsAvailable());
		}

		return result;
	}

	public Result PutObject(T obj)
	{
		if (obj == null)
		{
			return new ErrorResult(default, default);
		}

		if (obj is IPoolable {IsRestored: false} poolItem)
		{
			poolItem.Restore();
		}

		_instances.Add(obj);

		return new SuccessResult();
	}

	#endregion

	#region PoolInstanceProvider

	public PoolInstanceProvider(IInstanceProvider<T> instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}