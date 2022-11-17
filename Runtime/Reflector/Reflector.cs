namespace EM.Foundation
{

using System;
using System.Collections.Generic;

public sealed class Reflector : IReflector
{
	private readonly Dictionary<Type, IReflectionInfo> _reflectionInfoCache = new();

	#region IReflector

	public Result<IReflectionInfo> GetReflectionInfo<T>()
	{
		return GetReflectionInfo(typeof(T));
	}

	public Result<IReflectionInfo> GetReflectionInfo(Type type)
	{
		if (type == null)
		{
			return new ErrorResult<IReflectionInfo>(ReflectorStringResources.TypeNull(this));
		}

		IReflectionInfo reflectionInfo;

		if (_reflectionInfoCache.TryGetValue(type, out var value))
		{
			reflectionInfo = value;
		}
		else
		{
			reflectionInfo = new ReflectionInfo(type);
			_reflectionInfoCache.Add(type, reflectionInfo);
		}

		return new SuccessResult<IReflectionInfo>(reflectionInfo);
	}

	#endregion
}

}
