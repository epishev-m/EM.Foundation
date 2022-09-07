namespace EM.Foundation
{

using System;
using System.Collections.Generic;

public sealed class Reflector : IReflector
{
	private readonly Dictionary<Type, IReflectionInfo> _reflectionInfoCache = new();

	#region IReflector

	public IReflectionInfo GetReflectionInfo<T>()
	{
		return GetReflectionInfo(typeof(T));
	}

	public IReflectionInfo GetReflectionInfo(Type type)
	{
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

		return reflectionInfo;
	}

	#endregion
}

}
