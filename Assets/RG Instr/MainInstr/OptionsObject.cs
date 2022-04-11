using UnityEngine;

[CreateAssetMenu(fileName = "buttonOptions",menuName ="ButtonOptionsPanel")]
public class OptionsObject : ScriptableObject
{
	public string PanelName,QuestionOrTitle;
	public string[] ButtonsOptions;
}
