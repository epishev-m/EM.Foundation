
namespace EM.Foundation
{
	public interface IBinding
	{
		object Key
		{
			get;
		}

		object Name
		{
			get;
		}

		object[] Values
		{
			get;
		}

		IBinding ToSelf();

		IBinding To<T>();

		IBinding To(object value);

		IBinding ToName<T>();

		IBinding ToName(object name);

		IBinding Named<T>();

		IBinding Named(object name);

		bool RemoveValue(object value);

		void RemoveAllValues();
	}
}
