using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementInstantNGPCams : MonoBehaviour
{
  public GameObject camsColmapGO;
  public List<GameObject> camsColmap = new List<GameObject>();
  public GameObject camsHMDGO;
  public List<GameObject> camsHMD = new List<GameObject>();
  public List<Vector3> dist = new List<Vector3>();
  public List<Vector3> rot = new List<Vector3>();
  public Vector3 distMAE;
  public float distMAE_f;
  public Vector3 rotMAE;
  public float rotMAE_f;

  public void MINGPC()
  {
    if (camsColmap.Count != camsHMD.Count)
    {
      Debug.LogError("Lists of cams is not the same size!");
      return;
    }

    distMAE = Vector3.zero;
    rotMAE = Vector3.zero;
    dist.Clear();
    rot.Clear();
    camsColmap = camsColmapGO.GetComponent<ReadInstantNGPtransforms>().cams;
    camsHMD = camsHMDGO.GetComponent<ReadInstantNGPtransforms>().cams;

    for (int i = 0; i < camsHMD.Count; i++)
    {
      //float distance = Vector3.Distance(camsHMD[i].transform.position, camsColmap[i].transform.position);
      Vector3 rotation = camsHMD[i].transform.rotation.eulerAngles - camsColmap[i].transform.rotation.eulerAngles;
      rotation = new Vector3(Mathf.Abs(rotation.x), Mathf.Abs(rotation.y), Mathf.Abs(rotation.z));
      rotMAE += rotation;
      rot.Add(rotation);

      Vector3 distance = camsHMD[i].transform.position - camsColmap[i].transform.position;
      distance = new Vector3(Mathf.Abs(distance.x), Mathf.Abs(distance.y), Mathf.Abs(distance.z));
      distMAE += distance;
      dist.Add(distance);
      Debug.DrawLine(camsHMD[i].transform.position, camsColmap[i].transform.position, Color.red, 3);
    }
    distMAE /= camsHMD.Count;
    rotMAE /= camsHMD.Count;

    distMAE_f = (distMAE.x + distMAE.y + distMAE.z) / 3;
    rotMAE_f = (rotMAE.x + rotMAE.y + rotMAE.z) / 3;
  }
}
