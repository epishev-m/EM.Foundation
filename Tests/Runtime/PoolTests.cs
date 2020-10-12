using EM.Foundation;
using NUnit.Framework;
using System;

public sealed class PoolTests
{
	#region Pool

	[Test]
	public void Pool_GetObject_Null()
	{
		// Arrange
		var expected = default(TestObject);

		// Act
		var pool = new Pool<TestObject>();
		var actual = pool.GetObject();

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_GetObject_NotNull()
	{
		// Arrange
		var expected = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(expected);
		var actual = pool.GetObject();

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_Count_NumberZero()
	{
		// Arrange
		var expected = 0;

		// Act
		var pool = new Pool<TestObject>();
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_Count_NumberOne()
	{
		// Arrange
		var expected = 1;
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_PutAndGetAndCount_NumberZero()
	{
		// Arrange
		var expected = 0;
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var unused = pool.GetObject();
		var actual = pool.Count;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Pool_PutObject_Exception()
	{
		// Arrange
		var actual = false;
		var testObject = default(TestObject);

		// Act
		var pool = new Pool<TestObject>();

		try
		{
			pool.PutObject(testObject);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void PoolAndPoolable_IsRestored_False()
	{
		// Arrange
		var expected = false;
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var tempObject = pool.GetObject();
		tempObject.Use();
		var poolable = tempObject as IPoolable;
		var actual = poolable.IsRestored;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void PoolAndPoolable_IsRestored_True()
	{
		// Arrange
		var expected = true;
		var testObject = new TestObject();

		// Act
		var pool = new Pool<TestObject>();
		pool.PutObject(testObject);
		var tempObject = pool.GetObject();
		tempObject.Use();
		var poolable = tempObject as IPoolable;
		var actual = poolable.IsRestored;

		Assert.IsFalse(actual);

		pool.PutObject(tempObject);
		poolable = testObject as IPoolable;
		actual = poolable.IsRestored;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	#endregion
	#region PoolAndInstanceProvider

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
			var instance = pool.GetObject();
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
		var instanceProvider = default(TestInstanceProvider);

		// Act
		try
		{
			var pool = new Pool<TestObject>(instanceProvider);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion
	#region Nested

	private sealed class TestObject : IPoolable
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

	private sealed class TestInstanceProvider : IInstanceProvider
	{
		public object GetInstance()
		{
			return Activator.CreateInstance(typeof(TestObject));
		}
	}

	private sealed class FailInstanceProvider : IInstanceProvider
	{
		public object GetInstance()
		{
			return Activator.CreateInstance(typeof(string));
		}
	}

	#endregion
}