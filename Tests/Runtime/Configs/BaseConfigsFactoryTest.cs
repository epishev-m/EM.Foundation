using System;
using EM.Foundation;
using NUnit.Framework;

public sealed class BaseConfigsFactoryTest
{
	[Test]
	public void BaseConfigsFactory_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new TestConfigsFactory(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	#region Nested
	
	private sealed class TestConfigsFactory : BaseConfigsFactory
	{
		#region BaseConfigsFactory

		protected override string Key => "test";

		#endregion

		#region TestConfigsFactory
		
		public TestConfigsFactory(IAssetsManager assetsManager)
			: base(assetsManager)
		{
		}

		#endregion
	}

	#endregion
}