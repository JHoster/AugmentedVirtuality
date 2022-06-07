using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TakePics : MonoBehaviour
{
  public bool record;
  public WebCamTexture wct;
  public string _SavePath = "C:/hmdPics/";
  private int _CaptureCounter = 0;
  public GameObject hmd; //head mounted display
  public Matrix4x4 transformMatrix;
  public string fileName = "transforms";
  private StreamWriter _writer;
  public float repeatRate;
  public Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
  public float scale = 1;
  public bool camOffset;
  private Transform hmdT;
  public GameObject prefab;
  public List<Vector3> camPos = new List<Vector3>();
  private bool closed;
  public bool showUnitBox;
  public bool changeUnitBox;
  public GameObject UnitBox;
  public Vector3 center; //similar to offset but calculated while taking pics
  public bool useObjectronCenter;
  public GameObject VO;
  public float chairHeight;

  void Start()
  {
    if (record)
    {
      System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //comma as decimal symbol causes problems
      fileName += ".json"; //".txt";
      if (File.Exists(_SavePath + fileName))
      {
        File.Delete(_SavePath + fileName);
        Debug.Log("Former" + fileName + " got deleted.");

        if (Directory.Exists(_SavePath + "/images")) { Directory.Delete(_SavePath + "/images", true); }
        Directory.CreateDirectory(_SavePath + "/images");

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
    }
    InvokeRepeating(nameof(TakePic), 3f, repeatRate); //Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
  }

  //Update is called once per frame
  void Update()
  {
    if (hmd == null && GameObject.Find("Camera"))
      hmd = GameObject.Find("Camera");
    if (hmd && camPos.Count > 0 && Input.GetKeyDown(KeyCode.Space))
    {
      if (record)
      {
        _writer.WriteLine("    }"); //last frame without comma
        _writer.WriteLine("  ]");
        _writer.WriteLine("}");
        _writer.Close();
      }
      closed = true;
      CancelInvoke();
      Debug.Log("Took " + camPos.Count + " pictures.");

      UnitBox.transform.position = center / camPos.Count;
      //}

      ////Show and change Unit Bounding Box (height)
      //if (closed && changeUnitBox)
      //{
      //  UnitBox.SetActive(true);
      //  if (Input.GetKey(KeyCode.UpArrow))
      //    UnitBox.transform.position += new Vector3(0, Time.deltaTime, 0);
      //  if (Input.GetKey(KeyCode.DownArrow))
      //    UnitBox.transform.position += new Vector3(0, -Time.deltaTime, 0);
      //}
      //else
      //  UnitBox.SetActive(false);

      //if (closed && Input.GetKeyDown(KeyCode.Return))
      //{
      offset = Vector3.zero;
      foreach (Vector3 c in camPos)
      {
        offset += c;
      }
      offset /= camPos.Count;
      center = UnitBox.transform.position;
      Debug.Log("offsetOrg " + offset.ToString("F4"));
      //Debug.Log("center " + center.ToString("F4"));
      //Debug.Log(offset == center);
      if (changeUnitBox)
        offset = center;
      if (useObjectronCenter && VO) //Needs VO with Ray2World
        offset = VO.transform.position;
      if (chairHeight != 0)
      {
        offset = new Vector3(offset.x, offset.y - (chairHeight / 2), offset.z); //offset.y = hmdHeight, but chair is smaller
        //scale = 1 - chairHeight;
      }
      Debug.Log("new offset: " + offset.ToString("F4"));
      if (VO)
        Debug.Log("VOPos " + VO.transform.position);
      offset = new Vector3(0.5f, 0.5f, 0.5f) - offset;
      Debug.Log("final offset: " + offset.ToString("F4"));
      if (record)
      {
        //Correct offset:
        lineChanger("  \"offset\": [" + offset.x + "," + offset.z + "," + offset.y + "],", _SavePath + fileName, 16);

        Debug.Log("Finished");
      }
    }
  }

  static void lineChanger(string newText, string fileName, int line_to_edit)
  {
    string[] arrLine = File.ReadAllLines(fileName);
    arrLine[line_to_edit - 1] = newText;
    File.WriteAllLines(fileName, arrLine);
  }

  private void TakePic()
  {
    //Debug.Log("TakePic started");
    if (hmd)
    {
      hmdT = hmd.transform;

      if (camOffset)
        hmdT.transform.position = hmdT.transform.position + (hmdT.transform.forward * 0.1f);
      //Debug.Log("offset applied");

      camPos.Add(hmdT.transform.position);
      //Instantiate(prefab, hmd.transform.position, hmd.transform.rotation, transform);

      center += hmdT.transform.position;
      if (showUnitBox)
      {
        UnitBox.SetActive(true);
        UnitBox.transform.position = center / camPos.Count;
      }
      else
        UnitBox.SetActive(false);

      if (record)
      {
        //mirror
        hmdT.transform.position = new Vector3(-hmdT.transform.position.x, hmdT.transform.position.y, hmdT.transform.position.z);
        hmdT.transform.eulerAngles = new Vector3(-hmdT.transform.eulerAngles.x, -hmdT.transform.localEulerAngles.y + 180, hmdT.transform.localEulerAngles.z);

        //Rotate 90° around y
        hmdT.transform.RotateAround(Vector3.zero, Vector3.up, 90f);

        transformMatrix = hmdT.transform.localToWorldMatrix;

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
        wct = GetComponent<CamTextureSource>()._webCamTexture;
        Texture2D snap = new Texture2D(wct.width, wct.height);
        snap.SetPixels(wct.GetPixels());
        snap.Apply();
        System.IO.File.WriteAllBytes(_SavePath + "images/" + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
      }
    }
  }

  private void OnDisable()
  {
    if (!closed && record)
    {
      _writer.WriteLine("    }"); //last frame without comma
      _writer.WriteLine("  ]");
      _writer.WriteLine("}");
      _writer.Close();
    }
  }
}
