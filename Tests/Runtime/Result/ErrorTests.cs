using EM.Foundation;
using NUnit.Framework;

public sealed class ErrorTests
{
	[Test]
	public void Error_Code()
	{
		// Arrange
		const string expected = "code";

		// Act
		var error = new Error(expected, null);
		var actual = error.Code;

		//Assert
		Assert.AreEqual(expected, actual);
	}
	
	[Test]
	public void Error_Details()
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