using NUnit.Framework;
using EM.Foundation;
using System;

internal sealed class InstanceProviderTests
{
	#region InstanceProvider

	[Test]
	public void InstanceProvider_Constructor_Exeption()
	{
		// Arrange
		var actual = false;
		var providerParam = default(Test);

		// Act
		try
		{
			var provider = new InstanceProvider(providerParam);
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
		var actual = provider.GetInstance();

		//Assert
		Assert.AreEqual(expected, actual);
	}

	#endregion
	#region Nested

	internal sealed class Test
	{
	}

	#endregion
}