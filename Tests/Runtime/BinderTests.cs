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
			binder.Bind(null);
		}
		catch (Exception e)
		{

			actual = e is ArgumentNullException;
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
	#region GetBinding

	[Test]
	public void Binder_GetBinding_Exception()
	{
		// Act
		var actual = false;

		try
		{
			var binder = new Binder();
			binder.GetBinding(null);
		}
		catch (Exception e)
		{

			actual = e is ArgumentNullException;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binder_GetBinding_ReturnNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var actual = binder.GetBinding(key);

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterBind_ReturnNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key);
		var actual = binder.GetBinding(key);

		//Assert
		Assert.AreNotEqual(binding, actual);
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterTo_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var value = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).To(value);
		var actual = binder.GetBinding(key);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterToGeneric_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).To<string>();
		var actual = binder.GetBinding(key);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterToSelf_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding(key);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterToName_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName(name);
		var actual = binder.GetBinding(key, name);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterToNameGeneric_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName<string>();
		var actual = binder.GetBinding(key, name);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingAfterToName_ReturnNewBinding()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding(key);
		var newBinding = binding.ToName(name);
		var newActual = binder.GetBinding(key, name);
		var oldActual = binder.GetBinding(key);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
		Assert.AreEqual(binding, newBinding);
		Assert.IsNotNull(newActual);
		Assert.IsNull(oldActual);
	}
	
	[Test]
	public void Binder_GetBindingAfterDoubleBind_ReturnTheSameBinding()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding(key);

		var newBinding = binder.Bind(key).ToName(name);
		var newActual = binder.GetBinding(key, name);

		//Assert
		Assert.AreEqual(actual, newActual);
	}

	#endregion
	#region GetBindingGeneric

	[Test]
	public void Binder_GetBindingGeneric_ReturnNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var actual = binder.GetBinding<string>();

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterBind_ReturnNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key);
		var actual = binder.GetBinding<string>();

		//Assert
		Assert.AreNotEqual(binding, actual);
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterTo_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var value = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).To(value);
		var actual = binder.GetBinding<string>();

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterToGeneric_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).To<string>();
		var actual = binder.GetBinding<string>();

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterToSelf_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding<string>();

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterToName_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName(name);
		var actual = binder.GetBinding<string>(name);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterToNameGeneric_ReturnNotNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName<string>();
		var actual = binder.GetBinding<string>(name);

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterToName_ReturnNewBinding()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding<string>();
		var newBinding = binding.ToName(name);
		var newActual = binder.GetBinding<string>(name);
		var oldActual = binder.GetBinding<string>();

		//Assert
		Assert.AreEqual(binding, actual);
		Assert.IsNotNull(actual);
		Assert.AreEqual(binding, newBinding);
		Assert.IsNotNull(newActual);
		Assert.IsNull(oldActual);
	}

	[Test]
	public void Binder_GetBindingGenericAfterDoubleBind_ReturnTheSameBinding()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		var actual = binder.GetBinding<string>();

		var newBinding = binder.Bind(key).ToName(name);
		var newActual = binder.GetBinding<string>(name);

		//Assert
		Assert.AreEqual(actual, newActual);
	}

	#endregion
	#region Unbind

	[Test]
	public void Binder_Unbind_Exception()
	{
		// Act
		var actual = false;

		try
		{
			var binder = new Binder();
			binder.Unbind(null);
		}
		catch (Exception e)
		{

			actual = e is ArgumentNullException;
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

	[Test]
	public void Binder_UnbindAndGetBinding_ReturnNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		binder.Unbind(key);
		var actual = binder.GetBinding(key);

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_UnbindAndGetBindingAfterToName_ReturnNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName(name);
		binder.Unbind(key, name);
		var actual = binder.GetBinding(key, name);

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_UnbindGenericAndGetBinding_ReturnNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToSelf();
		binder.Unbind<string>();
		var actual = binder.GetBinding(key);

		//Assert
		Assert.IsNull(actual);
	}

	[Test]
	public void Binder_UnbindGenericAndGetBindingAfterToName_ReturnNull()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);

		// Act
		var binder = new Binder();
		var binding = binder.Bind(key).ToName(name);
		binder.Unbind<string>(name);
		var actual = binder.GetBinding(key, name);

		//Assert
		Assert.IsNull(actual);
	}

	#endregion
}
