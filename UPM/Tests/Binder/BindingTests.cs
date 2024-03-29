﻿using EM.Foundation;
using NUnit.Framework;
using System;
using System.Linq;

internal sealed class BindingTests
{
	#region Common

	[Test]
	public void Binding_Constructor_Exception()
	{
		// Act
		var actual = false;

		try
		{
			var unused = new Binding(null, null, null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binding_GetKey_ReturnKey()
	{
		// Arrange
		const string key = "key";

		// Act
		var binding = new Binding(key, null, null);
		var actual = binding.Key;

		//Assert
		Assert.AreEqual(key, actual);
	}

	[Test]
	public void Binding_GetName_ReturnName()
	{
		// Arrange
		const string name = "name";

		// Act
		var binding = new Binding("key", name, null);
		var actual = binding.Name;

		//Assert
		Assert.AreEqual(name, actual);
	}

	[Test]
	public void Binding_GetName_ReturnNull()
	{
		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.Name;

		//Assert
		Assert.AreEqual(null, actual);
	}

	[Test]
	public void Binding_GetValues_ReturnNull()
	{
		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.Values;

		//Assert
		Assert.AreEqual(null, actual);
	}

	#endregion

	#region To

	[Test]
	public void Binding_To_ReturnBinding()
	{
		// Arrange
		var value = typeof(string);
		var binding = new Binding("key", null, null);

		// Act
		var actual = binding.To(value);

		//Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void Binding_ToAndGetValues_ReturnKey()
	{
		// Arrange
		var key = typeof(string);
		var value = typeof(string);

		// Act
		var binding = new Binding(key, null, null);
		var valuesArray = binding.To(value).Values.ToArray();
		var actual = valuesArray.ToArray()[0];

		//Assert
		Assert.AreEqual(key, actual);
	}

	[Test]
	public void Binding_ToAndGetValuesLength_ReturnArray()
	{
		// Arrange
		var key = typeof(string);
		var value = typeof(string);

		// Act
		var binding1 = new Binding(key, null, null);
		var actual1 = binding1.To(value).Values.Count();

		var binding2 = new Binding(key, null, null);
		var actual2 = binding2.To(value).To(value).Values.Count();

		//Assert
		Assert.AreEqual(1, actual1);
		Assert.AreEqual(2, actual2);
	}

	[Test]
	public void Binding_To_RunResolver()
	{
		// Arrange
		var key = typeof(string);
		var value = typeof(string);
		var actual = false;

		// Act
		void Resolver(IBinding bind) => actual = true;

		var binding = new Binding(key, null, Resolver);
		var unused = binding.To(value);

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region ToGeneric

	[Test]
	public void Binding_ToGeneric_ReturnBinding()
	{
		// Arrange
		var binding = new Binding("key", null, null);

		// Act
		var actual = binding.To<string>();

		//Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void Binding_ToGenericAndGetValues_ReturnKey()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binding = new Binding(key, null, null);
		var valuesArray = binding.To<string>().Values.ToArray();
		var actual = valuesArray[0];

		//Assert
		Assert.AreEqual(key, actual);
	}

	[Test]
	public void Binding_ToGenericAndGetValuesLength_ReturnArray()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binding1 = new Binding(key, null, null);
		var actual1 = binding1.To<string>().Values.Count();

		var binding2 = new Binding(key, null, null);
		var actual2 = binding2.To<string>().To<string>().Values.Count();

		//Assert
		Assert.AreEqual(1, actual1);
		Assert.AreEqual(2, actual2);
	}

	[Test]
	public void Binding_ToGeneric_RunResolver()
	{
		// Arrange
		var key = typeof(string);
		var actual = false;

		// Act
		void Resolver(IBinding bind) => actual = true;

		var binding = new Binding(key, null, Resolver);
		var unused = binding.To<string>();

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region ToSelf

	[Test]
	public void Binding_ToSelf_ReturnBinding()
	{
		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.ToSelf();

		//Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void Binding_ToSelfAndGetValues_ReturnKey()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binding = new Binding(key, null, null);
		var valuesArray = binding.ToSelf().Values.ToArray();
		var actual = valuesArray[0];

		//Assert
		Assert.AreEqual(key, actual);
	}

	[Test]
	public void Binding_ToSelfAndGetValuesLength_ReturnArray()
	{
		// Arrange
		var key = typeof(string);

		// Act
		var binding1 = new Binding(key, null, null);
		var actual1 = binding1.ToSelf().Values.Count();

		var binding2 = new Binding(key, null, null);
		var actual2 = binding2.ToSelf().ToSelf().Values.Count();

		//Assert
		Assert.AreEqual(1, actual1);
		Assert.AreEqual(2, actual2);
	}

	[Test]
	public void Binding_ToSelf_RunResolver()
	{
		// Arrange
		var key = typeof(string);
		var actual = false;

		// Act
		void Resolver(IBinding bind) => actual = true;

		var binding = new Binding(key, null, Resolver);
		var unused = binding.ToSelf();

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region ToName

	[Test]
	public void Binding_ToName_Exception()
	{
		// Arrange
		var binding = new Binding("key", null, null);

		// Act
		var actual = false;

		try
		{
			var unused = binding.ToName(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binding_ToName_ReturnBinding()
	{
		// Arrange
		var binding = new Binding("key", null, null);

		// Act
		var actual = binding.ToName("name");

		//Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void Binding_ToNameAndGetName_ReturnName()
	{
		// Arrange
		const string name = "name";
		var binding = new Binding("key", null, null);

		// Act
		var actual = binding.ToName(name).Name;

		//Assert
		Assert.AreEqual(name, actual);
	}

	[Test]
	public void Binding_ToName_RunResolver()
	{
		// Arrange
		var key = typeof(string);
		var name = typeof(string);
		var actual = false;

		// Act
		void Resolver(IBinding bind) => actual = true;

		var binding = new Binding(key, null, Resolver);
		var unused = binding.ToName(name);

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region ToNameGeneric

	[Test]
	public void Binding_ToNameGeneric_ReturnBinding()
	{
		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.ToName<string>();

		//Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void Binding_ToNameGenericAndGetName_ReturnName()
	{
		// Arrange
		var name = typeof(string);

		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.ToName<string>().Name;

		//Assert
		Assert.AreEqual(name, actual);
	}

	[Test]
	public void Binding_ToNameGeneric_RunResolver()
	{
		// Arrange
		var key = typeof(string);
		var actual = false;

		// Act
		void Resolver(IBinding bind) => actual = true;

		var binding = new Binding(key, null, Resolver);
		var unused = binding.ToName<string>();

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region Remove

	[Test]
	public void Binding_RemoveValue_ReturnTrue()
	{
		// Arrange
		var value = typeof(string);

		// Act
		var binding = new Binding("key", null, null);
		var unused = binding.To(value);
		var actual = binding.RemoveValue(value);

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Binding_RemoveValue_ReturnFalse()
	{
		// Arrange
		var value = typeof(string);

		// Act
		var binding = new Binding("key", null, null);
		var actual = binding.RemoveValue(value);

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void Binding_RemoveValueAndGetValues_ReturnNull()
	{
		// Arrange
		var value = typeof(string);

		// Act
		var binding = new Binding("key", null, null);
		var unused = binding.To(value).ToSelf().To<string>();
		binding.RemoveAllValues();
		var actual = binding.Values;

		//Assert
		Assert.IsNull(actual);
	}

	#endregion
}