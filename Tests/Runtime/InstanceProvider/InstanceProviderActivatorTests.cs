using NUnit.Framework;
using EM.Foundation;
using System;

internal sealed class InstanceProviderActivatorTests
{
	#region InstanceProviderActivator

	[Test]
	public void InstanceProviderActivator_GetInstance()
	{
		// Arrange
		var provider = new InstanceProviderActivator<Test>();

		// Act
		var actual = provider.GetInstance();

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void InstanceProviderActivator_InstanceGetType()
	{
		// Arrange
		var expected = typeof(Test);

		// Act
		var provider = new InstanceProviderActivator<Test>();
		var instans = provider.GetInstance();
		var actual = instans.GetType();

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