using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Access camera directly and display as texture

public class CamTexture : MonoBehaviour
{
  public RawImage rawimage;
  void Start()
  {
    WebCamTexture webcamTexture = new WebCamTexture();
    rawimage.texture = webcamTexture;
    rawimage.material.mainTexture = webcamTexture;
    webcamTexture.Play();
  }
}
