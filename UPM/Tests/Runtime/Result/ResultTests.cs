using System;
using EM.Foundation;
using NUnit.Framework;

public sealed class ResultTests
{
	[Test]
	public void Result_SuccessAndFailure()
	{
		// Act
		var result = new ResultTest(true);
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsTrue(actualSuccess);
		Assert.IsFalse(actualFailure);
	}

	[Test]
	public void ResultGeneric_SuccessAndFailure()
	{
		// Act
		var result = new ResultGenericTest(null, true);
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsTrue(actualSuccess);
		Assert.IsFalse(actualFailure);
	}

	[Test]
	public void ResultGeneric_Data_Exception()
	{
		// Arrange
		var actual = false;
		var data = new Test();
		var result = new ResultGenericTest(data, false);

		// Act
		try
		{
			var unused = result.Data;
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ResultGeneric_Data()
	{
		// Arrange
		var expected = new Test();

		// Act
		var result = new ResultGenericTest(expected, true);
		var actual = result.Data;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	#region Nested

	private sealed class ResultTest : Result
	{
		public ResultTest(bool success)
		{
			Success = success;
		}
	}

	private sealed class ResultGenericTest : Result<Test>
	{
		public ResultGenericTest(Test data,
			bool success)
			: base(data)
		{
			Success = success;
		}
	}

	private sealed class Test
	{
	}

	#endregion
}