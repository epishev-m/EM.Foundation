using EM.Foundation;
using NUnit.Framework;
using System;

internal sealed class BaseCompositeCommandTests
{
	[Test]
	public void BaseCompositeCommand_Add_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		var compositeCommand = new CompositeCommand();

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
	public void BaseCompositeCommand_Add_ReturnCompositeCommand()
	{
		// Arrange
		var command = new Command();

		// Act
		var compositeCommand = new CompositeCommand();
		var actual = compositeCommand.Add(command);

		//Assert
		Assert.AreEqual(actual, compositeCommand);
	}

	private sealed class Command : BaseCommand
	{
		public override void Execute()
		{
			DoneInvoke();
		}
	}

	private sealed class CompositeCommand : BaseCompositeCommand
	{
		public override void Execute()
		{
		}
	}
}
