using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VoiceModule))]
public class VoiceModuleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // CreateButton("apple");
        // CreateButton("bottle");
    }

    void CreateButton(string s)
    {
        if(GUILayout.Button(s))
        {
            if(Application.isPlaying)
            {
                ((VoiceModule)target).Command(s);
            }
        }

    }
}
