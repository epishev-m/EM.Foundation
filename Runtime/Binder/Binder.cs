
namespace EM.Foundation
{
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

			if (_bindings.ContainsKey(bindingKey))
			{
				_bindings.Remove(bindingKey);
				result = true;
			}

			return result;
		}

		public IBinding GetBinding<T>(
			object name = null)
		{
			return GetBinding(typeof(T), name);
		}

		public IBinding GetBinding(
			object key,
			object name = null)
		{
			Requires.IsNotNull(key, nameof(key));

			IBinding result = default;
			var bindingKey = new BindingKey(key, name);

			if (_bindings.ContainsKey(bindingKey))
			{
				result = _bindings[bindingKey];
			}

			return result;
		}

		#endregion
		#region Binder

		protected readonly Dictionary<BindingKey, IBinding> _bindings;

		public Binder()
		{
			_bindings = new Dictionary<BindingKey, IBinding>(128);
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

				if (_bindings.ContainsKey(bindingKey))
				{
					_bindings.Remove(bindingKey);
				}
			}

			var actualBindingKey = new BindingKey(binding.Key, binding.Name);

			if (!_bindings.ContainsKey(actualBindingKey))
			{
				_bindings.Add(actualBindingKey, binding);
			}
		}

		#endregion
	}
}
