
namespace EM.Foundation.Editor
{

public sealed class CodeGeneratorSimpleNamespace :
	ICodeGenerator
{
	private const string template = "\nnamespace {0}\n{{{1}}}\n";

	private readonly string name;
	
	private readonly ICodeGenerator codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = codeGenerator?.Create() ?? string.Empty;
		code = string.Format(template, name, code);

		return code;
	}

	#endregion
	#region CodeGeneratorSimpleNamespace

	public CodeGeneratorSimpleNamespace(
		string name,
		ICodeGenerator codeGenerator)
	{
		this.name = name;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}