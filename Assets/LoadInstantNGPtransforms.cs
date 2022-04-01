using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadInstantNGPtransforms : MonoBehaviour
{
  public TextAsset textJSON;

  [System.Serializable]
  public class frames
  {
    public string file_path;
    public float sharpness;
    public List<List<object>> transform_matrix;
  }

  [System.Serializable]
  public class framesList
  {
    public frames[] frames;
  }

  public framesList newframesList = new framesList();

  //public string _SavePath = "C:/hmdPics/";
  //public Matrix4x4 transformMatrix;
  //public string fileName = "transforms";
  //private StreamReader _reader;

  // Start is called before the first frame update
  void Start()
  {


    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //comma as decimal symbol causes problems
    //fileName += ".json"; //".txt";
    //string jsonText = File.ReadAllText(fileName);

    newframesList = JsonUtility.FromJson<framesList>(textJSON.text);

    Debug.Log(newframesList.frames[0].file_path);
    Debug.Log(newframesList.frames[0].transform_matrix);

    //if (!File.Exists(_SavePath + fileName))
    //{
    //  Debug.Log("File does not exist.");
    //}

    //_reader = File.ReadLines();
  }
}
