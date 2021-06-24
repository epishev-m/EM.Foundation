using EM.Foundation;
using NUnit.Framework;

internal sealed class CommandSequenceTests
{
	[Test]
	public void BaseCommand_Execute_Success()
	{
		// Arrange
		Command.Counter = 0;
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
		Assert.AreEqual(1, command1.Count);
		Assert.AreEqual(2, command2.Count);
		Assert.AreEqual(3, command3.Count);
	}

	private sealed class Command :
		CommandBase
	{
		public static int Counter;

		#region CommandBase

		public override void Execute()
		{
			Counter++;
			Count = Counter;
			DoneInvoke();
		}

		#endregion
		#region Command

		public int Count
		{
			get;
			private set;
		}

		#endregion
	}
}
