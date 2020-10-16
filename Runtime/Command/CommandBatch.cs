
namespace EM.Foundation
{
	public sealed class CommandBatch :
		CommandCompositeBase
	{
		#region BaseCompositeCommand

		public override void Execute()
		{
			if (_canExecute)
			{
				_canExecute = false;
				_counter = Count;

				while (true)
				{
					var command = Dequeue();

					if (command == null)
					{
						break;
					}

					command.Done += () => OnDone();

					command.Execute();
				}
			}
		}

		#endregion
		#region CommandBatch

		private bool _canExecute = true;

		private int _counter;

		private void OnDone()
		{
			_counter--;

			if (_counter <= 0)
			{
				_canExecute = true;
				DoneInvoke();
			}
		}

		#endregion
	}
}
