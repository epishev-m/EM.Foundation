
namespace EM.Foundation
{
	public interface IBinder
	{
		IBinding Bind<T>(object name = null);

		IBinding Bind(object key, object name = null);

		bool Unbind<T>(object name = null);

		bool Unbind(object key, object name = null);

		IBinding GetBinding<T>(object name = null);

		IBinding GetBinding(object key, object name = null);
	}
}
