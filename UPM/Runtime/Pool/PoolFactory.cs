namespace EM.Foundation
{

using System.Collections.Concurrent;

public class PoolFactory<T> : IPool<T>
	where T : class
{
	private readonly ConcurrentBag<T> _instances = new();

	private readonly IFactory<T> _factory;

	#region IPool

	public int Count => _instances.Count;

	public virtual Result<T> GetObject()
	{
		if (_instances.TryTake(out var item))
		{
			return new SuccessResult<T>(item);
		}

		var result = _factory.Create();

		if (result.Failure)
		{
			return new ErrorResult<T>(PoolStringResources.InstanceProviderReturnedNull(this));
		}

		return result;
	}

	public virtual Result PutObject(T obj)
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

	#region PoolFactory

	public PoolFactory(IFactory<T> factory)
	{
		Requires.NotNullParam(factory, nameof(factory));

		_factory = factory;
	}

	#endregion
}

}