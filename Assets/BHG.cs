using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHG : MonoBehaviour
{
  //BlackHoleGravity: Make Object move to point

  public static float speed;

    void Update()
    {
    transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
    }
}
