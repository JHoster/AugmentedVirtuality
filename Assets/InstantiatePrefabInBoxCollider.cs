using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabInBoxCollider : MonoBehaviour
{
  public Vector3 pos;
  public Vector3 size;
  public float scaleFactor;
  public GameObject prefab;

  // Start is called before the first frame update
  void Start()
  {
    pos = transform.TransformPoint(GetComponent<BoxCollider>().center);
    size = GetComponent<BoxCollider>().size;
    prefab = Instantiate(prefab, pos, Quaternion.identity, gameObject.transform);
    prefab.transform.localScale = size / scaleFactor;
    prefab.transform.localRotation = Quaternion.identity; // Euler(0, 0, 0);
    prefab.transform.GetChild(Random.Range(0, prefab.transform.childCount)).gameObject.SetActive(true);
  }
}
