using System;

namespace CG.Foundation
{
	public interface IInstanceProvider
	{
		T GetInstance<T>(object name = null) where T : class;

		object GetInstance(Type key, object name = null);
	}
}
