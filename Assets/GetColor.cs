using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColor : MonoBehaviour
{
  public Color colour;
  public Color colourOrg;
  public Component[] children;

  void OnEnable()
  {
    children = gameObject.transform.GetComponentsInChildren<MeshRenderer>();
    transform.parent.GetComponent<VirtualObject>().children = children;
    if (gameObject.GetComponent<MeshRenderer>())
      colour = gameObject.GetComponent<MeshRenderer>().material.color;
    else
      colour = children[0].GetComponentInChildren<MeshRenderer>().material.color;
    colourOrg = colour;
    colourOrg.a = 1;
    transform.parent.GetComponent<VirtualObject>().colour = colour;
    transform.parent.GetComponent<VirtualObject>().colourOrg = colourOrg;
  }
}
