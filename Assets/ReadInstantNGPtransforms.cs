using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadInstantNGPtransforms : MonoBehaviour
{
  public string _SavePath = "C:/hmdPics/";
  public string fileName = "transforms";
  private StreamReader _reader;
  public string content;


  public List<string> file_paths = new List<string>();
  public List<float> sharpness = new List<float>();
  public List<float> transform_matrix = new List<float>();
  public List<float> tm = new List<float>();
  public List<Matrix4x4> tms = new List<Matrix4x4>();

  public GameObject prefab;

  // Start is called before the first frame update
  void Start()
  {
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //comma as decimal symbol causes problems
    fileName += ".json"; //".txt";
    if (!File.Exists(_SavePath + fileName))
      Debug.Log("File not found.");

    // Read the file and display it line by line.  
    foreach (string line in File.ReadLines(_SavePath + fileName))
    {
      if (line.Contains("file_path"))
        file_paths.Add(line.Remove(line.Length - 1).Remove(0, 18));
      if (line.Contains("sharpness"))
        sharpness.Add(float.Parse(line.Remove(line.Length - 1).Remove(0, 18)));
      if (line.Contains("          "))
      {
        if(line.Contains(","))
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
      Matrix4x4 m = new Matrix4x4(new Vector4(tm[4], tm[8], tm[0] * -1, tm[12]), new Vector4(tm[5], tm[9], tm[1] * -1, tm[13]), new Vector4(tm[6], tm[10], tm[2] * -1, tm[14]), new Vector4(tm[7], tm[11], tm[3] * -1, tm[15]));
      tms.Add(m);
      GameObject pf = Instantiate(prefab, m.GetColumn(3), Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)), transform);
      pf.transform.localScale = new Vector3(m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);
      pf.name = file_paths[0];
      file_paths.RemoveAt(0);
    }

    //content = File.ReadAllText(_SavePath + fileName);

    //    foreach (string s in info) 
    //{
    //    found = s.IndexOf(": ");
    //    Console.WriteLine("   {0}", s.Substring(found + 2));
    //}
  }

  // Update is called once per frame
  void Update()
  {

  }
}
