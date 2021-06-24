using EM.Foundation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

public sealed class CommandBatchTests
{
	[Test]
	public void CommandBatch_Execute_Success()
	{
		// Arrange
		Command.Counter = 0;
		var command1 = new Command(2);
		var command2 = new Command(1);
		var command3 = new Command(3);

		// Act
		var batch = new CommandBatch();
		batch.Add(command3)
			.Add(command2)
			.Add(command1)
			.Execute();

		var task = Task.Run(async () =>
		{
			while (batch.IsDone == false)
			{
				await Task.Delay(TimeSpan.FromMilliseconds(20));
			}

			return 1;
		});

		var unused = task.Result;

		//Assert
		Assert.AreEqual(1, command2.Count);
		Assert.AreEqual(2, command1.Count);
		Assert.AreEqual(3, command3.Count);
	}

	private sealed class Command :
		CommandBase
	{
		public static int Counter;

		private readonly int index;

		#region CommandBase

		public override void Execute()
		{
			var unused = Task.Run(async () =>
			{
				await Task.Delay(TimeSpan.FromMilliseconds(50 * index));
				Counter++;
				Count = Counter;
				DoneInvoke();
			});
		}

		#endregion
		#region Command

		public int Count
		{
			get;
			private set;
		}

		public Command(int index)
		{
			this.index = index;
		}

		#endregion
	}
}