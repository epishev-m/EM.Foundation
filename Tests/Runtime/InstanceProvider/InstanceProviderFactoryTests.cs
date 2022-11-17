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
	public void InstanceProviderFactory_GetInstance_InstanceProviderErrorResult()
	{
		// Arrange
		var instanceProvider = new TestNullInstanceProvider();
		var instanceProviderFactory = new InstanceProviderFactory(instanceProvider);

		// Act
		var actual = instanceProviderFactory.GetInstance();

		// Assert
		Assert.IsAssignableFrom<ErrorResult<object>>(actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void InstanceProviderFactory_GetInstance()
	{
		// Arrange
		var instanceProvider = new TestInstanceProviderFactoryNotNullInstance();

		// Act
		var factory = new InstanceProviderFactory(instanceProvider);
		var result = factory.GetInstance();
		var actual = result.Data;

		// Assert
		Assert.NotNull(actual);
	}

	#region Nested

	private sealed class TestNullInstanceProvider : IInstanceProvider
	{
		public Result<object> GetInstance()
		{
			return new ErrorResult<object>(string.Empty);
		}
	}

	private sealed class TestInstanceProviderFactoryNotNullInstance : IInstanceProvider
	{
		public Result<object> GetInstance()
		{
			var factory = new TestFactory();

			return new SuccessResult<object>(factory);
		}
	}

	private sealed class TestFactory : IFactory
	{
		public Result<object> Create()
		{
			return new SuccessResult<object>(new Test());
		}
	}

	private sealed class Test
	{
	}

	#endregion
}