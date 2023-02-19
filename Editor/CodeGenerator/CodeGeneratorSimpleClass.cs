namespace EM.Foundation.Editor
{

public class CodeGeneratorSimpleClass : ICodeGenerator
{
	private const string Template = "\npublic {0} class {1}\n{{{2}}}\n";

	private readonly string _name;

	private readonly string _modifiers;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = _codeGenerator?.Create() ?? string.Empty;
		code = string.Format(Template, _modifiers, _name, code);

		return code;
	}

	#endregion

	#region ClassTemplate

	public CodeGeneratorSimpleClass(string name,
		string modifiers,
		ICodeGenerator codeGenerator)
	{
		_name = name;
		_modifiers = modifiers;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}