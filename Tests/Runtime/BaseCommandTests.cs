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

	[Test]
	public void BaseCommand_Clear_DoneEventEqualNull()
	{
		// Arrange
		var actual = true;

		// Act
		var command = new Command();
		command.Done += () => actual = false;
		command.Clear();
		command.Execute();

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void BaseCommand_Clear_DataEqualNull()
	{
		// Arrange
		var data = typeof(string);

		// Act
		var command = new Command
		{
			Data = data
		};
		command.Clear();
		var actual = command.GetData();

		//Assert
		Assert.IsNull(actual);
	}

	private sealed class Command : BaseCommand
	{
		public override void Execute()
		{
			DoneInvoke();
		}

		public object GetData()
		{
			return Data;
		}
	}

	private sealed class CommandFail : BaseCommand
	{
		public override void Execute()
		{
			Fail();
			DoneInvoke();
		}
	}
}
