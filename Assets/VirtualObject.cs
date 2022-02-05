using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mediapipe.Unity;

public class VirtualObject : MonoBehaviour
{
  //public Transform CenterPosition;
  public Transform TA; //Transform Annotation
  public bool smoothTransform;
  public bool smoothScaling;
  public bool smoothRotation;
  public float smoothFactor = 2;
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
  //Position
  public Transform MBRA; //MultiBoxRects Annotation
  //public Vector3 TL; //Top left
  public Vector3 TR; //Top right
  public Vector3 TRW; //Top right in world space
  public Vector3 BL; //Bottom left
  public Vector3 BLW; //Bottom left in world space
  //public Vector3 BR; //Bottom right
  //Rotation
  public Vector3 view;
  public Vector3 side;
  public Vector3 up;
  public bool rotateWithPLA;
  public GameObject AL;
  public bool freezeY; //Fix y-Axis for rotation
  //Fade
  public bool fade;
  public Component[] children;
  public float fadeUntil = 0;//0.1f; //Min alpha value in percent
  public Color colour;
  public Color colourOrg;
  public float timeToFade = 1.0f;
  //Disable Image
  public GameObject CP; //ContainerPanel
  private RawImage camImage;
  private bool disabledUI;

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
    if (GameObject.Find("Point List Annotation"))
      PLA = GameObject.Find("Point List Annotation").transform;
    if (GameObject.Find("MultiBoxRects Annotation"))
      MBRA = GameObject.Find("MultiBoxRects Annotation").transform;
    if (fade && children.Length > 0)
    {
      if (children.Length > 0)
        foreach (MeshRenderer MR in children)
          if(MR.materials.Length == 1)
            MR.material.color = new Color(MR.material.color.r, MR.material.color.g, MR.material.color.b, colour.a);
          else
          {
            foreach (Material mat in MR.materials)
              mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, colour.a); ;
          }
    }


    if (PLA && PLA.gameObject.activeInHierarchy && TA && TA.gameObject.activeInHierarchy)
    {
      //Position:
      if (smoothTransform)
        transform.position = Vector3.Lerp(transform.position, TA.position, Time.deltaTime * smoothFactor);
      else
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
      if (smoothScaling)
        transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(width, height, depth), Time.deltaTime * smoothFactor);
      else
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
          Quaternion lookZ = Quaternion.LookRotation(view, up);
          Debug.Log(Vector3.Angle(transform.up, up));
          float AngleY = Vector3.Angle(transform.up, up); //Angle between objects y-Axis and Bounding Box y-Axis
          //Debug.Log(look);
          //Debug.Log(look.eulerAngles);
          //transform.rotation = Quaternion.Euler(look.eulerAngles.x, 0, look.eulerAngles.z);
          //Debug.Log(lookZ.eulerAngles);
          transform.rotation = Quaternion.Euler(0, lookZ.eulerAngles.y, 0); //Only rotation around y-Axis
          //transform.rotation = Quaternion.Euler(0, lookZ.eulerAngles.y, lookZ.eulerAngles.z); //Only rotation around y-Axis and z-Axis
          //transform.rotation = Quaternion.Euler(AngleY, lookZ.eulerAngles.y, lookZ.eulerAngles.z);
          //transform.LookAt(new Vector3(zArrow.transform.position.x,zArrow.transform.position.y, zArrow.transform.position.z));
          //Align x-Axis? //add angle between Object x-Axis and Bounding box x-Axis to rotation around y-Axis
        }
        else
        {
          //transform.rotation = Quaternion.FromToRotation(view, up);
          if (smoothRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(view, up), Time.deltaTime * smoothFactor);
          else
            transform.rotation = Quaternion.LookRotation(view, up);
          //transform.LookAt(zArrow); //or yArrow for chlild object
        }
      }

      //ToDO:
      //Improve rotation
      //Make virtual Object stay in 2D Bounding box, if no 3D Bounding box available
      //Show where virtual Object was last detected in world space (so that cup can be placed on desk again)
      //Done:
      //Make smooth translation from current shape to next 3D Bounding box
      //Hide or fade out virtual Object when there is no real object detected

      if (fade)
        colour = colourOrg;
    }
    //else if (MBRA && MBRA.gameObject.activeInHierarchy) //Use 2D Bounding Box for positioning
    //{
    //  //Debug.Log(MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(0));
    //  BL = MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(0);
    //  BLW = Camera.main.ScreenToWorldPoint(new Vector3(2 * BL.x, transform.TransformPoint(MBRA.transform.position).y + 2 * BL.y, transform.TransformPoint(MBRA.transform.position).z));
    //  //BLW = transform.TransformPoint(MBRA.transform.GetChild(0).transform.position) + Camera.main.ScreenToWorldPoint(new Vector3(2 * BL.x, 2 * BL.y, 1));
    //  //Debug.Log(transform.TransformPoint(MBRA.transform.position + Camera.main.ScreenToWorldPoint(BL)));
    //  //BL = transform.TransformPoint(BL);
    //  //TL = MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(1);
    //  TR = MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(2);
    //  TRW = Camera.main.ScreenToWorldPoint(new Vector3(2 * TR.x, transform.TransformPoint(MBRA.transform.position).y + 2 * TR.y, transform.TransformPoint(MBRA.transform.position).z));

    //  Debug.DrawLine(BL, TR, colour = Color.blue);
    //  Debug.DrawLine(BLW, Vector3.Lerp(BLW,TRW,0.75f), colour = Color.red);

    //  //TR = transform.TransformPoint(TR);
    //  //BR = MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(3);
    //  //Debug.Log(Vector3.Lerp(BL, TR, 0.5f));
    //  //Debug.Log(Vector3.Lerp(BR, TL, 0.5f));
    //  //Debug.Log(Vector3.Lerp(BR, TL, 0.5f) == Vector3.Lerp(BL, TR, 0.5f));
    //  //Debug.DrawLine(Vector3.zero, Vector3.Lerp(BL, TR, 0.5f));
    //  //transform.position = Vector3.Lerp(BL, TR, 0.5f);
    //  //Debug.Log(Camera.main.ScreenToWorldPoint(Vector3.Lerp(BL, TR, 0.5f)));
    //}
    else if (fade && colour.a > fadeUntil)
    {
      colour.a -= Time.deltaTime / timeToFade;
    }
  }
}
