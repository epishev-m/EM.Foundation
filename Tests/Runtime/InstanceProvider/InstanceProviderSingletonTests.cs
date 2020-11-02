using NUnit.Framework;
using EM.Foundation;
using System;

internal sealed class InstanceProviderSingletonTests
{
	#region InstanceProviderSingleton

	[Test]
	public void InstanceProviderSingleton_Constructor_Exeption()
	{
		// Arrange
		var actual = false;
		var providerParam = default(IInstanceProvider);

		// Act
		try
		{
			var provider = new InstanceProviderSingleton(providerParam);
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

	internal sealed class Test
	{
	}

	internal sealed class TestProvider : IInstanceProvider
	{
		#region IProvider

		public object GetInstance()
		{
			return test;
		}

		#endregion
		#region TestProvider

		private readonly Test test;

		public TestProvider(Test test)
		{
			this.test = test ?? throw new ArgumentNullException(nameof(test));
		}

		#endregion
	}

	#endregion
}