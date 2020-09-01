using EM.Foundation;
using NUnit.Framework;

internal sealed class BaseCommandTests
{
	[Test]
	public void BaseCommand_ExecuteAndSubscribeEvent_ResultTrue()
	{
		// Arrange
		var actual = false;

		// Act
		var command = new Command();
		command.Done += () => actual = true;
		command.Execute();

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void BaseCommand_ExecuteAndCheckFail_ResultFalse()
	{
		// Arrange
		var actual = true;

		// Act
		var command = new Command();
		command.Execute();
		actual = command.IsFailed;

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void BaseCommand_ExecuteAndCheckFail_ResultTrue()
	{
		// Arrange
		var actual = false;

		// Act
		var commandFail = new CommandFail();
		commandFail.Execute();
		actual = commandFail.IsFailed;

		//Assert
		Assert.IsTrue(actual);
	}

	private class Command : BaseCommand
	{
		public override void Execute()
		{
			DoneInvoke();
		}
	}

	private class CommandFail : BaseCommand
	{
		public override void Execute()
		{
			Fail();
			DoneInvoke();
		}
	}
}
