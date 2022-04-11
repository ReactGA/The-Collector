using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyUITween))]
public class UITweenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MyUITween myUITween = (MyUITween)target;


        if (myUITween.AutoGenerateSizeByKey)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Key Start", GUILayout.Width(150), GUILayout.Height(30)))
            {
                myUITween.KeyStart();
            }
            if (GUILayout.Button("Key End", GUILayout.Width(150), GUILayout.Height(30)))
            {
                myUITween.KeyEnd();
            }
            if (GUILayout.Button("Finish", GUILayout.Width(150), GUILayout.Height(30)))
            {
                myUITween.FinishKey();
            }
            EditorGUILayout.EndHorizontal();
        }
        if (!myUITween.AutoGenerateSizeByKey)
        {
            if (myUITween.ShowPreviewButton)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Preview Animation", GUILayout.Width(150), GUILayout.Height(30)))
                {
                    if (!Application.isPlaying)
                    {
                        Debug.LogError("You can only preview in play mode, Press Play and click the button again");
                        return;
                    }
                    myUITween.PlayAnimation();

                }
            }
        }

        if (myUITween.useDefaultPopAnim_forButtons)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ScaleFactor", GUILayout.Width(210));
            myUITween.Scalefactor = EditorGUILayout.FloatField(myUITween.Scalefactor, GUILayout.Width(35));
            EditorGUILayout.EndHorizontal();
        }


    }
}
