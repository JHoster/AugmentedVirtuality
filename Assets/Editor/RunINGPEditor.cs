using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RunINGP))]
public class RunINGPEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    RunINGP RCMD = (RunINGP)target;

    GUILayout.BeginHorizontal();

    if (GUILayout.Button("Run INGP"))
    {
      RCMD.Run();
    }

    if (GUILayout.Button("Load obj"))
    {
      RCMD.LoadObj();
    }

    GUILayout.EndHorizontal();
  }
}
