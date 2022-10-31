using System.Collections.Generic;
using EM.Foundation;
using NUnit.Framework;

public sealed class ValidationErrorResultTests
{
	[Test]
	public void ValidationErrorResult_SuccessAndFailure()
	{
		// Arrange
		var result = new ValidationErrorResult(null, null);

		// Act
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsFalse(actualSuccess);
		Assert.IsTrue(actualFailure);
	}

	[Test]
	public void ValidationErrorResult_Message()
	{
		// Arrange
		const string expected = "message";
		var result = new ValidationErrorResult(expected, null);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ValidationErrorResult_Errors()
	{
		// Arrange
		var errors = new List<ValidationError>()
		{
			new(null, null)
		};

		var expected = errors.AsReadOnly();
		var result = new ValidationErrorResult(null, expected);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ValidationErrorResult_Errors_NotNull()
	{
		// Arrange
		var result = new ValidationErrorResult(null, null);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.IsNotNull(actual);
	}
}