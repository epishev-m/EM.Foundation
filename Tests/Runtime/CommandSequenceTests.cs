using EM.Foundation;
using NUnit.Framework;

internal sealed class CommandSequenceTests
{
	[Test]
	public void BaseCommand_Execute_Success()
	{
		// Arrange
		var command1 = new Command();
		var command2 = new Command();
		var command3 = new Command();

		// Act
		var sequence = new CommandSequence();
		sequence.Add(command1)
			.Add(command2)
			.Add(command3)
			.Execute();

		//Assert
		Assert.AreEqual(command1.Count, 1);
		Assert.AreEqual(command2.Count, 2);
		Assert.AreEqual(command3.Count, 3);
	}

	private sealed class Command : CommandBase
	{
		#region BaseCommand

		public override void Execute()
		{
			Counter++;
			Count = Counter;
			DoneInvoke();
		}

		#endregion
		#region Command

		public static int Counter = 0;

		public int Count
		{
			get;
			private set;
		}

		#endregion
	}
}
