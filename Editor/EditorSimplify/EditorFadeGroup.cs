namespace EM.Foundation.Editor
{

using System;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class EditorFadeGroup : IDisposable
{
	#region IDisposable

	public void Dispose()
	{
		EditorGUILayout.EndFadeGroup();
	}

	#endregion

	#region EditorFoldout

	public EditorFadeGroup(string name,
		AnimBool show)
	{
		show.target = EditorGUILayout.Foldout(show.target, name, true);
		IsVisible = EditorGUILayout.BeginFadeGroup(show.faded);
	}

	public bool IsVisible { get; }

	#endregion
}

}