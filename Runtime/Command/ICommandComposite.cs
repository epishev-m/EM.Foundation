
namespace EM.Foundation
{
	public interface ICommandComposite : ICommand
	{
		ICommandComposite Add(ICommand command);
	}
}
