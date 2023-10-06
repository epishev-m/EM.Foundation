using System.Collections.Generic;
using EM.Foundation;
using NUnit.Framework;

public sealed class ValidationErrorResultTests
{
	[Test]
	public void ValidationErrorResult_SuccessAndFailure()
	{
		// Arrange
		var result = new ValidationErrorResult(null);

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
		var result = new ValidationErrorResult(expected);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}
}