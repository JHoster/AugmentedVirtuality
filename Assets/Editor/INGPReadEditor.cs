using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReadInstantNGPtransforms))]
public class INGPReadEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    ReadInstantNGPtransforms readINGPtrans = (ReadInstantNGPtransforms)target;

    GUILayout.BeginHorizontal();

    if (GUILayout.Button("Read INGP transforms"))
    {
      readINGPtrans.ReadINGP();
    }

    if (GUILayout.Button("Reset"))
    {
      readINGPtrans.Reset();
    }

    GUILayout.EndHorizontal();
  }
  //public override void OnInspectorGUI()
  //{
  //  DrawDefaultInspector();

  //  MeasurementInstantNGPCams something = (MeasurementInstantNGPCams)target;
  //  if (GUILayout.Button("Measure differences in cameras"))
  //  {
  //    something.MINGPC();
  //  }
  //}
}
