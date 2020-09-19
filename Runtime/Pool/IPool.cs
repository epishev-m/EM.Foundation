
namespace CG.Foundation
{
	public interface IPool<T> where T : class
	{
		int CountInstance
		{
			get;
		}

		T GetObject();

		void PutObject(T item);
	}
}
