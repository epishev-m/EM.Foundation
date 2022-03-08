/*using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EM.Foundation.Editor;

namespace EM.Configs
{

public class CodeGeneratorEntity :
	ICodeGenerator
{
	private const string templateLibraryEntityLink = "\n\tpublic LibraryEntityLink<{0}> {1};\n";

	private const string templateBaseType = "\n\tpublic {0} {1};\n";
	
	private readonly TypeInfo typeInfo;

	#region ICodeGenerator

	public string Create()
	{
		var stringBuilder = new StringBuilder();
		var baseTypeInfo = typeInfo.BaseType.GetTypeInfo();
		CreateFields(stringBuilder, baseTypeInfo.DeclaredFields);
		CreateFields(stringBuilder, typeInfo.DeclaredFields);
		var code = stringBuilder.ToString();

		return code;
	}

	#endregion
	#region CodeGeneratorEntity

	public CodeGeneratorEntity(TypeInfo typeInfo)
	{
		this.typeInfo = typeInfo;
	}

	private static void CreateFields(
		StringBuilder stringBuilder,
		IEnumerable<FieldInfo> fields)
	{
		foreach (var fieldInfo in fields)
		{
			string field;

			if (fieldInfo.IsDefined(typeof(LibraryEntityLinkAttribute)))
			{
				var attribute = fieldInfo.GetCustomAttribute<LibraryEntityLinkAttribute>();
				field = string.Format(templateLibraryEntityLink, attribute.Type, fieldInfo.Name);
			}
			else
			{
				field = string.Format(templateBaseType, fieldInfo.FieldType, fieldInfo.Name);
			}

			stringBuilder.Append(field);
		}
	}

	#endregion
}

}*/