using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObject : MonoBehaviour
{
  //public Transform CenterPosition;
  public Transform TA; //Transform Annotation
  public Transform xArrow;
  public Transform yArrow;
  public Transform zArrow;
  public Transform PLA; //Point List Annotation
  private float depth;
  private float width;
  private float height;
  public GameObject SePA; //Second Point Annotation
  public GameObject TPA; //Third Point Annotation
  public GameObject FPA; //Fourth Point Annotation
  public GameObject SiPA; //Sixth Point Annotation
  //public GameObject LPA; //Last Point Annotation
  public Vector3 view;
  public Vector3 up;

  // Start is called before the first frame update
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    TA = GameObject.Find("Transform Annotation").transform;
    PLA = GameObject.Find("Point List Annotation").transform;

    if (TA)
    {
      //Position:
      transform.position = TA.position;
      //Scale:
      SePA = PLA.transform.GetChild(1).gameObject;
      TPA = PLA.transform.GetChild(2).gameObject;
      FPA = PLA.transform.GetChild(3).gameObject;
      SiPA = PLA.transform.GetChild(5).gameObject;
      //LPA = PLA.transform.GetChild(8).gameObject;
      //scale = SePA.transform.position - LPA.transform.position; //Simple scaling appraoch: Does not work.
      depth = Vector3.Distance(SePA.transform.position, TPA.transform.position);
      height = Vector3.Distance(SePA.transform.position, FPA.transform.position);
      width = Vector3.Distance(SePA.transform.position, SiPA.transform.position);
      transform.localScale = new Vector3(width, height, depth);
      //Old scaling approach:
      //transform.localScale = new Vector3(xArrow.transform.position.x - transform.position.x, yArrow.transform.position.y - transform.position.y, zArrow.transform.position.z - transform.position.z);
      //Rotation:
      xArrow = TA.transform.GetChild(0).GetChild(0);
      yArrow = TA.transform.GetChild(1).GetChild(0);
      zArrow = TA.transform.GetChild(2).GetChild(0);
      view = zArrow.transform.position - transform.position;
      up = yArrow.transform.position - transform.position;
      //transform.rotation = Quaternion.FromToRotation(view, up);
      //transform.rotation = Quaternion.LookRotation(view, up);
      transform.LookAt(yArrow); //or yArrow for chlild object

      //ToDO:
      //Make translation from current shape to next 3D Bounding box
      //Hide or fade out virtual Object when there is no real object detected
      //Show where virtual Object was last detected (so that cup can be placed on desk again)
    }
  }
}
