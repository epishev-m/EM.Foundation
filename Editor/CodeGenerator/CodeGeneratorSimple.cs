namespace EM.Foundation.Editor
{

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public sealed class CodeGeneratorSimple :
	ICodeGenerator
{
	private readonly string name;

	private readonly string path;

	private readonly ICodeGenerator codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		var code = codeGenerator?.Create() ?? string.Empty;

		try
		{
			using var stream = File.Open(path + name, FileMode.Create, FileAccess.Write);
			using var writer = new StreamWriter(stream);
			writer.Write(code);
		}
		catch (Exception e)
		{
			Debug.LogException(e);

			throw;
		}

		AssetDatabase.Refresh();

		return code;
	}

	#endregion

	#region CodeGeneratorSimple

	public CodeGeneratorSimple(string name,
		string path,
		ICodeGenerator codeGenerator)
	{
		this.name = name;
		this.path = path;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}