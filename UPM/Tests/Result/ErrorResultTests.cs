using System;
using System.Collections.Generic;
using EM.Foundation;
using NUnit.Framework;

public sealed class ErrorResultTests
{
	[Test]
	public void ErrorResult_SuccessAndFailure()
	{
		// Arrange
		var result = new ErrorResult(null);

		// Act
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsFalse(actualSuccess);
		Assert.IsTrue(actualFailure);
	}

	[Test]
	public void ErrorResult_Message()
	{
		// Arrange
		const string expected = "message";
		var result = new ErrorResult(expected);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResultGeneric_SuccessAndFailure()
	{
		// Arrange
		var result = new ErrorResult<Test>(null);

		// Act
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsFalse(actualSuccess);
		Assert.IsTrue(actualFailure);
	}

	[Test]
	public void ErrorResultGeneric_Message()
	{
		// Arrange
		const string expected = "message";
		var result = new ErrorResult<Test>(expected);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResultGeneric_Data_Exception()
	{
		// Arrange
		var actual = false;
		var result = new ErrorResult<Test>(null);

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

	#region Nested

	// ReSharper disable once ClassNeverInstantiated.Local
	private sealed class Test
	{
	}

	#endregion
}