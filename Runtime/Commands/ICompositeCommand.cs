
namespace EM.Foundation
{
	public interface ICompositeCommand : ICommand
	{
		ICompositeCommand Add(ICommand command);
	}
}
