namespace EM.Foundation.Editor
{

using System;
using UnityEditor;

public sealed class EditorIndentLevel : IDisposable
{
	private readonly int _level;

	#region IDisposable

	public void Dispose()
	{
		EditorGUI.indentLevel -= _level;
	}

	#endregion

	#region EditorGUIIndentLevel

	public EditorIndentLevel(int level = 1)
	{
		_level = level;
		EditorGUI.indentLevel += _level;
	}

	#endregion
}

}