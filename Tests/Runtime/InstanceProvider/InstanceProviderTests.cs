using NUnit.Framework;
using EM.Foundation;
using System;

internal sealed class InstanceProviderTests
{
	#region InstanceProvider

	[Test]
	public void InstanceProvider_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new InstanceProvider(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProvider_GetInstance()
	{
		// Arrange
		var expected = new Test();

		// Act
		var provider = new InstanceProvider(expected);
		var result = provider.GetInstance();
		var actual = result.Data;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	#endregion

	#region Nested

	private sealed class Test
	{
	}

	#endregion
}