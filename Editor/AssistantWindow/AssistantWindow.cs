namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AssistantWindowBase :
	EditorWindow
{
	private readonly List<AssistantWindowComponentGroupBox> _components = new();

	#region EditorWindow

	private void OnEnable()
	{
		CreateComponents();

		foreach (var component in _components)
		{
			component.Prepare();
			component.AddListener(Repaint);
		}
	}

	private void OnDisable()
	{
		_components.ForEach(c => c.RemoveListener(Repaint));
	}

	private void OnGUI()
	{
		OnGUIButtons();
		_components.ForEach(c => c.OnGUI());
	}

	#endregion

	#region AssistantWindowBase

	protected abstract IEnumerable<IAssistantWindowComponent> GetWindowComponents();

	private void CreateComponents()
	{
		_components.ForEach(c => c.RemoveListener(Repaint));
		_components.Clear();
		var newComponents = GetWindowComponents();

		foreach (var component in newComponents)
		{
			_components.Add(new AssistantWindowComponentGroupBox(component));
		}
	}

	private void OnGUIButtons()
	{
		EditorGUILayout.BeginHorizontal("GroupBox");

		{
			if (GUILayout.Button("Show All"))
			{
				_components.ForEach(c => c.Show());
			}

			if (GUILayout.Button("Hide All"))
			{
				_components.ForEach(c => c.Hide());
			}
		}

		EditorGUILayout.EndHorizontal();
	}

	#endregion
}

}