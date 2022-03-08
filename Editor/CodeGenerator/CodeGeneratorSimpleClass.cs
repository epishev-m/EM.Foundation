
namespace EM.Foundation.Editor
{

public class CodeGeneratorSimpleClass :
	ICodeGenerator
{
	private const string template = "\npublic sealed class {0}\n{{{1}}}\n";

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

	#region ClassTemplate

	public CodeGeneratorSimpleClass(
		string name,
		ICodeGenerator codeGenerator)
	{
		this.name = name;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}