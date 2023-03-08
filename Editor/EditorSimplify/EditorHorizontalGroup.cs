namespace EM.Foundation.Editor
{

using System;
using UnityEngine;
using UnityEditor;

public sealed class EditorHorizontalGroup : IDisposable
{
	#region IDisposable

	public void Dispose()
	{
		EditorGUILayout.EndHorizontal();
	}

	#endregion

	#region EditorHorizontalGroup

	public EditorHorizontalGroup()
	{
		EditorGUILayout.BeginHorizontal();
	}

	public EditorHorizontalGroup(string style,
		params GUILayoutOption[] options)
	{
		EditorGUILayout.BeginHorizontal(style, options);
	}

	#endregion
}

}