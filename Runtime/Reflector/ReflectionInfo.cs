namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public sealed class ReflectionInfo : IReflectionInfo
{
	private readonly Type _type;

	private ConstructorInfo _constructorInfo;

	private IEnumerable<Type> _constructorParamTypes;

	private IEnumerable<Attribute> _attributes;

	#region IReflectionInfo

	public Result<ConstructorInfo> GetConstructorInfo()
	{
		if (_constructorInfo != null)
		{
			return new SuccessResult<ConstructorInfo>(_constructorInfo);
		}

		var constructors = _type.GetConstructors(BindingFlags.FlattenHierarchy |
												BindingFlags.Public |
												BindingFlags.Instance |
												BindingFlags.InvokeMethod);

		Result<ConstructorInfo> result = constructors.Length switch
		{
			0 => new SuccessResult<ConstructorInfo>(default),
			> 1 => new ErrorResult<ConstructorInfo>(ReflectorStringResources.MultipleConstructors(this)),
			_ => new SuccessResult<ConstructorInfo>(constructors[0])
		};

		return result;
	}

	public Result<IEnumerable<Type>> GetConstructorParamTypes()
	{
		var constructorInfo = GetConstructorInfo();

		if (constructorInfo.Failure)
		{
			return new ErrorResult<IEnumerable<Type>>(ReflectorStringResources.MultipleConstructors(this));
		}

		var constructorParameters = constructorInfo.Data.GetParameters();
		var constructorParametersTypes = constructorParameters.Select(param => param.ParameterType);

		return new SuccessResult<IEnumerable<Type>>(constructorParametersTypes);
	}

	public Result<IEnumerable<Attribute>> GetAttributes()
	{
		var attributes = _type.GetCustomAttributes(false).Select(a => (Attribute) a);

		return new SuccessResult<IEnumerable<Attribute>>(attributes);
	}

	#endregion

	#region ReflectionInfo

	public ReflectionInfo(Type type)
	{
		Requires.NotNull(type, nameof(type));

		_type = type;
	}

	#endregion
}

}