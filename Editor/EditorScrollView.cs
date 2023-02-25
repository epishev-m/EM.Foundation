namespace EM.Foundation.Editor
{

using System;
using UnityEditor;

public sealed class EditorScrollView : IDisposable
{
	#region IDisposable

	public void Dispose()
	{
		EditorGUILayout.EndScrollView();
	}

	#endregion

	#region EditorScrollView

	public EditorScrollView(ref UnityEngine.Vector2 scrollPos)
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
	}

	#endregion
}

}