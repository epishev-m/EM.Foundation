namespace EM.Foundation
{

using System.Collections.Generic;

public delegate void Resolver(IBinding binding);

public class Binding : IBinding
{
	private readonly LinkedList<object> _values = new();

	private readonly Resolver _resolver;

	#region IBinding

	public object Key
	{
		get;
	}

	public object Name
	{
		get;
		private set;
	}

	public IEnumerable<object> Values => _values.Count <= 0 ? null : _values;

	public IBinding To<T>()
	{
		return To(typeof(T));
	}

	public IBinding To(object value)
	{
		Requires.NotNull(value, nameof(value));

		_values.AddLast(value);
		_resolver?.Invoke(this);

		return this;
	}

	public IBinding ToSelf()
	{
		_values.AddLast(Key);
		_resolver?.Invoke(this);

		return this;
	}

	public IBinding ToName<T>()
	{
		return ToName(typeof(T));
	}

	public IBinding ToName(object name)
	{
		Requires.NotNull(name, nameof(name));

		Name = name;
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

	public Binding(object key,
		object name,
		Resolver resolver)
	{
		Requires.NotNull(key, nameof(key));

		Key = key;
		Name = name;
		_resolver = resolver;
	}

	#endregion
}

}
