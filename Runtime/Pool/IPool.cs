
namespace CG.Foundation
{
	public interface IPool<T> where T : class
	{
		int Count
		{
			get;
		}

		T GetObject();

		void PutObject(T item);
	}
}
