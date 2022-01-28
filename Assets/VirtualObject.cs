using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mediapipe.Unity;

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
  public Vector3 side;
  public Vector3 up;
  public bool rotateWithPLA;
  public GameObject AL;
  public bool freezeY; //Fix y-Axis for rotation
  //Fade
  public bool fade;
  private Component[] children;
  public float fadeUntil = 0.1f; //Min alpha value in percent
  private Color colour;
  private Color colourOrg;
  public float timeToFade = 1.0f;
  //Disable Image
  public GameObject CP; //ContainerPanel
  private RawImage camImage;
  private bool disabledUI;

  // Start is called before the first frame update
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }
  void Start()
  {
    children = gameObject.transform.GetComponentsInChildren<MeshRenderer>();
    colour = gameObject.transform.GetComponentInChildren<MeshRenderer>().material.color;
    colourOrg = colour;
  }

  // Update is called once per frame
  void Update()
  {
    if (!disabledUI && GameObject.Find("Container Panel")) //Disable UI images and make camImage transparent, so that Virtual Object can be seen
    {
      disabledUI = true;
      CP = GameObject.Find("Container Panel");
      CP.GetComponent<Image>().enabled = false;
      CP.transform.GetChild(0).GetComponent<Image>().enabled = false;
      camImage = CP.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>();
      camImage.color = new Color(camImage.color.r, camImage.color.g, camImage.color.b, 0.5f);
    }

    if (GameObject.Find("Transform Annotation"))
      TA = GameObject.Find("Transform Annotation").transform;
    if(GameObject.Find("Point List Annotation"))
      PLA = GameObject.Find("Point List Annotation").transform;
    if (fade)
      foreach(MeshRenderer MR in children)
        MR.material.color = colour;

    if (PLA && PLA.gameObject.activeInHierarchy && TA && TA.gameObject.activeInHierarchy)
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
      ////transform.localScale = new Vector3(xArrow.transform.position.x - transform.position.x, yArrow.transform.position.y - transform.position.y, zArrow.transform.position.z - transform.position.z);

      if (rotateWithPLA) //Rotation calculated with points of PLA, else TA is used.
      {
        AL = PLA.transform.parent.transform.parent.transform.parent.gameObject;
        //AL.GetComponent<FrameAnnotationController>()._scaleZ = 3000; //change _scaleZ to public float to make this work or do it manually
        Vector3 xDirection = TPA.transform.position - SePA.transform.position;
        Vector3 yDirection = FPA.transform.position - SePA.transform.position;
        Vector3 zDirection = SiPA.transform.position - SePA.transform.position;
        //Vector3 view = zDirection - transform.position;
        //transform.rotation = Quaternion.LookRotation(view);
        //transform.LookAt(view);
        Vector3 zAxisDir = Vector3.Lerp(SiPA.transform.position, FPA.transform.position, 0.5f); //Middle point between SiPA and FPA in which zAxis should point if bounding box is 3D
        Vector3 yAxisDir = Vector3.Lerp(TPA.transform.position, SiPA.transform.position, 0.5f);
        Vector3 view = zAxisDir - transform.position;
        Vector3 up = -(yAxisDir - transform.position);
        transform.rotation = Quaternion.LookRotation(view, up);
        Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, transform.position + up, 0.5f));
        Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, zAxisDir, 0.5f));
          //Debug.DrawLine(SiPA.transform.position, Vector3.Lerp(SiPA.transform.position, FPA.transform.position, 0.5f));
          //Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, zAxisDir, 0.5f));
          //Vector3 TFPA = 1 / 2 * (TPA.transform.position - FPA.transform.position); //Vector pointing to middle between Third and Fourth PA
          //Vector3 SeTFPA = 1 / 2 * (SePA.transform.position - TPA.transform.position); //Vector pointing to middle between Second and Third PA
          //Debug.DrawLine(SePA.transform.position, TPA.transform.position);
      }
      else
      {
        //Rotation with TA:
        xArrow = TA.transform.GetChild(0).GetChild(0);
        yArrow = TA.transform.GetChild(1).GetChild(0);
        zArrow = TA.transform.GetChild(2).GetChild(0);
        view = zArrow.transform.position - transform.position;
        side = xArrow.transform.position - transform.position;
        up = yArrow.transform.position - transform.position;
        if (freezeY) //works good if cam view is parallel to ground
        {
          //Vielleicht y festsetzen und nur x und z rotieren lassen? Objekte werden eh nur aufrecht erkannt.
          //Stimmt, aber funktioniert nicht, sobald die Kamera geneigt wird
          Quaternion look = Quaternion.LookRotation(view, up);
          Debug.Log(look);
          Debug.Log(look.eulerAngles);
          //transform.rotation = Quaternion.Euler(look.eulerAngles.x, 0, look.eulerAngles.z);
          transform.rotation = Quaternion.Euler(0, look.eulerAngles.y, 0);
          //Align x-Axis? //add angle between Object x-Axis and Bounding box x-Axis to rotation around y-Axis
        }
        else
        {
          //transform.rotation = Quaternion.FromToRotation(view, up);
          transform.rotation = Quaternion.LookRotation(view, up);
          //transform.LookAt(zArrow); //or yArrow for chlild object
        }
      }

      //ToDO:
      //Improve rotation
      //Make translation from current shape to next 3D Bounding box
      //Make virtual Object stay in 2D Bounding box, if no 3D Bounding box available
      //Done:
      //Hide or fade out virtual Object when there is no real object detected
      //Show where virtual Object was last detected (so that cup can be placed on desk again)

      if (fade)
        colour = colourOrg;
    }
    else if(fade && colour.a > fadeUntil)
    {
      colour.a -= Time.deltaTime/timeToFade;
    }
  }
}
