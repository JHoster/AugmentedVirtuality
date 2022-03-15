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
  private Transform xArrow;
  private Transform yArrow;
  private Transform zArrow;
  public bool zArrowFix; //Rotate z-Arrow to make it orthogonal
  public float zArrowRot = 90; //Angle z-Arrow
  private Transform PLA; //Point List Annotation
  private float depth;
  private float width;
  private float height;
  private GameObject SePA; //Second Point Annotation
  private GameObject TPA; //Third Point Annotation
  private GameObject FPA; //Fourth Point Annotation
  private GameObject SiPA; //Sixth Point Annotation
  //public GameObject LPA; //Last Point Annotation
  //Position
  public Transform MBRA; //MultiBoxRects Annotation
  //public Vector3 TL; //Top left
  private Vector3 TR; //Top right
  private Vector3 TRW; //Top right in world space
  private Vector3 BL; //Bottom left
  private Vector3 BLW; //Bottom left in world space
  //public Vector3 BR; //Bottom right
  public bool ray2World;
  public float chairDistance;
  public Vector3 chair3DPos;
  public float chairHeight;
  //Rotation
  private Vector3 view;
  private Vector3 side;
  private Vector3 up;
  public bool rotateWithPLA;
  private GameObject AL;
  public bool noXrot; //Set rotation around x-Axis to zero
  public bool noZrot; //Set rotation around z-Axis to zero
  //Fade
  public bool fade;
  public Component[] children;
  public float fadeUntil = 0;//0.1f; //Min alpha value in percent
  public Color colour;
  public Color colourOrg;
  private float timeToFade = 1.0f;
  //Disable Image
  public GameObject CP; //ContainerPanel
  private RawImage camImage;
  public bool disabledAnnotation;
  //Show cam or masked view
  private float resWidth;
  private float resHeight;
  public bool camView;
  public bool camMask;
  public Camera cam;
  private Rect boundingBox2D;
  private LineRenderer LR;
  private GameObject body;
  private RectMask2D mask;
  //public Vector2 TL2D;
  public Vector3 positionCenter;
  public Vector3 positionVP;
  public Vector3 positionScreen;
  private float width2D;
  private float height2D;
  public float extraWidth = 1;
  public float extraHeight = 1;
  public bool fadeMask;
  private int maskSoftnessOrgX;
  private int maskSoftnessOrgY;
  public float width2World;
  public float height2World;

  // Update is called once per frame
  void Update()
  {
    if (TA == null && GameObject.Find("Transform Annotation"))
      TA = GameObject.Find("Transform Annotation").transform;
    if (PLA == null && GameObject.Find("Point List Annotation"))
      PLA = GameObject.Find("Point List Annotation").transform;
    if (MBRA == null && GameObject.Find("MultiBoxRects Annotation"))
      MBRA = GameObject.Find("MultiBoxRects Annotation").transform;
    if (cam == null && GameObject.Find("Camera"))
      cam = GameObject.Find("Camera").GetComponent<Camera>();
    if (MBRA != null && body == null)
      body = MBRA.transform.parent.parent.parent.gameObject;
    if (body != null && mask == null)
    {
      mask = body.GetComponent<RectMask2D>();
      maskSoftnessOrgX = mask.softness.x;
      maskSoftnessOrgY = mask.softness.y;
    }

    if (CP == null && GameObject.Find("Container Panel")) //Disable UI images and make camImage transparent, so that Virtual Object can be seen
    {
      CP = GameObject.Find("Container Panel");
      CP.GetComponent<Image>().enabled = false;
      CP.transform.GetChild(0).GetComponent<Image>().enabled = false;
      camImage = CP.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>();
      camImage.color = new Color(camImage.color.r, camImage.color.g, camImage.color.b, 0.5f);
      resWidth = CP.transform.parent.GetComponent<CanvasScaler>().referenceResolution.x; //2436;
      resHeight = CP.transform.parent.GetComponent<CanvasScaler>().referenceResolution.y; //1125;
    }

    if (camImage != null)
    {
      if (camView)
      {
        camImage.enabled = true;
        if (camMask)
        {
          mask.enabled = true;
          if (!MBRA.gameObject.activeInHierarchy)
          {
            if (fadeMask)
            {
              float softX = Mathf.Lerp(mask.softness.x, 100000, Time.deltaTime / 10);
              float softY = Mathf.Lerp(mask.softness.y, 100000, Time.deltaTime / 10);
              mask.softness = new Vector2Int((int)softX, (int)softY);
            }
            else
              camImage.enabled = false;
          }
          else
          {
            if (fadeMask)
            {
              float softX = Mathf.Lerp(mask.softness.x, maskSoftnessOrgX, Time.deltaTime * 10);
              float softY = Mathf.Lerp(mask.softness.y, maskSoftnessOrgY, Time.deltaTime * 20);
              mask.softness = new Vector2Int((int)softX, (int)softY);
            }
          }
        }
        else
          mask.enabled = false;
      }
      else
      {
        camImage.enabled = false;
        mask.enabled = false;
      }
    }

    if (disabledAnnotation && PLA)
    {
      AL = PLA.transform.parent.transform.parent.transform.parent.gameObject;
      foreach (MeshRenderer mr in AL.GetComponentsInChildren<MeshRenderer>())
        mr.enabled = false;
      foreach (LineRenderer lr in AL.GetComponentsInChildren<LineRenderer>())
        lr.enabled = false;
    }
    else if (AL)
    {
      foreach (MeshRenderer mr in AL.GetComponentsInChildren<MeshRenderer>())
        mr.enabled = true;
      foreach (LineRenderer lr in AL.GetComponentsInChildren<LineRenderer>())
        lr.enabled = true;
    }

    //Create Camera View Mask around 2D Bounding Box:
    if (MBRA != null && MBRA.gameObject.activeInHierarchy && MBRA.transform.childCount > 0)
    {
      positionCenter = MBRA.GetChild(0).GetComponent<Renderer>().bounds.center;
      //Debug.Log(position2D);
      //Debug.Log(cam.WorldToScreenPoint(position2D));
      //Debug.Log(cam.WorldToViewportPoint(position2D));
      //Debug.DrawLine(Vector3.zero, position2D, Color.red);
      positionVP = cam.WorldToViewportPoint(positionCenter);
      positionScreen = cam.WorldToScreenPoint(positionCenter);

      //LR.GetPosition 0 = BL, 1 = TL, 2 = TR, 3 = BR
      LR = MBRA.GetChild(0).GetComponent<LineRenderer>();
      width2D = Mathf.Abs(LR.GetPosition(2).x - LR.GetPosition(1).x);
      //Debug.Log(width2D);
      height2D = Mathf.Abs(LR.GetPosition(1).y - LR.GetPosition(0).y);

      if (camMask)
      {
        //Debug.Log(height2D);
        //Debug.Log(new Vector2(width2D, height2D));
        //boundingBox2D.Set(LR.GetPosition(1).x, LR.GetPosition(1).y, width2D, height2D);

        ////Transform LR positions to screen-size (add offset of screen center)
        //Rect screenRect = body.transform.GetChild(0).GetComponent<RectTransform>().rect; //resolution changes sometimes?!
        //TL2D = new Vector2(0, 0);
        //if (LR.GetPosition(1).x > 0)
        //  TL2D.x = LR.GetPosition(1).x + screenRect.width / 2;
        //else
        //  TL2D.x = Mathf.Abs(LR.GetPosition(1).x) + screenRect.width / 2;
        //if (LR.GetPosition(1).y > 0)
        //  TL2D.y = LR.GetPosition(1).y + screenRect.height / 2;
        //else
        //  TL2D.y = Mathf.Abs(LR.GetPosition(1).y) + screenRect.height / 2;
        //Set padding
        //mask.padding = new Vector4(TL2D.x, 0, screenRect.width - (TL2D.x + boundingBox2D.width), TL2D.y);
        if (mask)
        {
          mask.enabled = true;
          float left = resWidth * positionVP.x - width2D / 2;// screenRect.width - (screenRect.width - (Position.x));
          float bottom = resHeight * positionVP.y - height2D / 2;//screenRect.height - (TL2D.y + height2D + extra);
          float right = resWidth * (1 - positionVP.x) - width2D / 2;//screenRect.width - (Position.x + width2D / 2);// screenRect.width - (Position.x + width2D / 2);
          float top = resHeight * (1 - positionVP.y) - height2D / 2;// TL2D.y;
          mask.padding = new Vector4(left - extraWidth, bottom - extraHeight, right - extraWidth, top - extraHeight); //Left, Bottom, Right, Top
          //mask.padding = new Vector4(TL2D.x + extra, screenRect.height - (TL2D.y + height2D + extra), screenRect.width - (TL2D.x + width2D + extra), TL2D.y + extra); //Left, Bottom, Right, Top
        }

        //boundBox2D[4] = Vector3.zero;
        //for (int i = 0; i < 4; i++)
        //  boundBox2D[i] = MBRA.GetChild(0).GetComponent<LineRenderer>().GetPosition(i);
      }
    }

    if (fade && children.Length > 0)
    {
      if (children.Length > 0)
        foreach (MeshRenderer MR in children)
          if (MR.materials.Length == 1)
            MR.material.color = new Color(MR.material.color.r, MR.material.color.g, MR.material.color.b, colour.a);
          else
          {
            foreach (Material mat in MR.materials)
              mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, colour.a);
          }
    }

    if (ray2World && cam.transform.position.y >= positionCenter.y)
    {
      //chairDistance gets negative when camera is lower than chairHeight/2!
      Vector3 dir = positionCenter - cam.transform.position;//TA.position - cam.transform.position;
      Ray ray = new Ray(cam.transform.position, dir);
      Plane chairPlane = new Plane(Vector3.up, new Vector3(0, chairHeight / 2, 0));
      //Debug.DrawLine(cam.transform.position, TA.position, Color.white);
      //Debug.DrawLine(cam.transform.position, cam.ScreenToWorldPoint(new Vector3(positionScreen.x, positionScreen.y, zDistance)), Color.white);
      //Debug.DrawLine(cam.transform.position, cam.ScreenToWorldPoint(new Vector3(positionScreen.x, positionScreen.y, zDistance), Camera.MonoOrStereoscopicEye.Right), Color.white);
      if (chairPlane.Raycast(ray, out chairDistance))
      {
        chair3DPos = ray.GetPoint(chairDistance);
        Debug.DrawLine(cam.transform.position, chair3DPos, Color.red);
        Vector3 camGrounded = new Vector3(cam.transform.position.x, chairHeight / 2, cam.transform.position.z);
        //Vector3 dirGrounded = chairDistance - camGrounded;
        //Ray rayGrounded = new Ray(camGrounded, dirGrounded);
        Debug.DrawLine(camGrounded, chair3DPos, Color.blue);
        CP.transform.parent.gameObject.GetComponent<Canvas>().planeDistance = Vector3.Distance(camGrounded, chair3DPos);
      }
    }
    else
      CP.transform.parent.gameObject.GetComponent<Canvas>().planeDistance = 1;

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
        //Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, transform.position + up, 0.5f));
        //Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, zAxisDir, 0.5f));
        //Debug.DrawLine(SiPA.transform.position, Vector3.Lerp(SiPA.transform.position, FPA.transform.position, 0.5f));
        //Debug.DrawLine(transform.position, Vector3.Lerp(transform.position, zAxisDir, 0.5f));
        //Vector3 TFPA = 1 / 2 * (TPA.transform.position - FPA.transform.position); //Vector pointing to middle between Third and Fourth PA
        //Vector3 SeTFPA = 1 / 2 * (SePA.transform.position - TPA.transform.position); //Vector pointing to middle between Second and Third PA
        //Debug.DrawLine(SePA.transform.position, TPA.transform.position);
      }
      else
      {
        //Rotation with TA:
        //xArrow = TA.transform.GetChild(0).GetChild(0);
        yArrow = TA.transform.GetChild(1).GetChild(0);
        zArrow = TA.transform.GetChild(2).GetChild(0);
        view = zArrow.transform.position - TA.position;
        //side = xArrow.transform.position - TA.position;
        up = yArrow.transform.position - TA.position;
        if (noXrot) //works good if cam view is parallel to ground
        {
          //Vielleicht y festsetzen und nur x und z rotieren lassen? Objekte werden eh nur aufrecht erkannt.
          //Stimmt, aber funktioniert nicht, sobald die Kamera geneigt wird
          Quaternion lookZ = Quaternion.LookRotation(view, up);
          //Debug.Log(Vector3.Angle(transform.up, up));
          //float AngleY = Vector3.Angle(transform.up, up); //Angle between objects y-Axis and Bounding Box y-Axis
          //Debug.Log(look);
          //Debug.Log(look.eulerAngles);
          //transform.rotation = Quaternion.Euler(look.eulerAngles.x, 0, look.eulerAngles.z);
          //Debug.Log(lookZ.eulerAngles);
          if (noZrot)
            transform.rotation = Quaternion.Euler(0, lookZ.eulerAngles.y, 0); //Only rotation around y-Axis
          else
            transform.rotation = Quaternion.Euler(0, lookZ.eulerAngles.y, lookZ.eulerAngles.z); //Only rotation around y-Axis and z-Axis
          //transform.rotation = Quaternion.Euler(AngleY, lookZ.eulerAngles.y, lookZ.eulerAngles.z);
          //transform.LookAt(new Vector3(zArrow.transform.position.x,zArrow.transform.position.y, zArrow.transform.position.z));
          //Align x-Axis? //add angle between Object x-Axis and Bounding box x-Axis to rotation around y-Axis
        }
        else
        {
          if (!zArrowFix)
            zArrow.transform.parent.transform.eulerAngles = Vector3.zero;
          else
          {
            // Vector3.Angle(view, up) + zArrowRot = 90
            //zArrowRot = 90 - Vector3.Angle(view, TA.position + up);
            //Debug.Log(Vector3.Angle(view, TA.position + up));
            //Debug.DrawLine(TA.position, TA.position + view, colour = Color.blue);
            //Debug.DrawLine(TA.position, TA.position + up, colour = Color.green);
            zArrow.transform.parent.transform.eulerAngles = new Vector3(zArrowRot, 0, 0);
            //Debug.Log(zArrowRot + Vector3.Angle(view, transform.position + up));
            //Debug.Log(Vector3.Angle(view, TA.position + up)); // should be 90°
            //Debug.Log(Vector3.Angle(view, TA.position + up)); // should be 90°
            //Debug.Log(Vector3.Dot(view, transform.position + up)); // should be 0

          }
          //transform.rotation = Quaternion.FromToRotation(view, up);
          if (smoothRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(view, up), Time.deltaTime * smoothFactor);
          else
            transform.rotation = Quaternion.LookRotation(view, up);
          //transform.LookAt(zArrow); //or yArrow for chlild object
        }
      }

      //ToDO:
      //Improve rotation (Works when VR Headset is calibrated (knows where floor is) so only rotation around the y-Axis in x-z.plane.
      //Make virtual Object stay in 2D Bounding box, if no 3D Bounding box available
      //Show where virtual Object was last detected in world space (so that cup can be placed on desk again)
      //Create VOs for multiple bounding boxes
      //Done:
      //Make smooth translation from current shape to next 3D Bounding box
      //Hide or fade out virtual Object when there is no real object detected

      if (fade)
        colour = colourOrg;
    }
    else if (MBRA && MBRA.gameObject.activeInHierarchy) //Use 2D Bounding Box for positioning
    {
      if (smoothTransform)
        transform.position = Vector3.Lerp(transform.position, positionCenter, Time.deltaTime * smoothFactor * 2);
      else
        transform.position = positionCenter;

      //Scaling with 2D BB:
      //if (MBRA != null && MBRA.gameObject.activeInHierarchy && MBRA.transform.childCount > 0)
      //{
      ////LR.GetPosition 0 = BL, 1 = TL, 2 = TR, 3 = BR
      ////Debug.Log(LR.GetPosition(2));
      //////Debug.Log(cam.ScreenToViewportPoint(LR.GetPosition(2)));
      ////Debug.LogWarning(cam.ScreenToWorldPoint(new Vector3(LR.GetPosition(2).x, LR.GetPosition(2).y, 1)));
      //Vector3 BLW = cam.ScreenToWorldPoint(new Vector3(LR.GetPosition(0).x, LR.GetPosition(0).y, 1));
      //Vector3 TLW = cam.ScreenToWorldPoint(new Vector3(LR.GetPosition(1).x, LR.GetPosition(1).y, 1));
      //Vector3 TRW = cam.ScreenToWorldPoint(new Vector3(LR.GetPosition(2).x, LR.GetPosition(2).y, 1));
      //Debug.DrawLine(TRW, TLW, Color.red);
      //Debug.DrawLine(TLW, BLW, Color.blue);
      //width2World = Mathf.Abs(TRW.x - TLW.x);
      ////Debug.Log(width2D);
      //height2World = Mathf.Abs(TLW.y - BLW.y);

      //Vector3 movedPosX = new Vector3(positionScreen.x + width2D, positionScreen.y, positionScreen.z);
      //Vector3 movedPosY = new Vector3(positionScreen.x, positionScreen.y + height2D, positionScreen.z);
      //Debug.DrawLine(cam.ScreenToWorldPoint(positionScreen), cam.ScreenToWorldPoint(movedPosX), colour = Color.blue);
      //Debug.DrawLine(cam.ScreenToWorldPoint(positionScreen), cam.ScreenToWorldPoint(movedPosY), colour = Color.red);
      //float width2World = Vector3.Distance(cam.ScreenToWorldPoint(positionScreen), cam.ScreenToWorldPoint(movedPosX));
      //float height2World = Vector3.Distance(cam.ScreenToWorldPoint(positionScreen), cam.ScreenToWorldPoint(movedPosY));
      //transform.localScale = new Vector3(width2World, height2World, 1);
      //}

      // 2D Bounding Box position already done above. This was the old approach:
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
    }
    else if (fade && colour.a > fadeUntil)
    {
      colour.a -= Time.deltaTime / timeToFade;
    }
  }
}
