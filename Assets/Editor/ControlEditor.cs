using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControlGeneral))]
public class ControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Siguiente"))
        {
            ((ControlGeneral)target).Siguiente();
        }
        if (GUILayout.Button("Nuevo Inmigrante"))
        {
            ((ControlGeneral)target).InstanciarInmigrante();
        }
    }
}
