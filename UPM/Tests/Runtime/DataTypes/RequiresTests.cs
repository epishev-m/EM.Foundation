using System;
using EM.Foundation;
using NUnit.Framework;
using UnityEngine;

public sealed class RequiresTests
{
	[Test]
	public void Requires_ValidOperation()
	{
		// Arrange
		var instance = new Test();
		var actual = false;

		// Act
		try
		{
			Requires.ValidOperation(false, instance);
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_ValidOperation2()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			Requires.ValidOperation(false, string.Empty);
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_ValidArgument()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			Requires.ValidArgument(false, string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_NotNull_SystemObject()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			Requires.NotNullParam(default(object), string.Empty);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_NotNull_UnityEngineObject()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			Requires.NotNullParam(default, string.Empty);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_Null_SystemObject()
	{
		// Arrange
		var instance = new Test();
		var actual = false;

		// Act
		try
		{
			Requires.NullParam(instance, string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_Null_UnityEngineObject()
	{
		// Arrange
		var instance = new GameObject();
		var actual = false;

		// Act
		try
		{
			Requires.NullParam(instance, string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_ReferenceType()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			Requires.ReferenceType(typeof(TestStruct), string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_Type_SystemObject()
	{
		// Arrange
		var instance = new Test();
		var actual = false;

		// Act
		try
		{
			Requires.Type<TestStruct>(instance, string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Requires_Type_UnityEngineObject()
	{
		// Arrange
		var instance = new GameObject();
		var actual = false;

		// Act
		try
		{
			Requires.Type<TestStruct>(instance, string.Empty);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	#region Nested

	private sealed class Test
	{
	}

	private struct TestStruct
	{
	}

	#endregion
}