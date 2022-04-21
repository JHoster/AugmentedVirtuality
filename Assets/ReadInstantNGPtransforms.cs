using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ReadInstantNGPtransforms : MonoBehaviour
{
  public string _SavePath = "C:/hmdPics/";
  public string fileName = "transforms";
  //private StreamReader _reader;
  //public string content;

  public Vector3 offset;
  public float scale;

  public List<string> file_paths = new List<string>();
  public List<float> sharpness = new List<float>();
  public List<float> transform_matrix = new List<float>();
  public List<float> tm = new List<float>();
  public List<Matrix4x4> tms = new List<Matrix4x4>();
  public bool colmapOffsetAndScale;
  public bool colmapOrder; //colmap uses inverse order
  public GameObject prefab;
  public List<GameObject> cams = new List<GameObject>();
  public bool centerByBounds;
  public bool centerToUB;
  public Vector3 centerPoint;
  public Vector3 offsetToUnitBox;


  public void Start()
  {
    ReadINGP();
  }

  public void Reset()
  {
    while (transform.childCount > 0)
      foreach (Transform child in transform)
        DestroyImmediate(child.gameObject);
    offset = Vector3.zero;
    centerPoint = Vector3.zero;
    offsetToUnitBox = Vector3.zero;
    scale = 0;
    file_paths.Clear();
    sharpness.Clear();
    transform_matrix.Clear();
    tm.Clear();
    tms.Clear();
    cams.Clear();
    transform.localScale = Vector3.one;
    transform.position = Vector3.zero;
  }

  public void ReadINGP()
  {
    Reset();

    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //comma as decimal symbol causes problems
    if(!fileName.EndsWith(".json"))
      fileName += ".json"; //".txt";
    if (_SavePath.Contains("/"))
      _SavePath = _SavePath.Replace("/", "\\");
    if (!File.Exists(_SavePath + fileName))
      Debug.Log("File not found.");

    // Read the file and display it line by line.  
    foreach (string line in File.ReadLines(_SavePath + fileName))
    {
      if (line.Contains("offset"))
      {
        string[] offs = line.Remove(line.Length - 2).Remove(0, 13).Split(',');
        offset.z = float.Parse(offs[0]);
        offset.x = float.Parse(offs[1]);
        offset.y = float.Parse(offs[2]);
      }
      if (line.Contains("\"scale"))
        scale = float.Parse(line.Remove(line.Length - 1).Remove(0, 10));
      if (line.Contains("file_path"))
        file_paths.Add(line.Remove(line.Length - 1).Remove(0, 18));
      if (line.Contains("sharpness"))
        sharpness.Add(float.Parse(line.Remove(line.Length - 1).Remove(0, 18)));
      if (line.Contains("          "))
      {
        if (line.Contains(","))
          transform_matrix.Add(float.Parse(line.Remove(line.Length - 1).Remove(0, 9)));
        else
          transform_matrix.Add(float.Parse(line.Remove(0, 9)));
      }
    }


    while (transform_matrix.Count > 0)
    {
      tm = transform_matrix.GetRange(0, 16);
      transform_matrix.RemoveRange(0, 16);
      //from zxyw to xyzw and -1 for z-Axis
      //Matrix4x4 m = new Matrix4x4(new Vector4(tm[4], tm[8], tm[0] * -1, tm[12]), new Vector4(tm[5], tm[9], tm[1] * -1, tm[13]), new Vector4(tm[6], tm[10], tm[2] * -1, tm[14]), new Vector4(tm[7], tm[11], tm[3] * -1, tm[15]));
      Matrix4x4 m = new Matrix4x4(new Vector4(tm[4], tm[8], tm[0], tm[12]), new Vector4(tm[5], tm[9], tm[1], tm[13]), new Vector4(tm[6], tm[10], tm[2], tm[14]), new Vector4(tm[7], tm[11], tm[3], tm[15]));
      //m[0, 2] *= -1;
      //m[1, 2] *= -1;
      //m[2, 2] *= -1;
      //m[3, 2] *= -1;
      tms.Add(m);
      GameObject pf = Instantiate(prefab, m.GetColumn(3), Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)), transform);
      pf.transform.localScale = new Vector3(m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);

      //mirror
      pf.transform.position = new Vector3(-pf.transform.position.x, pf.transform.position.y, pf.transform.position.z);
      pf.transform.eulerAngles = new Vector3(-pf.transform.eulerAngles.x, -pf.transform.localEulerAngles.y + 180, pf.transform.localEulerAngles.z);

      //pf.transform.RotateAround(Vector3.zero, Vector3.up, 90f);
      pf.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //just for better visualization

      pf.name = file_paths[0].Remove(file_paths[0].Length - 5).Remove(0, 11);
      file_paths.RemoveAt(0);
      //if (colmap)
      cams.Add(pf);
      //pf.name = file_paths[0];
      //Debug.Log("old " + pf.transform.GetSiblingIndex());
      //Debug.Log("should be " + int.Parse(file_paths[0].Remove(file_paths[0].Length - 5).Remove(0, 11)));
      //if (colmap)
      //  pf.transform.SetSiblingIndex(int.Parse(file_paths[0].Remove(file_paths[0].Length - 5).Remove(0, 11)));
      //Debug.Log("new " + pf.transform.GetSiblingIndex());
      //file_paths.RemoveAt(0);
    }

    if (colmapOrder)
    {
      cams = cams.OrderBy(go => int.Parse(go.name)).ToList();
      //cams.Reverse();
      //int counter = 0;
      //foreach (GameObject cam in cams)
      //{
      //  cam.transform.SetAsLastSibling();
      //  cam.name = counter.ToString();
      //  counter++;
      //}
    }

    if (centerByBounds)
    {
      var bounds = new Bounds(cams[0].transform.position, Vector3.zero);
      foreach (GameObject cam in cams)
      {
        bounds.Encapsulate(cam.transform.position);
      }
      centerPoint = bounds.center;
      offsetToUnitBox = new Vector3(0.5f, 0.5f, 0.5f) - centerPoint;
    }

    if (colmapOffsetAndScale)
    {
      offset = new Vector3(0.5f, 0.5f, 0.5f);
      scale = 0.33f;
      transform.localPosition = offset;
      transform.localScale = new Vector3(scale, scale, scale);
    }
    else
    {
      if (centerToUB)
        transform.localPosition = offsetToUnitBox;
      else
        transform.localPosition = offset;
      transform.localScale = new Vector3(scale, scale, scale);
    }

    //Now it's possible to compare camsSorted Lists elementwise!


    //if (colmap)
    //{
    //  List<Transform> children = new List<Transform>();
    //  for (int i = transform.childCount - 1; i >= 0; i--)
    //  {
    //    Transform child = transform.GetChild(i);
    //    children.Add(child);
    //    child.parent = null;
    //  }
    //  children.Sort((Transform t1, Transform t2) => { return t1.name.CompareTo(t2.name); });
    //  foreach (Transform child in children)
    //  {
    //    child.parent = transform;
    //  }
    //}

    //if (colmap)
    //{
    //  if (counter < 2)
    //  {
    //    Debug.Log("\"./images/" + counter + ".png\"");
    //    Transform currentChild = transform.Find("./images/" + counter + ".png");
    //    Debug.Log(currentChild.gameObject.name);
    //    Debug.Log(currentChild.GetSiblingIndex());
    //    currentChild.SetAsLastSibling();
    //    Debug.Log(currentChild.GetSiblingIndex());
    //  }
    //}

    //if (colmap)
    //{
    //  foreach (Transform child in transform)
    //  {
    //    child.name = "\"./images/" + counter + ".png\"";
    //    counter--;
    //  }
    //}

    //if (colmap) // Invert colmap order
    //{
    //  camsSorted = cams.OrderBy(go => go.name).ToList();
    //}

    //content = File.ReadAllText(_SavePath + fileName);

    //    foreach (string s in info) 
    //{
    //    found = s.IndexOf(": ");
    //    Console.WriteLine("   {0}", s.Substring(found + 2));
    //}
  }

  // Update is called once per frame
  //void Update()
  //{

  //}
}
