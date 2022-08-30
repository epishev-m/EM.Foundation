namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public sealed class ReflectionInfo :
	IReflectionInfo
{
	private readonly Type _type;

	private ConstructorInfo _constructorInfo;

	private IEnumerable<Type> _constructorParamTypes;

	private IEnumerable<Attribute> _attributes;

	#region IReflectionInfo

	public ConstructorInfo ConstructorInfo => _constructorInfo ??= GetConstructorInfo();

	public IEnumerable<Type> ConstructorParametersTypes => _constructorParamTypes ??= GetConstructorParamTypes();

	public IEnumerable<Attribute> Attributes => _attributes ??= GetAttributes();

	#endregion

	#region ReflectionInfo

	public ReflectionInfo(Type type)
	{
		Requires.NotNull(type, nameof(type));

		_type = type;
	}

	private ConstructorInfo GetConstructorInfo()
	{
		var constructors = _type.GetConstructors(BindingFlags.FlattenHierarchy |
												BindingFlags.Public |
												BindingFlags.Instance |
												BindingFlags.InvokeMethod);

		var result = constructors.Length switch
		{
			0 => default,
			> 1 => throw new InvalidOperationException($"Type {_type} has several constructors."),
			_ => constructors[0]
		};

		return result;
	}

	private IEnumerable<Type> GetConstructorParamTypes()
	{
		var constructorInfo = GetConstructorInfo();
		var constructorParameters = constructorInfo.GetParameters();
		var constructorParametersTypes = constructorParameters.Select(param => param.ParameterType);

		return constructorParametersTypes;
	}

	private IEnumerable<Attribute> GetAttributes()
	{
		var attributes = _type.GetCustomAttributes(false).Select(a => (Attribute) a);

		return attributes;
	}

	#endregion
}

}
