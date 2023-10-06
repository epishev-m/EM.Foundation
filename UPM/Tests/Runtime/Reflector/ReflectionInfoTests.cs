using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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
	public void ReflectionInfo_GetConstructorInfo_ManyConstructors()
	{
		// Arrange
		var type = typeof(TestManyConstructors);
		var reflectionInfo = new ReflectionInfo(type);

		// Act
		var resultResult = reflectionInfo.GetConstructorInfo();
		var actual = resultResult.Failure;

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ReflectionInfo_GetConstructorInfo()
	{
		// Arrange
		var reflectionInfo = new ReflectionInfo(typeof(Test));

		// Act
		var result = reflectionInfo.GetConstructorInfo();
		var actualSuccess = result.Success;
		var actual = result.Data;

		// Assert
		Assert.IsTrue(actualSuccess);
		Assert.NotNull(actual);
	}

	[Test]
	public void ReflectionInfo_ConstructorParametersTypes()
	{
		// Arrange
		var type = typeof(Test);
		var reflectionInfo = new ReflectionInfo(type);

		// Act
		var result = reflectionInfo.GetConstructorParamTypes();
		var actual = result.Data.Count();

		// Assert
		Assert.AreEqual(2, actual);
	}

	[Test]
	public void ReflectionInfo_GetAttributes_Empty()
	{
		// Act
		var reflectionInfo = new ReflectionInfo(typeof(TestNotAttr));
		var result = reflectionInfo.GetAttributes();
		var actual = result.Data.Any();

		// Assert
		Assert.False(actual);
	}

	[Test]
	public void ReflectionInfo_Attributes_NotNull()
	{
		// Act
		var reflectionInfo = new ReflectionInfo(typeof(Test));
		var result = reflectionInfo.GetAttributes();
		var actual = result.Data;

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