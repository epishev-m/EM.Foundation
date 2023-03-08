namespace EM.Foundation.Editor
{

using System;
using UnityEditor;

public sealed class EditorDisabledGroup : IDisposable
{
	#region IDisposable

	public void Dispose()
	{
		EditorGUI.EndDisabledGroup();
	}

	#endregion

	#region EditorHorizontalGroup

	public EditorDisabledGroup(bool disabled)
	{
		EditorGUI.BeginDisabledGroup(disabled);
	}

	#endregion
}

}