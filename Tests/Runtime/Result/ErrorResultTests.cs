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
		var result = new ErrorResult(null, null);

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
		var result = new ErrorResult(expected, null);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResult_Errors()
	{
		// Arrange
		var errors = new List<Error>()
		{
			new(null, null)
		};

		var expected = errors.AsReadOnly();
		var result = new ErrorResult(null, expected);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResult_Errors_NotNull()
	{
		// Arrange
		var result = new ErrorResult(null, null);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void ErrorResultGeneric_SuccessAndFailure()
	{
		// Arrange
		var result = new ErrorResult<Test>(null, null);

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
		var result = new ErrorResult<Test>(expected, null);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResultGeneric_Errors()
	{
		// Arrange
		var errors = new List<Error>()
		{
			new(null, null)
		};

		var expected = errors.AsReadOnly();
		var result = new ErrorResult<Test>(null, expected);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ErrorResultGeneric_Errors_NotNull()
	{
		// Arrange
		var result = new ErrorResult<Test>(null, null);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void ErrorResultGeneric_Data_Exception()
	{
		// Arrange
		var actual = false;
		var result = new ErrorResult<Test>(null, null);

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