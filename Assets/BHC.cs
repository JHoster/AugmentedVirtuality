using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHC : MonoBehaviour
{
  public GameObject prefab;
  public int number;
  public float speed;

  //BlackHoleController: Instantiate objects
  void Start()
  {
    for (int i = 0; i < number; i++)
      Instantiate(prefab, Random.onUnitSphere * 10, Quaternion.identity, transform);
  }

  private void Update()
  {
    BHG.speed = speed;
  }
}
