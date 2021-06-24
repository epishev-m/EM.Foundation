namespace EM.Foundation
{
using System.Collections.Generic;

public delegate void Resolver(IBinding binding);

public class Binding :
	IBinding
{
	private readonly LinkedList<object> values = new LinkedList<object>();

	private readonly Resolver resolver;

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

	public IEnumerable<object> Values => values.Count <= 0 ? null : values;

	public IBinding To<T>()
	{
		return To(typeof(T));
	}

	public IBinding To(
		object value)
	{
		Requires.NotNull(value, nameof(value));

		values.AddLast(value);
		resolver?.Invoke(this);

		return this;
	}

	public IBinding ToSelf()
	{
		values.AddLast(Key);
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
		Requires.NotNull(name, nameof(name));

		Name = name;
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

	public Binding(
		object key,
		object name,
		Resolver resolver)
	{
		Requires.NotNull(key, nameof(key));

		Key = key;
		Name = name;
		this.resolver = resolver;
	}

	#endregion
}

}
