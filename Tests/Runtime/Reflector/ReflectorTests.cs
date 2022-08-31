using NUnit.Framework;
using EM.Foundation;

internal sealed class ReflectorTests
{
	[Test]
	public void Reflector_GetReflectionInfo_NotNull()
	{
		// Arrange
		var reflector = new Reflector();

		// Act
		var actual = reflector.GetReflectionInfo(typeof(Test));

		//Assert
		Assert.NotNull(actual);
	}

	[Test]
	public void Reflector_GetReflectionInfoT_NotNull()
	{
		// Arrange
		var reflector = new Reflector();

		// Act
		var actual = reflector.GetReflectionInfo<Test>();

		//Assert
		Assert.NotNull(actual);
	}

	[Test]
	public void Reflector_GetReflectionInfo_Pool()
	{
		// Arrange
		var reflector = new Reflector();
		var expected = reflector.GetReflectionInfo(typeof(Test));

		// Act
		var actual = reflector.GetReflectionInfo(typeof(Test));

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Reflector_GetReflectionInfoT_Pool()
	{
		// Arrange
		var reflector = new Reflector();
		var expected = reflector.GetReflectionInfo<Test>();

		// Act
		var actual = reflector.GetReflectionInfo<Test>();

		//Assert
		Assert.AreEqual(expected, actual);
	}

	#region Nested

	private sealed class Test
	{
	}

	#endregion
}