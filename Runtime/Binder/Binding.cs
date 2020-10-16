
namespace EM.Foundation
{
	using System.Collections.Generic;
	
	public delegate void Resolver(IBinding binding);

	public class Binding :
		IBinding
	{
		#region IBinding

		public object Key => key;

		public object Name => name;

		public IEnumerable<object> Values => values.Count <= 0 ? null : values;

		public IBinding To<T>()
		{
			return To(typeof(T));
		}

		public IBinding To(
			object value)
		{
			Requires.IsNotNull(value, nameof(value));

			var tempValue = value;
			values.AddLast(tempValue);
			resolver?.Invoke(this);

			return this;
		}

		public IBinding ToSelf()
		{
			values.AddLast(key);
			resolver?.Invoke(this);

			return this;
		}

		public IBinding ToName<T>()
		{
			return ToName(typeof(T));
		}

		public IBinding ToName(
			object name)
		{
			Requires.IsNotNull(name, nameof(name));

			this.name = name;
			resolver?.Invoke(this);

			return this;
		}

		public bool RemoveValue(
			object value)
		{
			return values.Remove(value);
		}

		public void RemoveAllValues()
		{
			values.Clear();
		}

		#endregion
		#region Binding

		private readonly LinkedList<object> values = new LinkedList<object>();

		private readonly object key;

		private readonly Resolver resolver;

		private object name;

		public Binding(
			object key,
			object name,
			Resolver resolver)
		{
			Requires.IsNotNull(key, nameof(key));

			this.key = key;
			this.name = name;
			this.resolver = resolver;
		}

		#endregion
	}
}
