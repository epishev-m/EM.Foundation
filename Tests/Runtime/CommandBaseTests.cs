using EM.Foundation;
using NUnit.Framework;

internal sealed class CommandBaseTests
{
	[Test]
	public void CommandBase_ExecuteAndSubscribeEvent_ResultTrue()
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
	public void CommandBase_ExecuteAndCheckFail_ResultFalse()
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
	public void CommandBase_ExecuteAndCheckFail_ResultTrue()
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
	public void CommandBase_Clear_DoneEventEqualNull()
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
	public void CommandBase_Clear_DataEqualNull()
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

	private sealed class Command : CommandBase
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

	private sealed class CommandFail : CommandBase
	{
		public override void Execute()
		{
			Fail();
			DoneInvoke();
		}
	}
}
