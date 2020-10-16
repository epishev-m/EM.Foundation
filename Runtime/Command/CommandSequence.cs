
namespace EM.Foundation
{
	public sealed class CommandSequence :
		CommandCompositeBase
	{
		#region BaseCompositeCommand

		public override void Execute()
		{
			if (canExecute)
			{
				canExecute = false;
				currentCommand = Dequeue();

				if (currentCommand != null)
				{
					currentCommand.Done += OnDone;
					currentCommand.Execute();
				}
				else
				{
					canExecute = true;
					OnDone();
				}
			}
		}

		#endregion
		#region CommandSequence

		private bool canExecute = true;

		private ICommand currentCommand = default;

		private void OnDone()
		{
			if (currentCommand != null)
			{
				currentCommand.Done -= OnDone;
				currentCommand = null;
				canExecute = true;
				Execute();
			}
			else
			{
				DoneInvoke();
			}
		}

		#endregion
	}
}
