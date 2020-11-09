﻿
namespace EM.Foundation
{
	using System;
	using System.Globalization;

	internal static class StringResources
	{
		internal static string SuppliedTypeIsNotAReferenceType(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture,
				"The supplied type {0} is not a reference type. Only reference types are supported.",
					type);
		}

		internal static string SuppliedTypeIsNotAGivenType(Type type, Type givenType)
		{
			return string.Format(CultureInfo.InvariantCulture,
				"The supplied type {0} is not a given type {1}. Only given type are supported.",
				type, givenType);
		}
	}
}