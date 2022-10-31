using EM.Foundation;
using NUnit.Framework;

public sealed class ValidationErrorTests
{
	[Test]
	public void ValidationError_Code()
	{
		// Act
		var error = new Error(null, null);
		var actual = error.Code;

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void ValidationError_PropertyName()
	{
		// Arrange
		const string expected = "property";

		// Act
		var error = new ValidationError(expected, null);
		var actual = error.PropertyName;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ValidationError_Details()
	{
		// Arrange
		const string expected = "details";

		// Act
		var error = new Error(null, expected);
		var actual = error.Details;

		//Assert
		Assert.AreEqual(expected, actual);
	}
}