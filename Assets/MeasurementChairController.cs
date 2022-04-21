using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Measurement : MonoBehaviour
{
  public GameObject ControllerObj;
  public GameObject VirtualChairObj;
  //public Vector3 absoluteValue;
  //public float distance;
  //public float distance2;
  public string fileName = "DM"; //DistanceMeasurement
  private StreamWriter _writer;
  //private bool start;

  void Start()
  {
    VirtualChairObj = gameObject;
    fileName += ".txt";
    if (File.Exists(fileName))
    {
      Debug.Log(fileName + " already exists.");
      enabled = false;
    }
    _writer = File.CreateText(fileName);
    _writer.WriteLine("XPosCon;YPosCon;ZPosCon;XRotCon;YRotCon;ZRotCon;XPosChair;YPosChair;ZPosChair;XRotChair;YRotChair;ZRotChair;XScaleChair;YScaleChair;ZScaleChair;StatusBB");
    //_writer.Close();
  }

  // Update is called once per frame
  void Update()
  {
    if (!ControllerObj & GameObject.Find("Controller"))
      ControllerObj = GameObject.Find("Controller");

    Vector3 Controller = ControllerObj.transform.position;
    Vector3 ControllerRot = ControllerObj.transform.rotation.eulerAngles;
    Vector3 VirtualChair = VirtualChairObj.transform.position;
    Vector3 VirtualChairRot = VirtualChairObj.transform.rotation.eulerAngles;
    Vector3 VirtualChairScale = VirtualChairObj.transform.localScale;
    //absoluteValue = new Vector3(Mathf.Abs(Controller.x - VirtualChair.x), Mathf.Abs(Controller.y - VirtualChair.y), Mathf.Abs(Controller.z - VirtualChair.z));
    //distance = Vector3.Distance(Controller, VirtualChair);
    //distance2 = Mathf.Sqrt(absoluteValue.x * absoluteValue.x + absoluteValue.y * absoluteValue.y + absoluteValue.z * absoluteValue.z);
    //if (Mathf.Approximately(distance, distance2))
    //  Debug.LogError("False");
    //else
    //WriteString(Controller, VirtualChair);

    //Start measuring after first 3D BB is detected?
    //if (gameObject.GetComponent<VirtualObject>().status == "3DBB")
    //  start = true;
    //if(start)
    //  _writer.WriteLine(Controller.x + ";" + Controller.y + ";" + Controller.z + ";" + ControllerRot.x + ";" + ControllerRot.y + ";" + ControllerRot.z + ";" + VirtualChair.x + ";" + VirtualChair.y + ";" + VirtualChair.z + ";" + VirtualChairRot.x + ";" + VirtualChairRot.y + ";" + VirtualChairRot.z + ";" + VirtualChairScale.x + ";" + VirtualChairScale.y + ";" + VirtualChairScale.z + ";" + gameObject.GetComponent<VirtualObject>().status);
    
    //Start measuring after VO moved from initial position:
    if(gameObject.transform.position != Vector3.zero)
      _writer.WriteLine(Controller.x + ";" + Controller.y + ";" + Controller.z + ";" + ControllerRot.x + ";" + ControllerRot.y + ";" + ControllerRot.z + ";" + VirtualChair.x + ";" + VirtualChair.y + ";" + VirtualChair.z + ";" + VirtualChairRot.x + ";" + VirtualChairRot.y + ";" + VirtualChairRot.z + ";" + VirtualChairScale.x + ";" + VirtualChairScale.y + ";" + VirtualChairScale.z + ";" + gameObject.GetComponent<VirtualObject>().status);
    
    //Start measuring directly:
    //_writer.WriteLine(Controller.x + ";" + Controller.y + ";" + Controller.z + ";" + ControllerRot.x + ";" + ControllerRot.y + ";" + ControllerRot.z + ";" + VirtualChair.x + ";" + VirtualChair.y + ";" + VirtualChair.z + ";" + VirtualChairRot.x + ";" + VirtualChairRot.y + ";" + VirtualChairRot.z + ";" + VirtualChairScale.x + ";" + VirtualChairScale.y + ";" + VirtualChairScale.z + ";" + gameObject.GetComponent<VirtualObject>().status);
  }

  private void OnDisable()
  {
    _writer.Close();
  }
}
