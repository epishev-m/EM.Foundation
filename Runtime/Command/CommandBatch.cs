namespace EM.Foundation
{

public sealed class CommandBatch :
	CommandCompositeBase
{
	private bool canExecute = true;

	private int counter;

	#region BaseCompositeCommand

	public override void Execute()
	{
		if (!canExecute)
		{
			return;
		}

		canExecute = false;
		counter = Count;

		while (true)
		{
			var command = Dequeue();

			if (command == null)
			{
				break;
			}

			command.Done += OnDone;

			command.Execute();
		}
	}

	#endregion
	#region CommandBatch

	private void OnDone()
	{
		counter--;

		if (counter > 0)
		{
			return;
		}

		canExecute = true;
		DoneInvoke();
	}

	#endregion
}

}
