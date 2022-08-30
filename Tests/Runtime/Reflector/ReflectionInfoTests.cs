using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EM.Foundation;

internal sealed class ReflectionInfoTests
{
	[Test]
	public void ReflectionInfo_ConstructorParam1_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new ReflectionInfo(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ReflectionInfo_ConstructorInfo_ExceptionManyConstructors()
	{
		// Arrange
		var actual = false;
		var type = typeof(TestManyConstructors);

		// Act
		try
		{
			var reflectionInfo = new ReflectionInfo(type);
			var unused = reflectionInfo.ConstructorInfo;
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ReflectionInfo_ConstructorInfo()
	{
		// Act
		var reflectionInfo = new ReflectionInfo(typeof(Test));
		var actual = reflectionInfo.ConstructorInfo;

		// Assert
		Assert.NotNull(actual);
	}

	[Test]
	public void ReflectionInfo_ConstructorParametersTypes()
	{
		// Arrange
		var type = typeof(Test);

		// Act
		var reflectionInfo = new ReflectionInfo(type);
		var actual = reflectionInfo.ConstructorParametersTypes.Count();

		// Assert
		Assert.AreEqual(2, actual);
	}

	[Test]
	public void ReflectionInfo_Attributes_Empty()
	{
		// Act
		var reflectionInfo = new ReflectionInfo(typeof(TestNotAttr));
		var actual = reflectionInfo.Attributes.Any();

		// Assert
		Assert.False(actual);
	}

	[Test]
	public void ReflectionInfo_Attributes_NotNull()
	{
		// Act
		var reflectionInfo = new ReflectionInfo(typeof(Test));
		var actual = reflectionInfo.Attributes;

		// Assert
		Assert.NotNull(actual);
	}

	#region Nested

	[SuppressMessage("ReSharper", "UnusedParameter.Local")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	private sealed class TestManyConstructors
	{
		public TestManyConstructors(int param)
		{
		}

		public TestManyConstructors(int param1,
			int param2)
		{
		}
	}

	[SuppressMessage("ReSharper", "UnusedParameter.Local")]
	private sealed class Test
	{
		public Test(int param1,
			int param2)
		{
		}
	}

	private sealed class TestNotAttr
	{
	}

	#endregion
}