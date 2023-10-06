namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Linq;
using BindingKey = System.ValueTuple<object, object>;

public class Binder : IBinder
{
	protected readonly Dictionary<BindingKey, IBinding> Bindings = new(128);

	#region IBinder

	public IBinding Bind<T>(object name = null)
	{
		return Bind(typeof(T), name);
	}

	public IBinding Bind(object key,
		object name = null)
	{
		Requires.NotNullParam(key, nameof(key));

		var binding = GetBinding(key, name) ??
					GetRawBinding(key, name);

		return binding;
	}

	public bool Unbind<T>(object name = null)
	{
		return Unbind(typeof(T), name);
	}

	public bool Unbind(object key,
		object name = null)
	{
		Requires.NotNullParam(key, nameof(key));

		var bindingKey = new BindingKey(key, name);

		if (!Bindings.ContainsKey(bindingKey))
		{
			return false;
		}

		Bindings.Remove(bindingKey);

		return true;
	}

	public void Unbind(Predicate<IBinding> match)
	{
		var resultList = Bindings.Where(keyValue => match(keyValue.Value)).ToArray();

		foreach (var pair in resultList)
		{
			Unbind(pair.Key.Item1, pair.Key.Item2);
		}
	}

	public void UnbindAll()
	{
		Bindings.Clear();
	}

	#endregion

	#region Binder

	protected IBinding GetBinding(object key,
		object name = null)
	{
		Requires.NotNullParam(key, nameof(key));

		IBinding result = default;
		var bindingKey = new BindingKey(key, name);

		if (Bindings.ContainsKey(bindingKey))
		{
			result = Bindings[bindingKey];
		}

		return result;
	}

	protected virtual IBinding GetRawBinding(object key,
		object name)
	{
		return new Binding(key, name, BindingResolver);
	}

	protected void BindingResolver(IBinding binding)
	{
		if (binding.Name != null)
		{
			var bindingKey = new BindingKey(binding.Key, null);

			if (Bindings.ContainsKey(bindingKey))
			{
				Bindings.Remove(bindingKey);
			}
		}

		var actualBindingKey = new BindingKey(binding.Key, binding.Name);

		if (!Bindings.ContainsKey(actualBindingKey))
		{
			Bindings.Add(actualBindingKey, binding);
		}
	}

	#endregion
}

}