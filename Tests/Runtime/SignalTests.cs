using EM.Foundation;
using NUnit.Framework;

public sealed class SignalTests
{
	[Test]
	public void Signal_AddListenerAndDispatch_Success()
	{
		// Arrange
		var expectedArgs = new object[1];
		var expectedSignal = new Signal();
		var actualTarget = default(ISignal);
		var actualArgs = default(object[]);

		// Act
		expectedSignal.AddListener(ActionRuntime);
		expectedSignal.Dispatch(expectedArgs);

		void ActionRuntime(ISignal target, object[] args)
		{
			actualTarget = target;
			actualArgs = args;
		}

		//Assert
		Assert.AreEqual(expectedArgs, actualArgs);
		Assert.AreEqual(expectedSignal, actualTarget);
	}

	[Test]
	public void Signal_AddListenerOnceAndDispatch_Success()
	{
		// Arrange
		var expectedArgs = new object[1];
		var expectedSignal = new Signal();
		var actualTarget = default(ISignal);
		var actualArgs = default(object[]);

		// Act
		expectedSignal.AddListenerOnce(ActionRuntime);
		expectedSignal.Dispatch(expectedArgs);

		void ActionRuntime(ISignal target, object[] args)
		{
			actualTarget = target;
			actualArgs = args;
		}

		//Assert
		Assert.AreEqual(expectedArgs, actualArgs);
		Assert.AreEqual(expectedSignal, actualTarget);
	}

	[Test]
	public void Signal_AddListenerAnd2Dispatch_Success()
	{
		// Arrange
		var argsArray = new object[1];
		var expected = 2;
		var actual = 0;

		// Act
		var signal = new Signal();
		signal.AddListener(ActionRuntime);
		signal.Dispatch(argsArray);
		signal.Dispatch(argsArray);

		void ActionRuntime(ISignal target, object[] args) => actual++;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Signal_AddListenerOnceAnd2Dispatch_Success()
	{
		// Arrange
		var argsArray = new object[1];
		var expected = 1;
		var actual = 0;

		// Act
		var signal = new Signal();
		signal.AddListenerOnce(ActionRuntime);
		signal.Dispatch(argsArray);
		signal.Dispatch(argsArray);

		void ActionRuntime(ISignal target, object[] args) => actual++;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Signal_RemoveAllListeners_Success()
	{
		// Arrange
		var argsArray = new object[1];
		var expected = 0;
		var actual = 0;

		// Act
		var signal = new Signal();
		signal.AddListener(ActionRuntime);
		signal.AddListenerOnce(ActionRuntime);
		signal.RemoveAllListeners();
		signal.Dispatch(argsArray);

		void ActionRuntime(ISignal target, object[] args) => actual++;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Signal_RemoveListener_Success()
	{
		// Arrange
		var argsArray = new object[1];
		var expected = 2;
		var actual = 0;

		// Act
		var signal = new Signal();
		signal.AddListener(ActionRuntime);
		signal.AddListener(ActionRuntime);
		signal.AddListenerOnce(ActionRuntimeOne);
		signal.AddListenerOnce(ActionRuntimeOne);
		signal.RemoveListener(ActionRuntime);
		signal.RemoveListener(ActionRuntimeOne);
		signal.Dispatch(argsArray);

		void ActionRuntime(ISignal target, object[] args) => actual++;
		void ActionRuntimeOne(ISignal target, object[] args) => actual++;

		//Assert
		Assert.AreEqual(expected, actual);
	}
}