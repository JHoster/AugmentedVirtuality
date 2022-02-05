using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
  public GameObject Sol; //Solution
  public GameObject MC; //Model Canvas
  public GameObject chair;
  public GameObject cup;
  public GameObject active;
  private int selected;
  public string cat;
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }

  // Start is called before the first frame update
  void Start()
  {
    active = chair;
    Select();
  }

  // Update is called once per frame
  void Update()
  {
    if (GameObject.Find("Solution"))
      Sol = GameObject.Find("Solution").transform.gameObject;
    if (GameObject.Find("Main Canvas"))
      MC = GameObject.Find("Main Canvas").transform.gameObject;


    if (Input.GetKeyDown(KeyCode.Backspace))
    {
      chair.SetActive(false);
      cup.SetActive(false);
    }

    //if (!active)
    //  active = cup;
    if (MC && MC.transform.GetChild(1).gameObject.activeInHierarchy)
    {
      chair.SetActive(false);
      cup.SetActive(false);
      GameObject Cat = GameObject.Find("Category").transform.gameObject;
      GameObject label = Cat.transform.GetChild(1).transform.GetChild(0).transform.gameObject;
      cat = label.GetComponent<Text>().text;
    }
    else
    {
      if (cat == "Chair")
      {
        active = chair;
        cup.SetActive(false);
      }
      else if (cat == "Cup")
      {
        active = cup;
        chair.SetActive(false);
      }
      active.gameObject.SetActive(true);
    }

    //if (Input.GetKeyDown(KeyCode.C))
    //{
    //  if (active == chair)
    //  {
    //    active = cup;

    //    chair.SetActive(false);
    //  }
    //  else
    //  {
    //    active = chair;
    //    cup.SetActive(false);
    //  }
    //  active.gameObject.SetActive(true);
    //}

    int previousSelected = selected;

    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    {
      if (selected >= active.transform.childCount - 1)
        selected = 0;
      else
        selected++;
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    {
      if (selected <= 0)
        selected = active.transform.childCount - 1;
      else
        selected--;
    }

    if (Input.GetKeyDown(KeyCode.Alpha1))
      selected = 0;
    if (Input.GetKeyDown(KeyCode.Alpha2) && active.transform.childCount >= 2)
      selected = 1;
    if (Input.GetKeyDown(KeyCode.Alpha3) && active.transform.childCount >= 3)
      selected = 2;

    if (previousSelected != selected)
    {
      Select();
    }
  }

  void Select()
  {
    int i = 0;
    foreach (Transform vo in active.transform)
    {
      if (i == selected)
        vo.gameObject.SetActive(true);
      else
        vo.gameObject.SetActive(false);
      i++;
    }
  }
}
