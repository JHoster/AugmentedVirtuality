using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeasurementInstantNGPCams))]
public class INGPMeasureCamsEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    MeasurementInstantNGPCams MINGPCams = (MeasurementInstantNGPCams)target;

    if (GUILayout.Button("Measure differences in cameras"))
    {
      MINGPCams.MINGPC();
    }
  }
}
