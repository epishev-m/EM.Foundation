using System;
using System.Collections.Generic;

namespace EM.Foundation
{
	public abstract class CommandCompositeBase : CommandBase, ICommandComposite
	{
		#region ICommandComposite

		public ICommandComposite Add(ICommand command)
		{
			var unused = command ?? throw new ArgumentNullException(nameof(command));
			_queueCommands.Enqueue(command);

			return this;
		}

		public override void Clear()
		{
			base.Clear();
			_queueCommands.Clear();
		}

		#endregion
		#region CommandCompositeBase

		private readonly Queue<ICommand> _queueCommands = new Queue<ICommand>(16);

		protected int Count => _queueCommands.Count;

		protected ICommand Dequeue()
		{
			var command = default(ICommand);

			if (_queueCommands.Count > 0)
			{
				command = _queueCommands.Dequeue();
			}

			return command;
		}

		#endregion
	}
}
