using EM.Foundation;
using NUnit.Framework;
using System;

public sealed class PoolTests
{
	[Test]
	public void Pool_GetObject_PoolIsEmptyResult()
	{
		// Act
		var pool = new Pool<TestObject>();
		var actual = pool.GetObject();

		// Assert
		Assert.IsInstanceOf<ErrorResult<TestObject>>(actual);
	}

	[Test]
	public void Pool_GetObject_PoolIsEmptyResult_Exception()
	{
		// Arrange
		var actual = false;
		var pool = new Pool<TestObject>();
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
	public void Pool_GetObject_SuccessResult()
	{
		// Arrange
		var expected = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(expected);
		var actual = pool.GetObject();

		//Assert
		Assert.IsInstanceOf<SuccessResult<TestObject>>(actual);
	}

	[Test]
	public void Pool_GetObject_SuccessResult_NotNull()
	{
		// Arrange
		var expected = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(expected);
		var result = pool.GetObject();
		var actual = result.Data;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_Count_NumberZero()
	{
		// Arrange
		var pool = new Pool<TestObject>();

		// Act
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(0, actual);
	}

	[Test]
	public void Pool_Count_NumberOne()
	{
		// Arrange
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void Pool_PutAndGetAndCount_NumberZero()
	{
		// Arrange
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var unused = pool.GetObject();
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(0, actual);
	}

	[Test]
	public void Pool_PutObject_SuccessResult()
	{
		// Arrange
		var obj = new TestObject();
		var pool = new Pool<TestObject>();

		// Act
		var result = pool.PutObject(obj);
		var actual = result.Success;

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Pool_PutObject_ErrorResult()
	{
		// Arrange
		var pool = new Pool<TestObject>();

		// Act
		var result = pool.PutObject(null);
		var actual = result.Failure;

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolAndRestore_IsRestored_False()
	{
		// Arrange
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
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
	public void PoolAndRestore_IsRestored_True()
	{
		// Arrange
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
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

	/*#region PoolAndInstanceProvider

	[Test]
	public void PoolAndInstanceProvider_GetObject_NotNull()
	{
		// Arrange
		var instanceProvider = new TestInstanceProvider();

		// Act
		var pool = new Pool<TestObject>(instanceProvider);
		var actual = pool.GetObject();

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void PoolAndInstanceProvider_GetObject_Exception()
	{
		// Arrange
		var instanceProvider = new FailInstanceProvider();
		var pool = new Pool<TestObject>(instanceProvider);
		var actual = false;

		// Act
		try
		{
			var unused = pool.GetObject();
		}
		catch (Exception)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolAndInstanceProvider_PutAnd2Get_2NotNull()
	{
		// Arrange
		var instanceProvider = new TestInstanceProvider();
		var expected = new TestObject();

		// Act
		var pool = new Pool<TestObject>(instanceProvider);
		pool.PutObject(expected);
		var actual1 = pool.GetObject();
		var actual2 = pool.GetObject();

		//Assert
		Assert.AreEqual(expected, actual1);
		Assert.AreNotEqual(expected, actual2);
		Assert.IsNotNull(actual2);
	}

	[Test]
	public void PoolAndInstanceProvider_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new Pool<TestObject>(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion*/

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

	/*private sealed class TestInstanceProvider : IInstanceProvider<TestObject>
	{
		public TestObject GetInstance()
		{
			return Activator.CreateInstance(typeof(TestObject)) as TestObject;
		}
	}

	private sealed class FailInstanceProvider : IInstanceProvider
	{
		public object GetInstance()
		{
			return Activator.CreateInstance(typeof(string));
		}
	}*/

	#endregion
}