
namespace EM.Foundation
{
	using System.Collections.Generic;

	public abstract class CommandCompositeBase :
		CommandBase,
		ICommandComposite
	{
		#region ICommandComposite

		public IEnumerable<ICommand> Commands => queueCommands;

		public ICommandComposite Add(
			ICommand command)
		{
			Requires.IsNotNull(command, nameof(command));

			var unused = command;
			queueCommands.Enqueue(command);

			return this;
		}

		#endregion
		#region CommandBase

		public override void Clear()
		{
			base.Clear();
			queueCommands.Clear();
		}

		#endregion
		#region CommandCompositeBase

		private readonly Queue<ICommand> queueCommands = new Queue<ICommand>(16);

		protected int Count => queueCommands.Count;

		protected ICommand Dequeue()
		{
			var command = default(ICommand);

			if (queueCommands.Count > 0)
			{
				command = queueCommands.Dequeue();
			}

			return command;
		}

		#endregion
	}
}
