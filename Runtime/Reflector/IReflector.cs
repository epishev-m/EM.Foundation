namespace EM.Foundation
{

using System;

public interface IReflector
{
	Result<IReflectionInfo> GetReflectionInfo<T>();

	Result<IReflectionInfo> GetReflectionInfo(Type type);
}

}