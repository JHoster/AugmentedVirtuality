using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObject : MonoBehaviour
{
  //public Transform CenterPosition;
  public Transform TA;
  public Transform xArrow;
  public Transform yArrow;
  public Transform zArrow;

  // Start is called before the first frame update
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    TA = GameObject.Find("Transform Annotation").transform;

    if (TA)
    {
      xArrow = TA.transform.GetChild(0).GetChild(0);
      yArrow = TA.transform.GetChild(1).GetChild(0);
      zArrow = TA.transform.GetChild(2).GetChild(0);
      transform.position = TA.position;
      transform.LookAt(zArrow); //or yArrow for chlild object
      //transform.localScale = new Vector3(xArrow.transform.position.x - transform.position.x, yArrow.transform.position.y - transform.position.y, zArrow.transform.position.z - transform.position.z);
    }
  }
}
