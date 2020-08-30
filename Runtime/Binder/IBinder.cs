
namespace EM.Foundation
{
	public interface IBinder
	{
		IBinding Bind<T>(object name = null);

		IBinding Bind(object key, object name = null);

		void Unbind<T>(object name = null);

		void Unbind(object key, object name = null);

		IBinding GetBinding<T>(object name = null);

		IBinding GetBinding(object key, object name = null);
	}
}
