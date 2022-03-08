/*using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EM.Foundation.Editor;

namespace EM.Configs
{

public sealed class CodeGeneratorEntityLibrary :
	ICodeGenerator
{
	private const string fieldTemplate = "\n\tpublic List<{0}> {1};\n";

	private readonly IEnumerable<TypeInfo> typesInfo;
	
	#region ICodeGenerator

	public string Create()
	{
		var code = GetEntityLibraryFieldsCode();

		return code;
	}

	#endregion
	#region CodeGeneratorEntityLibrary

	public CodeGeneratorEntityLibrary(IEnumerable<TypeInfo> typesInfo)
	{
		this.typesInfo = typesInfo;
	}

	private string GetEntityLibraryFieldsCode()
	{
		var stringBuilder = new StringBuilder();

		foreach (var typeInfo in typesInfo)
		{
			var attribute = typeInfo.GetCustomAttribute(typeof(LibraryEntityGroupAttribute));

			var libraryEntityGroupAttribute = (LibraryEntityGroupAttribute) attribute;
			var names = libraryEntityGroupAttribute.Names.ToArray();

			foreach (var name in names)
			{
				stringBuilder.AppendFormat(fieldTemplate, typeInfo.Name, name);
			}
		}

		return stringBuilder.ToString();
	}

	#endregion
}

}*/