using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

namespace Mediapipe.Unity
{
  public class SteamVRCam : ImageSource
  {
    public Material material;
    public Transform target;
    public SteamVR_TrackedCamera.VideoStreamTexture source;
    public Texture2D source2;
    public WebCamTexture source3;
    public bool undistorted = true;
    public bool cropped = true;

    //private void OnEnable()
    //{
    //  // The video stream must be symmetrically acquired and released in
    //  // order to properly disable the stream once there are no consumers.
    //  source = SteamVR_TrackedCamera.Source(undistorted);
    //  source.Acquire();

    //  // Auto-disable if no camera is present.
    //  if (!source.hasCamera)
    //    enabled = false;
    //}

    //private void OnDisable()
    //{
    //  // Clear the texture when no longer active.
    //  material.mainTexture = null;

    //  // The video stream must be symmetrically acquired and released in
    //  // order to properly disable the stream once there are no consumers.
    //  source = SteamVR_TrackedCamera.Source(undistorted);
    //  source.Release();
    //}

    //private void Update()
    //{
    //  if(GameObject.Find("TrackedCamera"))
    //    target = GameObject.Find("TrackedCamera").transform;
    //  if (source == null)
    //  {
    //    // The video stream must be symmetrically acquired and released in
    //    // order to properly disable the stream once there are no consumers.
    //    source = SteamVR_TrackedCamera.Source(undistorted);
    //    source.Acquire();

    //    // Auto-disable if no camera is present.
    //    if (!source.hasCamera)
    //      enabled = false;
    //  }

    //  source = SteamVR_TrackedCamera.Source(undistorted);
    //  Texture2D texture = source.texture;

    //  //source2 = source.texture;
    //  ////source2 = myTexture2D;
    //  //Debug.LogWarning(source2.isReadable);
    //  //_ = source2.GetPixels32();
    //  //Debug.LogWarning("Type:" + source.GetType());
    //  ////Debug.LogWarning("Type:" + source2.GetType());

    //  if (texture == null)
    //  {
    //    return;
    //  }

    //  // Apply the latest texture to the material.  This must be performed
    //  // every frame since the underlying texture is actually part of a ring
    //  // buffer which is updated in lock-step with its associated pose.
    //  // (You actually really only need to call any of the accessors which
    //  // internally call Update on the SteamVR_TrackedCamera.VideoStreamTexture).
    //  material.mainTexture = texture;

    //  // Adjust the height of the quad based on the aspect to keep the texels square.
    //  float aspect = (float)texture.width / texture.height;

    //  // The undistorted video feed has 'bad' areas near the edges where the original
    //  // square texture feed is stretched to undo the fisheye from the lens.
    //  // Therefore, you'll want to crop it to the specified frameBounds to remove this.
    //  if (cropped)
    //  {
    //    VRTextureBounds_t bounds = source.frameBounds;
    //    material.mainTextureOffset = new Vector2(bounds.uMin, bounds.vMin);

    //    float du = bounds.uMax - bounds.uMin;
    //    float dv = bounds.vMax - bounds.vMin;
    //    material.mainTextureScale = new Vector2(du, dv);

    //    aspect *= Mathf.Abs(du / dv);
    //  }
    //  else
    //  {
    //    material.mainTextureOffset = Vector2.zero;
    //    material.mainTextureScale = new Vector2(1, -1);
    //  }
    //  if(target)
    //    target.localScale = new Vector3(1, 1.0f / aspect, 1);

    //  // Apply the pose that this frame was recorded at.
    //  if (source.hasTracking && target)
    //  {
    //    SteamVR_Utils.RigidTransform rigidTransform = source.transform;
    //    target.localPosition = rigidTransform.pos;
    //    target.localRotation = rigidTransform.rot;
    //  }
    //}

    public override SourceType type => SourceType.SteamVR;

    public override string sourceName => "SteamVRsourceName";// source?.ToString();

    public override string[] sourceCandidateNames => throw new System.NotImplementedException();

    public override ResolutionStruct[] availableResolutions => throw new System.NotImplementedException();

    public override bool isPrepared => true;//source != null; //throw new System.NotImplementedException();

    public override bool isPlaying => true;//source != null; //throw new System.NotImplementedException();

    public override Texture GetCurrentTexture()
    {
      // The video stream must be symmetrically acquired and released in
      // order to properly disable the stream once there are no consumers.
      source = SteamVR_TrackedCamera.Source(undistorted);
      source.Acquire();
      Texture2D texture = source.texture;

      //Fix texture
      RenderTexture tmp = RenderTexture.GetTemporary(
                    texture.width,
                    texture.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);
      // Blit the pixels on texture to the RenderTexture
      Graphics.Blit(texture, tmp);
      // Backup the currently set RenderTexture
      RenderTexture previous = RenderTexture.active;
      // Set the current RenderTexture to the temporary one we created
      RenderTexture.active = tmp;
      // Create a new readable Texture2D to copy the pixels to it
      Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
      // Copy the pixels from the RenderTexture to the new Texture
      //myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
      myTexture2D.Apply();
      // Reset the active RenderTexture
      RenderTexture.active = previous;
      // Release the temporary RenderTexture
      RenderTexture.ReleaseTemporary(tmp);

      return myTexture2D;
    }

    public override void Pause()
    {
      throw new System.NotImplementedException();
    }

    public override IEnumerator Play()
    {
      //if (source == null)
      //{
      //  throw new InvalidOperationException("Video is not selected");
      //}

      yield return true;//source.hasCamera;
      //throw new System.NotImplementedException();
    }

    public override IEnumerator Resume()
    {
      throw new System.NotImplementedException();
    }

    public override void SelectSource(int sourceId)
    {
      throw new System.NotImplementedException();
    }

    public override void Stop()
    {
      throw new System.NotImplementedException();
    }
  }
}
