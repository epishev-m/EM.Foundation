
namespace EM.Foundation
{
	using System.Collections.Generic;
	
	public interface ICommandComposite :
		ICommand
	{
		IEnumerable<ICommand> Commands
		{
			get;
		}

		ICommandComposite Add(
			ICommand command);
	}
}
