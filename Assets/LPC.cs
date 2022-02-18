using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
//LaserPointerController to interact with UI Buttons with VR Controller
//https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5

public class LPC : MonoBehaviour
{
  public SteamVR_LaserPointer laserPointer;

  void Awake()
  {
    laserPointer.PointerIn += PointerInside;
    laserPointer.PointerOut += PointerOutside;
    laserPointer.PointerClick += PointerClick;
  }

  public void PointerClick(object sender, PointerEventArgs e)
  {
    e.target.gameObject.GetComponent<Button>().onClick.Invoke();
    if (e.target.name == "Cube")
    {
      Debug.Log("Cube was clicked");
    }
    else if (e.target.name.Contains("Button"))
    {
      Debug.Log("Button was clicked");
    }
  }

  public void PointerInside(object sender, PointerEventArgs e)
  {
    if (e.target.name == "Cube")
    {
      Debug.Log("Cube was entered");
    }
    else if (e.target.name.Contains("Button"))
    {
      Debug.Log("Button was entered");
    }
  }

  public void PointerOutside(object sender, PointerEventArgs e)
  {
    if (e.target.name == "Cube")
    {
      Debug.Log("Cube was exited");
    }
    else if (e.target.name.Contains("Button"))
    {
      Debug.Log("Button was exited");
    }
  }
}
