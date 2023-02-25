namespace EM.Foundation.Editor
{

using System;
using UnityEditor;
using UnityEngine;

public sealed class EditorVerticalGroup : IDisposable
{
	#region IDisposable

	public void Dispose()
	{
		EditorGUILayout.EndVertical();
	}

	#endregion

	#region EditorVerticalGroup

	public EditorVerticalGroup()
	{
		EditorGUILayout.BeginVertical();
	}

	public EditorVerticalGroup(string style,
		params GUILayoutOption[] options)
	{
		EditorGUILayout.BeginVertical(style, options);
	}

	#endregion
}

}