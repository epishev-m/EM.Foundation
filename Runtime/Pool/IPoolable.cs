
namespace CG.Foundation
{
	public interface IPoolable
	{
		bool IsRestored
		{
			get;
		}

		void Restore();
	}
}
