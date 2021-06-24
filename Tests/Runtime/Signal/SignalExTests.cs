using EM.Foundation;
using NUnit.Framework;

internal sealed class SignalExTests
{
	[Test]
	public void SignalEx_AddListenerAndDispatch_Success()
	{
		// Arrange
		var actual = 0;

		// Act
		var signal = new SignalEx();
		signal.AddListener(ActionRuntime);
		signal.Dispatch();

		void ActionRuntime() => actual++;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void SignalEx_AddListenerOnceAndDispatch_Success()
	{
		// Arrange
		var actual = 0;

		// Act
		var signal = new SignalEx();
		signal.AddListenerOnce(ActionRuntime);
		signal.Dispatch();

		void ActionRuntime() => actual++;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void SignalEx_AddListenerAnd2Dispatch_Success()
	{
		// Arrange
		var actual = 0;

		// Act
		var signal = new SignalEx();
		signal.AddListener(ActionRuntime);
		signal.Dispatch();
		signal.Dispatch();

		void ActionRuntime() => actual++;

		//Assert
		Assert.AreEqual(2, actual);
	}

	[Test]
	public void SignalEx_AddListenerOnceAnd2Dispatch_Success()
	{
		// Arrange
		var actual = 0;

		// Act
		var signal = new SignalEx();
		signal.AddListenerOnce(ActionRuntime);
		signal.Dispatch();
		signal.Dispatch();

		void ActionRuntime() => actual++;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void SignalEx_RemoveAllListeners_Success()
	{
		// Arrange
		var actual = 0;

		// Act
		var signal = new SignalEx();
		signal.AddListener(ActionRuntime);
		signal.AddListenerOnce(ActionRuntime);
		signal.RemoveAllListeners();
		signal.Dispatch();

		void ActionRuntime() => actual++;

		//Assert
		Assert.AreEqual(0, actual);
	}
}