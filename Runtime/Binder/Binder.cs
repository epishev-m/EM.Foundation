
namespace EM.Foundation
{
	using System;
	using System.Collections.Generic;
	using BindingKey = System.ValueTuple<object, object>;

	public class Binder :
		IBinder
	{
		#region IBinder

		public IBinding Bind<T>(
			object name = null)
		{
			return Bind(typeof(T), name);
		}

		public IBinding Bind(
			object key,
			object name = null)
		{
			Requires.IsNotNull(key, nameof(key));

			var binding = GetBinding(key, name) ??
				GetRawBinding(key, name);

			return binding;
		}

		public bool Unbind<T>(
			object name = null)
		{
			return Unbind(typeof(T), name);
		}

		public bool Unbind(
			object key,
			object name = null)
		{
			Requires.IsNotNull(key, nameof(key));

			var result = false;
			var bindingKey = new BindingKey(key, name);

			if (bindings.ContainsKey(bindingKey))
			{
				bindings.Remove(bindingKey);
				result = true;
			}

			return result;
		}

		public void Unbind(
			Predicate<IBinding> match)
		{
			var resultList = new List<KeyValuePair<(object, object), IBinding>>();

			foreach (var keyValue in bindings)
			{
				if (match(keyValue.Value))
				{
					resultList.Add(keyValue);
				}
			}

			foreach (var pair in resultList)
			{
				Unbind(pair.Key.Item1, pair.Key.Item2);
			}
		}

		public void UnbindAll()
		{
			bindings.Clear();
		}

		#endregion
		#region Binder

		protected readonly Dictionary<BindingKey, IBinding> bindings;

		public Binder()
		{
			bindings = new Dictionary<BindingKey, IBinding>(128);
		}

		protected virtual IBinding GetBinding(
			object key,
			object name = null)
		{
			Requires.IsNotNull(key, nameof(key));

			IBinding result = default;
			var bindingKey = new BindingKey(key, name);

			if (bindings.ContainsKey(bindingKey))
			{
				result = bindings[bindingKey];
			}

			return result;
		}

		protected virtual IBinding GetRawBinding(
			object key,
			object name)
		{
			return new Binding(key, name, BindingResolver);
		}

		protected virtual void BindingResolver(
			IBinding binding)
		{
			if (binding.Name != null)
			{
				var bindingKey = new BindingKey(binding.Key, null);

				if (bindings.ContainsKey(bindingKey))
				{
					bindings.Remove(bindingKey);
				}
			}

			var actualBindingKey = new BindingKey(binding.Key, binding.Name);

			if (!bindings.ContainsKey(actualBindingKey))
			{
				bindings.Add(actualBindingKey, binding);
			}
		}

		#endregion
	}
}
