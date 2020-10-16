using System;
using System.Collections.Generic;

namespace EM.Foundation
{
	public delegate void Resolver(IBinding binding);

	public class Binding : IBinding
	{
		#region IBinding

		public object Key => _key;

		public object Name => _name;

		public IEnumerable<object> Values => _values.Count <= 0 ? null : _values;

		public IBinding To<T>()
		{
			return To(typeof(T));
		}

		public IBinding To(object value)
		{
			var tempValue = value ?? throw new ArgumentNullException(nameof(value));
			_values.AddLast(tempValue);
			_resolver?.Invoke(this);

			return this;
		}

		public IBinding ToSelf()
		{
			_values.AddLast(_key);
			_resolver?.Invoke(this);

			return this;
		}

		public IBinding ToName<T>()
		{
			return ToName(typeof(T));
		}

		public IBinding ToName(object name)
		{
			_name = name ?? throw new ArgumentNullException(nameof(name));
			_resolver?.Invoke(this);

			return this;
		}

		public bool RemoveValue(object value)
		{
			return _values.Remove(value);
		}

		public void RemoveAllValues()
		{
			_values.Clear();
		}

		#endregion
		#region Binding

		private readonly LinkedList<object> _values = new LinkedList<object>();

		private readonly object _key;

		private readonly Resolver _resolver;

		private object _name;

		public Binding(object key, object name, Resolver resolver)
		{
			_key = key ?? throw new ArgumentNullException(nameof(key));
			_name = name;
			_resolver = resolver;
		}

		#endregion
	}
}
