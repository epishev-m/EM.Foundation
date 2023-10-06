using NUnit.Framework;
using EM.Foundation;
using System;

internal sealed class InstanceProviderSingletonTests
{
	#region InstanceProviderSingleton

	[Test]
	public void InstanceProviderSingleton_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new InstanceProviderSingleton(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProviderSingleton_GetInstance()
	{
		// Arrange
		var testProvider = new TestProvider();

		// Act
		var provider = new InstanceProviderSingleton(testProvider);
		var result1 = provider.GetInstance();
		var actual1 = result1.Data;
		var result2 = provider.GetInstance();
		var actual2 = result2.Data;

		//Assert
		Assert.AreEqual(actual1, actual2);
	}

	#endregion

	#region Nested

	private sealed class Test
	{
	}

	private sealed class TestProvider : IInstanceProvider
	{
		#region IProvider

		public Result<object> GetInstance()
		{
			return new SuccessResult<object>(new Test());
		}

		#endregion
	}

	#endregion
}