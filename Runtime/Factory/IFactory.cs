
namespace EM.Foundation
{
	public interface IFactory
	{
		bool Create(out object instance);
	}
}