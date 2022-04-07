using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TakePics : MonoBehaviour
{
  public WebCamTexture wct;
  public string _SavePath = "C:/hmdPics/";
  private int _CaptureCounter = 0;
  public GameObject hmd; //head mounted display
  public Matrix4x4 transformMatrix;
  public string fileName = "transforms";
  private StreamWriter _writer;
  public float repeatRate;
  public Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
  public float scale = 2f;
  public bool camOffset;
  public GameObject prefab;

  void Start()
  {
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //comma as decimal symbol causes problems
    fileName += ".json"; //".txt";
    if (File.Exists(_SavePath + fileName))
    {
      File.Delete(_SavePath + fileName);
      Debug.Log("Former" + fileName + " got deleted.");
      //Debug.Log(fileName + " already exists.");
      //enabled = false;
    }
    _writer = File.CreateText(_SavePath + fileName);
    _writer.WriteLine("{");
    _writer.WriteLine("  \"camera_angle_x\": 1.64,");
    _writer.WriteLine("  \"camera_angle_y\": 1.32,");
    _writer.WriteLine("  \"fl_x\": 300,");
    _writer.WriteLine("  \"fl_y\": 300,");
    _writer.WriteLine("  \"k1\": -0.19,");
    _writer.WriteLine("  \"k2\": 0.028,");
    _writer.WriteLine("  \"p1\": -0.001,");
    _writer.WriteLine("  \"p2\": 0.0002,");
    _writer.WriteLine("  \"cx\": 320.0,");
    _writer.WriteLine("  \"cy\": 240.0,");
    _writer.WriteLine("  \"w\": 640.0,");
    _writer.WriteLine("  \"h\": 480.0,");
    _writer.WriteLine("  \"aabb_scale\": 16,");
    _writer.WriteLine("  \"scale\": " + scale + ",");
    _writer.WriteLine("  \"offset\": [" + offset.z + "," + offset.x + "," + offset.y + "],"); //z, x, y in instant-ngp
    _writer.WriteLine("  \"frames\": [");
    InvokeRepeating(nameof(TakePic), 3f, repeatRate); //Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
  }

  //Update is called once per frame
  void Update()
  {
    if (hmd == null && GameObject.Find("Camera"))
      hmd = GameObject.Find("Camera");
    //if (hmd && Input.GetKeyDown(KeyCode.P))
    //  TakePic();
  }

  private void TakePic()
  {
    //Debug.Log("TakePic started");
    if (hmd)
    {
      if (camOffset)
        hmd.transform.position = hmd.transform.position + (hmd.transform.forward * 0.1f);
      //Debug.Log("offset applied");

      //mirror
      hmd.transform.position = new Vector3(-hmd.transform.position.x, hmd.transform.position.y, hmd.transform.position.z);
      hmd.transform.eulerAngles = new Vector3(-hmd.transform.eulerAngles.x, -hmd.transform.localEulerAngles.y + 180, hmd.transform.localEulerAngles.z);
      //Instantiate(prefab, hmd.transform.position, hmd.transform.rotation, transform);

      //Rotate 90° around y
      hmd.transform.RotateAround(Vector3.zero, Vector3.up, 90f);

      transformMatrix = hmd.transform.localToWorldMatrix;

      //     x     y     z
      //x ([0,0],[0,1],[0,2],[0,3])
      //y ([1,0],[1,1],[1,2],[1,3])
      //z ([2,0],[2,1],[2,2],[2,3])
      //  ([3,0],[3,1],[3,2],[3,3])

      //Unity's Matrix4x4 struct only supports Matrix * Vector:
      // (m00, m01, m02, m03)   (V.x)   (m00*V.x + m01*V.y + m02*V.z + m03*V.w)
      // (m10, m11, m12, m13) * (V.y) = (m10*V.x + m11*V.y + m12*V.z + m13*V.w)
      // (m20, m21, m22, m23)   (V.z)   (m20*V.x + m21*V.y + m22*V.z + m23*V.w)
      // (m30, m31, m32, m33)   (V.w)   (m30*V.x + m31*V.y + m32*V.z + m33*V.w)

      //Change z-axis sign (Unity -> OpenGL)
      //transformMatrix[a, b], a refers to the row index, while b refers to the column index.
      //transformMatrix[0, 2] *= -1;
      //transformMatrix[1, 2] *= -1;
      //transformMatrix[2, 2] *= -1;
      //transformMatrix[3, 2] *= -1;
    }

    if (_CaptureCounter > 0)
      _writer.WriteLine("    },"); //last frame with comma
    _writer.WriteLine("    {");
    _writer.WriteLine("      \"file_path\": \"./images/" + _CaptureCounter.ToString() + ".png\",");
    _writer.WriteLine("      \"sharpness\": 100.0,");
    _writer.WriteLine("      \"transform_matrix\": [");
    //changed order of elements to match instant-ngp coordinate systems order zxyw
    _writer.WriteLine("        [");
    _writer.WriteLine("          " + transformMatrix[2, 0] + ",");
    _writer.WriteLine("          " + transformMatrix[2, 1] + ",");
    _writer.WriteLine("          " + transformMatrix[2, 2] + ",");
    _writer.WriteLine("          " + transformMatrix[2, 3]);
    _writer.WriteLine("        ],");
    _writer.WriteLine("        [");
    _writer.WriteLine("          " + transformMatrix[0, 0] + ",");
    _writer.WriteLine("          " + transformMatrix[0, 1] + ",");
    _writer.WriteLine("          " + transformMatrix[0, 2] + ",");
    _writer.WriteLine("          " + transformMatrix[0, 3]);
    _writer.WriteLine("        ],");
    _writer.WriteLine("        [");
    _writer.WriteLine("          " + transformMatrix[1, 0] + ",");
    _writer.WriteLine("          " + transformMatrix[1, 1] + ",");
    _writer.WriteLine("          " + transformMatrix[1, 2] + ",");
    _writer.WriteLine("          " + transformMatrix[1, 3]);
    _writer.WriteLine("        ],");
    _writer.WriteLine("        [");
    _writer.WriteLine("          " + transformMatrix[3, 0] + ",");
    _writer.WriteLine("          " + transformMatrix[3, 1] + ",");
    _writer.WriteLine("          " + transformMatrix[3, 2] + ",");
    _writer.WriteLine("          " + transformMatrix[3, 3]);
    _writer.WriteLine("        ]");
    //elements of rows in order (xyzw)
    //_writer.WriteLine("        [");
    //_writer.WriteLine("          " + transformMatrix[0, 0] + ",");
    //_writer.WriteLine("          " + transformMatrix[0, 1] + ",");
    //_writer.WriteLine("          " + transformMatrix[0, 2] + ",");
    //_writer.WriteLine("          " + transformMatrix[0, 3]);
    //_writer.WriteLine("        ],");
    //_writer.WriteLine("        [");
    //_writer.WriteLine("          " + transformMatrix[1, 0] + ",");
    //_writer.WriteLine("          " + transformMatrix[1, 1] + ",");
    //_writer.WriteLine("          " + transformMatrix[1, 2] + ",");
    //_writer.WriteLine("          " + transformMatrix[1, 3]);
    //_writer.WriteLine("        ],");
    //_writer.WriteLine("        [");
    //_writer.WriteLine("          " + transformMatrix[2, 0] + ",");
    //_writer.WriteLine("          " + transformMatrix[2, 1] + ",");
    //_writer.WriteLine("          " + transformMatrix[2, 2] + ",");
    //_writer.WriteLine("          " + transformMatrix[2, 3]);
    //_writer.WriteLine("        ],");
    //_writer.WriteLine("        [");
    //_writer.WriteLine("          " + transformMatrix[3, 0] + ",");
    //_writer.WriteLine("          " + transformMatrix[3, 1] + ",");
    //_writer.WriteLine("          " + transformMatrix[3, 2] + ",");
    //_writer.WriteLine("          " + transformMatrix[3, 3]);
    //_writer.WriteLine("        ]");
    _writer.WriteLine("      ]");

    //_writer.WriteLine(hmdPos.ToString() + hmdRot.ToString() + hmdScale.ToString());
    //Debug.Log("transforms written, picture gets taken");
    wct = GetComponent<CamTextSource>()._webCamTexture;
    Texture2D snap = new Texture2D(wct.width, wct.height);
    snap.SetPixels(wct.GetPixels());
    snap.Apply();
    System.IO.File.WriteAllBytes(_SavePath + "images/" + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
    ++_CaptureCounter;
  }

  private void OnDisable()
  {
    _writer.WriteLine("    }"); //last frame without comma
    _writer.WriteLine("  ]");
    _writer.WriteLine("}");
    _writer.Close();
  }
}
