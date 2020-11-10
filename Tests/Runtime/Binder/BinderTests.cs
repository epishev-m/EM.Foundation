using EM.Foundation;
using NUnit.Framework;
using System;

internal sealed class BinderTests
{
	#region Bind

	[Test]
	public void Binder_Bind_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var binder = new Binder();
			var unused = binder.Bind(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binder_Bind_ReturnBinding()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var actual = binder.Bind(key);

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_BindGeneric_ReturnBinding()
	{
		// Act
		var binder = new Binder();
		var actual = binder.Bind<string>();

		//Assert
		Assert.IsNotNull(actual);
	}

	#endregion
	#region Unbind

	[Test]
	public void Binder_Unbind_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var binder = new Binder();
			var unused = binder.Unbind(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binder_Unbind_ReturnFalse()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var actual = binder.Unbind(key);

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void Binder_Unbind_ReturnTrue()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.Unbind(key);

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binder_UnbindGeneric_ReturnFalse()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var actual = binder.Unbind<string>();

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void Binder_UnbindGeneric_ReturnTrue()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.Unbind<string>();

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion
}
