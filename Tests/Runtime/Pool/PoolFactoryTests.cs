using System;
using EM.Foundation;
using NUnit.Framework;

public sealed class PoolFactoryTests
{
	[Test]
	public void PoolInstanceProvider_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new PoolFactory<TestObject>(default);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolInstanceProvider_GetObject_PoolIsEmptyResult()
	{
		// Arrange
		var instanceProvider = new TestFactory(false);

		// Act
		var pool = new PoolFactory<TestObject>(instanceProvider);
		var actual = pool.GetObject();

		// Assert
		Assert.IsInstanceOf<ErrorResult<TestObject>>(actual);
	}

	[Test]
	public void PoolInstanceProvider_GetObject_PoolIsEmptyResult_Exception()
	{
		// Arrange
		var actual = false;
		var instanceProvider = new TestFactory(default);
		var pool = new PoolFactory<TestObject>(instanceProvider);
		var result = pool.GetObject();

		// Act
		try
		{
			var unused = result.Data;
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolInstanceProvider_GetObject_SuccessResult()
	{
		// Arrange
		var instanceProvider = new TestFactory(true);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		var actual = pool.GetObject();

		//Assert
		Assert.IsInstanceOf<SuccessResult<TestObject>>(actual);
	}

	[Test]
	public void PoolInstanceProvider_GetObject()
	{
		// Arrange
		var expected = new TestObject();
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(expected);
		var result = pool.GetObject();
		var actual = result.Data;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void PoolAndInstanceProvider_GetObject_NotNull()
	{
		// Arrange
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		var actual = pool.GetObject();

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void PoolAndInstanceProvider_PutAnd2Get_2NotNull()
	{
		// Arrange
		var expected = new TestObject();
		var instanceProvider = new TestFactory(true);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(expected);
		var result1 = pool.GetObject();
		var actual1 = result1.Data;
		var result2 = pool.GetObject();
		var actual2 = result2.Data;

		//Assert
		Assert.AreEqual(expected, actual1);
		Assert.AreNotEqual(expected, actual2);
		Assert.IsNotNull(actual2);
	}

	[Test]
	public void PoolInstanceProvider_Count_NumberZero()
	{
		// Arrange
		var instanceProvider = new TestFactory(default);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(0, actual);
	}

	[Test]
	public void PoolInstanceProvider_Count_NumberOne()
	{
		// Arrange
		var testObject = new TestObject();
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(testObject);
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void PoolInstanceProvider_PutAndGetAndCount_NumberZero()
	{
		// Arrange
		var testObject = new TestObject();
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(testObject);
		var unused = pool.GetObject();
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(0, actual);
	}

	[Test]
	public void PoolInstanceProvider_PutObject_SuccessResult()
	{
		// Arrange
		var testObject = new TestObject();
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		var result = pool.PutObject(testObject);
		var actual = result.Success;

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolInstanceProvider_PutObject_ErrorResult()
	{
		// Arrange
		var instanceProvider = new TestFactory(false);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		var result = pool.PutObject(null);
		var actual = result.Failure;

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolInstanceProviderAndRestore_IsRestored_False()
	{
		// Arrange
		var testObject = new TestObject();
		var instanceProvider = new TestFactory(default);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(testObject);
		var result = pool.GetObject();

		if (result.Failure)
		{
			Assert.Fail();
		}

		var data = result.Data;
		data.Use();
		var obj = (IPoolable) data;
		var actual = obj.IsRestored;

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void PoolInstanceProviderAndRestore_IsRestored_True()
	{
		// Arrange
		var testObject = new TestObject();
		var instanceProvider = new TestFactory(default);
		var pool = new PoolFactory<TestObject>(instanceProvider);

		// Act
		pool.PutObject(testObject);
		var result = pool.GetObject();

		if (result.Failure)
		{
			Assert.Fail();
		}

		var data = result.Data;
		data.Use();
		var obj = (IPoolable) data;
		var actual = obj.IsRestored;

		Assert.IsFalse(actual);

		pool.PutObject(data);
		obj = testObject;
		actual = obj.IsRestored;

		//Assert
		Assert.IsTrue(actual);
	}

	#region Nested

	private sealed class TestObject :
		IPoolable
	{
		private bool _isRestored = true;

		bool IPoolable.IsRestored => _isRestored;

		public void Use()
		{
			_isRestored = false;
		}

		void IPoolable.Restore()
		{
			_isRestored = true;
		}
	}

	private sealed class TestFactory : IFactory<TestObject>
	{
		private readonly bool _isCreated;

		#region TestInstanceProvider

		public TestFactory(bool isCreated)
		{
			_isCreated = isCreated;
		}

		public Result<TestObject> Create()
		{
			if (!_isCreated)
			{
				return new ErrorResult<TestObject>(string.Empty);
			}

			var obj = Activator.CreateInstance(typeof(TestObject)) as TestObject;

			return new SuccessResult<TestObject>(obj);
		}

		#endregion
	}

	#endregion
}