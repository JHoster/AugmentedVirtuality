using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObject2 : MonoBehaviour
{
  public Transform middle;

  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    middle = GameObject.Find("Point List Annotation").transform.GetChild(0).transform;
    transform.position = middle.position;
    }
}
