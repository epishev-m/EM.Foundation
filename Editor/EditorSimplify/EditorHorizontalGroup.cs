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

	public EditorHorizontalGroup(float space = 0)
	{
		EditorGUILayout.BeginHorizontal();

		if (space != 0)
		{
			GUILayout.Space(space);
		}
	}

	public EditorHorizontalGroup(string style,
		params GUILayoutOption[] options)
	{
		EditorGUILayout.BeginHorizontal(style, options);
	}

	#endregion
}

}