using EM.Foundation;
using NUnit.Framework;

internal sealed class UnionAttributeTests
{
	[Test]
	public void UnionAttribute_Constructor_Type()
	{
		var expected = typeof(Test);

		// Act
		var configLink = new UnionAttribute(expected);
		var actual = configLink.Type;

		// Assert
		Assert.AreEqual(expected, actual);
	}

	#region Nested

	private sealed class Test
	{
	}

	#endregion
}