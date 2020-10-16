
namespace EM.Foundation
{
	public sealed class CommandBatch :
		CommandCompositeBase
	{
		#region BaseCompositeCommand

		public override void Execute()
		{
			if (canExecute)
			{
				canExecute = false;
				counter = Count;

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

		private bool canExecute = true;

		private int counter;

		private void OnDone()
		{
			counter--;

			if (counter <= 0)
			{
				canExecute = true;
				DoneInvoke();
			}
		}

		#endregion
	}
}
