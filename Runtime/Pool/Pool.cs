
namespace EM.Foundation
{
	using System;
	using System.Collections.Concurrent;

	public class Pool<T> :
		IPool<T>
		where T : class
	{
		#region IPool

		public int Count => instances.Count;

		public T GetObject()
		{
			if (!instances.TryTake(out var item))
			{
				if (instanceProvider != null)
				{
					var instance = instanceProvider.GetInstance();
					item = instance is T ? instance as T : throw new Exception();
				}
			}

			return item;
		}

		public void PutObject(
			T obj)
		{
			Requires.IsNotNull(obj, nameof(obj));

			if (obj is IPoolable poolItem && !poolItem.IsRestored)
			{
				poolItem.Restore();
			}

			instances.Add(obj);
		}

		#endregion
		#region Pool

		protected readonly ConcurrentBag<T> instances = new ConcurrentBag<T>();

		protected readonly IInstanceProvider instanceProvider;

		public Pool()
		{
			instanceProvider = null;
		}

		public Pool(
			IInstanceProvider instanceProvider)
		{
			Requires.IsNotNull(instanceProvider, nameof(instanceProvider));

			this.instanceProvider = instanceProvider;
		}

		#endregion
	}
}
