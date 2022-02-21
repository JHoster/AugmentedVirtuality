using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;

public class SVRC2MPO : MonoBehaviour
{
  public GameObject SVRCMaterialObj;
  public Material SVRCMaterial;
  public GameObject MPOTextureObj;
  public Texture MPOTexture;

  public bool undistorted = true;

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    SteamVR_TrackedCamera.VideoStreamTexture source = SteamVR_TrackedCamera.Source(undistorted);
    Texture2D texture = source.texture;
    if (texture == null)
    {
      return;
    }

    SVRCMaterial = SVRCMaterialObj.GetComponent<SteamVR_TestTrackedCamera>().material;
    //MPOTexture = MPOTextureObj.GetComponent<RawImage>().texture;
    MPOTexture = texture;
    MPOTextureObj.GetComponent<RawImage>().texture = texture;
  }
}
