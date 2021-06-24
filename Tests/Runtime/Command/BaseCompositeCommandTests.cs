using EM.Foundation;
using NUnit.Framework;
using System;

internal sealed class CommandCompositeBaseTests
{
	[Test]
	public void CommandCompositeBase_Add_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		var compositeCommand = new CommandComposite();

		try
		{
			compositeCommand.Add(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void CommandCompositeBase_Add_ReturnThis()
	{
		// Arrange
		var command = new Command();

		// Act
		var commandComposite = new CommandComposite();
		var actual = commandComposite.Add(command);

		//Assert
		Assert.AreEqual(actual, commandComposite);
	}

	private sealed class Command :
		CommandBase
	{
		public override void Execute()
		{
			DoneInvoke();
		}
	}

	private sealed class CommandComposite :
		CommandCompositeBase
	{
		public override void Execute()
		{
		}
	}
}
