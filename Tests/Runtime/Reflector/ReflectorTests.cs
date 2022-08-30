using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EM.Foundation;

internal sealed class ReflectorTests
{
	#region GetReflectionInfo

	[Test]
	public void Reflector_GetReflectionInfo_CountParams()
	{
		// Arrange
		var type = typeof(Test);
		var reflector = new Reflector();

		// Act
		var reflectionInfo = reflector.GetReflectionInfo(type);
		var actual = reflectionInfo.ConstructorParametersTypes.Count();

		// Assert
		Assert.AreEqual(2, actual);
	}

	[Test]
	public void Reflector_GetReflectionInfoGeneric_CountParams()
	{
		// Arrange
		var reflector = new Reflector();

		// Act
		var reflectionInfo = reflector.GetReflectionInfo<Test>();
		var actual = reflectionInfo.ConstructorParametersTypes.Count();

		// Assert
		Assert.AreEqual(2, actual);
	}

	#endregion

	#region Nested

	[SuppressMessage("ReSharper", "UnusedParameter.Local")]
	private sealed class Test
	{
		public Test(int param1,
			int param2)
		{
		}
	}

	#endregion
}