﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EM.Foundation
{
	public delegate void Resolver(IBinding binding);

	public class Binding : IBinding
	{
		#region IBinding

		public object Key => _key;

		public object Name
		{
			get;
			private set;
		}

		public object[] Values => _values.Count <= 0 ? null : _values.ToArray();

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
			Name = name ?? throw new ArgumentNullException(nameof(name));
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

		public Binding(object key, object name, Resolver resolver)
		{
			_key = key ?? throw new ArgumentNullException(nameof(key));
			Name = name;
			_resolver = resolver;
		}

		#endregion
	}
}