using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCoordinateSystemLocation : MonoBehaviour
{
  public Vector3 position;
  public Vector3 localPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    position = transform.position;
    localPosition = transform.localPosition;
    }
}
