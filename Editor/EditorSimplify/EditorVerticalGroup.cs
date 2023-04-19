namespace EM.Foundation.Editor
{

using System;
using UnityEditor;
using UnityEngine;

public sealed class EditorVerticalGroup : IDisposable
{
	private float _space;

	#region IDisposable

	public void Dispose()
	{
		EditorGUILayout.EndVertical();

		if (_space != 0)
		{
			EditorGUILayout.EndHorizontal();
		}
	}

	#endregion

	#region EditorVerticalGroup

	public EditorVerticalGroup()
	{
		_space = 0;
		EditorGUILayout.BeginVertical();
	}

	public EditorVerticalGroup(string style,
		params GUILayoutOption[] options)
	{
		_space = 0;
		EditorGUILayout.BeginVertical(style, options);
	}
	
	public EditorVerticalGroup(float space, string style,
		params GUILayoutOption[] options)
	{
		_space = space;
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(space);
		EditorGUILayout.BeginVertical(style, options);
	}

	public EditorVerticalGroup(float space)
	{
		_space = space;
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(space);
		EditorGUILayout.BeginVertical();
	}

	#endregion
}

}