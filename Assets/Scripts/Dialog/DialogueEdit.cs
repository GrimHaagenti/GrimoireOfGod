using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogue_Scr))]
public class DialogueEdit : Editor
{
    Dialogue_Scr dial;
    private void OnEnable()
    {
        dial = target as Dialogue_Scr;
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Texture2D tex = AssetPreview.GetAssetPreview(dial.m_characterPortrait);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), tex);
    }

}
