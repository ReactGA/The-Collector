using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptionsCreateClass))]
public class InstructionOptionEditor : Editor
{

	public override void OnInspectorGUI()
	{

		base.OnInspectorGUI();
		OptionsCreateClass optionsCreateClass = (OptionsCreateClass)target;
		 
		if(optionsCreateClass.CreateInEditMode){
			EditorGUILayout.Space();
			if (GUILayout.Button("Craete Options Panel",GUILayout.Width(200),GUILayout.Height(30)))
			{
				optionsCreateClass.CreateButtonOptionsPanel();
			}
		}
		

	}
}
