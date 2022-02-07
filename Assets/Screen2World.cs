using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen2World : MonoBehaviour
{
  /// <summary>
  ///   Using ScreenToWOrldPoint Function to transform bounding box to local Unity coordinates.
  /// </summary>
  public Camera cam;
  public GameObject[] inputPoints;
  public GameObject[] outputPoints;
  private int i=0;
  public int depth=0;
    // Start is called before the first frame update
    void Start()
    {
    cam = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    i = 0;
    foreach (GameObject go in inputPoints)
    {
      // Transforms of inputPoints nicht richtig, da children. Eventuelle Lösung: https://docs.unity3d.com/ScriptReference/Transform.TransformPoint.html
      outputPoints[i].transform.position = cam.ScreenToWorldPoint(new Vector3(go.transform.position.x, go.transform.position.y, depth));
      i++;
    }
  }
}
