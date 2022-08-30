namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Reflection;

public interface IReflectionInfo
{
	ConstructorInfo ConstructorInfo
	{
		get;
	}

	IEnumerable<Type> ConstructorParametersTypes
	{
		get;
	}

	IEnumerable<Attribute> Attributes
	{
		get;
	}
}

}
