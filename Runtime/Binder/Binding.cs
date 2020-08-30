using System;

namespace EM.Foundation
{
	public delegate void Resolver(IBinding binding);

	public class Binding : IBinding
	{
		#region IBinding

		public object Key => throw new NotImplementedException();

		public object Name => throw new NotImplementedException();

		public object[] Values => throw new NotImplementedException();

		public IBinding ToSelf()
		{
			throw new NotImplementedException();
		}

		public IBinding To<T>()
		{
			throw new NotImplementedException();
		}

		public IBinding To(object value)
		{
			throw new NotImplementedException();
		}

		public IBinding ToName<T>()
		{
			throw new NotImplementedException();
		}

		public IBinding ToName(object name)
		{
			throw new NotImplementedException();
		}

		public IBinding Named<T>()
		{
			throw new NotImplementedException();
		}

		public IBinding Named(object name)
		{
			throw new NotImplementedException();
		}

		public bool RemoveValue(object value)
		{
			throw new NotImplementedException();
		}

		public void RemoveAllValues()
		{
			throw new NotImplementedException();
		}

		#endregion
		#region Binding

		public Binding(object key, object name, Resolver resolver)
		{
		}

		#endregion
	}
}
