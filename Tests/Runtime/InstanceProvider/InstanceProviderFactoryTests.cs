using System;
using EM.Foundation;
using NUnit.Framework;

public sealed class InstanceProviderFactoryTests
{
	[Test]
	public void InstanceProviderFactory_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new InstanceProviderFactory(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProviderFactory_GetInstance_InvalidOperationException()
	{
		// Arrange
		var actual = false;
		var instanceProvider = new TestNullInstanceProvider();

		// Act
		try
		{
			var factory = new InstanceProviderFactory(instanceProvider);
			var unused = factory.GetInstance();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProviderFactory_GetInstance_InvalidOperationException2()
	{
		// Arrange
		var actual = false;
		var instanceProvider = new TestInstanceProviderNotFactory();

		// Act
		try
		{
			var factory = new InstanceProviderFactory(instanceProvider);
			var unused = factory.GetInstance();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProviderFactory_GetInstance_InvalidOperationException3()
	{
		// Arrange
		var actual = false;
		var instanceProvider = new TestInstanceProviderFactoryNullInstance();

		// Act
		try
		{
			var factory = new InstanceProviderFactory(instanceProvider);
			var unused = factory.GetInstance();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void InstanceProviderFactory_GetInstance()
	{
		// Arrange
		var instanceProvider = new TestInstanceProviderFactoryNotNullInstance();

		// Act
		var factory = new InstanceProviderFactory(instanceProvider);
		var actual = factory.GetInstance();

		// Assert
		Assert.NotNull(actual);
	}

	#region Nested

	private sealed class TestNullInstanceProvider : IInstanceProvider
	{
		public object GetInstance()
		{
			return null;
		}
	}

	private sealed class TestInstanceProviderNotFactory : IInstanceProvider
	{
		public object GetInstance()
		{
			return new Test();
		}
	}

	private sealed class TestInstanceProviderFactoryNullInstance : IInstanceProvider
	{
		public object GetInstance()
		{
			return null;
		}
	}

	private sealed class TestInstanceProviderFactoryNotNullInstance : IInstanceProvider
	{
		public object GetInstance()
		{
			return new TestFactory();
		}
	}

	private sealed class TestFactory : IFactory
	{
		public bool TryCreate(out object instance)
		{
			instance = new Test();

			return true;
		}
	}

	private sealed class Test
	{
	}

	#endregion
}