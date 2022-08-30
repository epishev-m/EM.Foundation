using System;
using EM.Foundation;
using NUnit.Framework;

public sealed class ConfigLinkTests
{
	[Test]
	public void ConfigLink_Constructor_ExceptionType()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new ConfigLinkTest(null, "test");
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}
	
	[Test]
	public void ConfigLink_Constructor_ExceptionName()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new ConfigLinkTest(typeof(Test), null);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}
	
	[Test]
	public void ConfigLinkT_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new ConfigLink<Test>(null);
		}
		catch (ArgumentException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}
	
	[Test]
	public void ConfigLinkT_Constructor_TypeAndName()
	{
		const string expectedName = "test";

		// Act
		var configLink = new ConfigLink<Test>(expectedName);
		var actualType = configLink.Type;
		var actualName = configLink.Name;

		// Assert
		Assert.AreEqual(typeof(Test), actualType);
		Assert.AreEqual(expectedName, actualName);
	}

	#region Nested

	private sealed class ConfigLinkTest : ConfigLink
	{
		public ConfigLinkTest(Type entryType,
			string name)
			: base(entryType, name)
		{
		}
	}
	
	private sealed class Test
	{
	}

	#endregion
}
