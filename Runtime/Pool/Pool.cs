using System;
using System.Collections.Concurrent;

namespace CG.Foundation
{
	public class Pool<T> : IPool<T> where T : class
	{
		#region IPool

		public int Count => _instances.Count;

		public T GetObject()
		{
			if (!_instances.TryTake(out var item))
			{
				if (_instanceProvider != null)
				{
					item = _instanceProvider.GetInstance<T>();
				}
			}

			return item;
		}

		public void PutObject(T obj)
		{
			var item = obj ?? throw new ArgumentNullException(nameof(obj));

			if (item is IPoolable poolItem && !poolItem.IsRestored)
			{
				poolItem.Restore();
			}

			_instances.Add(item);
		}

		#endregion
		#region Pool

		protected readonly ConcurrentBag<T> _instances = new ConcurrentBag<T>();

		protected readonly IInstanceProvider _instanceProvider;

		public Pool()
		{
			_instanceProvider = null;
		}

		public Pool(IInstanceProvider instanceProvider)
		{
			_instanceProvider = instanceProvider ?? throw new ArgumentNullException(nameof(instanceProvider));
		}

		#endregion
	}
}
