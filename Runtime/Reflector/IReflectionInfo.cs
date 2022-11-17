namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Reflection;

public interface IReflectionInfo
{
	Result<ConstructorInfo> GetConstructorInfo();

	Result<IEnumerable<Type>> GetConstructorParamTypes();

	Result<IEnumerable<Attribute>> GetAttributes();
}

}
