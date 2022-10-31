using EM.Foundation;
using NUnit.Framework;

public sealed class SuccessResultTests
{
	[Test]
	public void SuccessResult_SuccessAndFailure()
	{
		// Arrange
		var result = new SuccessResult();

		// Act
		var success = result.Success;
		var failure = result.Failure;

		//Assert
		Assert.IsTrue(success);
		Assert.IsFalse(failure);
	}

	[Test]
	public void SuccessResultGeneric_SuccessAndFailure()
	{
		// Arrange
		var data = new Test();

		// Act
		var result = new SuccessResult<Test>(data);
		var success = result.Success;
		var failure = result.Failure;

		//Assert
		Assert.IsTrue(success);
		Assert.IsFalse(failure);
	}

	[Test]
	public void SuccessResultGeneric_Data()
	{
		// Arrange
		var expected = new Test();
		var successResult = new SuccessResult<Test>(expected);

		// Act
		var actualSuccess = successResult.Success;
		var actual = successResult.Data;

		//Assert
		Assert.IsTrue(actualSuccess);
		Assert.AreEqual(expected, actual);
	}

	#region Nested

	private sealed class Test
	{
	}

	#endregion
}