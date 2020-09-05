
namespace EM.Foundation
{
	public sealed class CommandSequence: BaseCompositeCommand
	{
		#region BaseCompositeCommand

		public override void Execute()
		{
			if (_canExecute)
			{
				_canExecute = false;
				_currentCommand = Dequeue();

				if (_currentCommand != null)
				{
					_currentCommand.Done += OnDone;
					_currentCommand.Execute();
				}
				else
				{
					_canExecute = true;
					OnDone();
				}
			}
		}

		#endregion
		#region CommandSequence

		private bool _canExecute = true;

		private ICommand _currentCommand = default;

		private void OnDone()
		{
			if (_currentCommand != null)
			{
				_currentCommand.Done -= OnDone;
				_currentCommand = null;
				_canExecute = true;
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
