using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.CoordinateSystem;

public class Cam2Local : MonoBehaviour
{
  public RectTransform rectTransform; // Position, size, anchor and pivot information for a rectangle.
  public float X; //X in camera coordinates
  public float Y; //Y in camera coordinates
  public float Z; //Z in camera coordinates
  public Vector2 focalLength; //Normalized focal lengths in image coordinates
  //By default, camera focal length defined in NDC space, i.e., (fx, fy). Default to (1.0, 1.0).
  //To specify focal length in pixel space instead, i.e., (fx_pixel, fy_pixel), users should provide image_size = (image_width, image_height)
  //to enable conversions inside the API. (https://google.github.io/mediapipe/solutions/objectron.html#ndc-space)
  public Vector2 principalPoint; //Normalized principal point in image coordinates
  //By default, camera principal point defined in NDC space, i.e., (px, py). Default to (0.0, 0.0).
  //To specify principal point in pixel space, i.e.,(px_pixel, py_pixel), users should provide image_size = (image_width, image_height)
  //to enable conversions inside the API. (https://google.github.io/mediapipe/solutions/objectron.html#ndc-space)
  public float zScale; //Ratio of Z values in camera coordinates to local coordinates in Unity
  //imageRotation //Counterclockwise rotation angle of the input image
  //public bool isMirrored //Set to true if the original coordinates is mirrored

  public Vector3 rwc;

  //public GameObject inputPoint;
  public GameObject outputPoint;
  //public GameObject[] inputPoints;
  //public GameObject[] outputPoints;
  //private int i = 0;

  // Start is called before the first frame update
  void Start()
    {
    rwc = CameraCoordinate.GetLocalPosition(rectTransform, X, Y, Z, focalLength, principalPoint, zScale); //, imageRotation, isMirrored);
    }

    // Update is called once per frame
    void Update()
    {
    outputPoint.transform.position = CameraCoordinate.GetLocalPosition(rectTransform, X, Y, Z, focalLength, principalPoint, zScale); //, imageRotation, isMirrored);
    //i = 0;
    //foreach (GameObject go in inputPoints)
    //{
    //  // Transforms of inputPoints nicht richtig, da children. Eventuelle Lösung: https://docs.unity3d.com/ScriptReference/Transform.TransformPoint.html
    //  outputPoints[i].transform.position = CameraCoordinate.GetLocalPosition(rectTransform, go.transform.position.x, go.transform.position.y, go.transform.position.z, focalLength, principalPoint, zScale); //, imageRotation, isMirrored);
    //  i++;
    //}
  }
}
