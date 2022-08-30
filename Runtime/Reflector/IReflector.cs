namespace EM.Foundation
{

using System;

public interface IReflector
{
	IReflectionInfo GetReflectionInfo<T>();

	IReflectionInfo GetReflectionInfo(Type type);
}

}