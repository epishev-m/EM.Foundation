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
		var expected = new Test();
		var testProvider = new TestProvider(expected);

		// Act
		var provider = new InstanceProviderSingleton(testProvider);
		var actual1 = provider.GetInstance();
		var actual2 = provider.GetInstance();

		//Assert
		Assert.AreEqual(expected, actual1);
		Assert.AreEqual(expected, actual2);
	}

	#endregion

	#region Nested

	private sealed class Test
	{
	}

	private sealed class TestProvider :
		IInstanceProvider
	{
		private readonly Test test;

		#region IProvider

		public object GetInstance()
		{
			return test;
		}

		#endregion

		#region TestProvider

		public TestProvider(
			Test test)
		{
			this.test = test ?? throw new ArgumentNullException(nameof(test));
		}

		#endregion
	}

	#endregion
}