namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AssistantWindowBase :
	EditorWindow
{
	private readonly List<AssistantWindowComponentGroupBox> components = new();

	#region EditorWindow

	private void OnEnable()
	{
		CreateComponents();

		foreach (var component in components)
		{
			component.Prepare();
			component.AddListener(Repaint);
		}
	}

	private void OnDisable()
	{
		components.ForEach(c => c.RemoveListener(Repaint));
	}

	private void OnGUI()
	{
		OnGUIButtons();
		components.ForEach(c => c.OnGUI());
	}

	#endregion

	#region AssistantWindowBase

	protected abstract IEnumerable<IAssistantWindowComponent> GetWindowComponents();
	
	private void CreateComponents()
	{
		components.ForEach(c => c.RemoveListener(Repaint));
		components.Clear();
		var newComponents = GetWindowComponents();

		foreach (var component in newComponents)
		{
			components.Add(new AssistantWindowComponentGroupBox(component));
		}
	}

	private void OnGUIButtons()
	{
		EditorGUILayout.BeginHorizontal("GroupBox");
		{
			if (GUILayout.Button("Show All"))
			{
				components.ForEach(c => c.Show());
			}

			if (GUILayout.Button("Hide All"))
			{
				components.ForEach(c => c.Hide());
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	#endregion
}

}