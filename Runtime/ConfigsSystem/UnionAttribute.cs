﻿namespace EM.Foundation
{

using System;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class,
	AllowMultiple = true,
	Inherited = false)]
public sealed class UnionAttribute :
	Attribute
{
	public Type Type { get; }

	public UnionAttribute(Type type)
	{
		Type = type;
	}
}

}