using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Keyboard Button Controller
public class KBC : MonoBehaviour
{

  public GameObject GraphConfigButton;
  public GameObject ImageSourceConfigButton;
  public GameObject GlobalConfigButton;
  public GameObject ConsoleButton;
  public GameObject ModelPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
      foreach (Transform child in ModelPanel.transform)
        GameObject.Destroy(child.gameObject);
    if (Input.GetKeyDown(KeyCode.Alpha1))
      GraphConfigButton.GetComponent<Button>().onClick.Invoke();
    if (Input.GetKeyDown(KeyCode.Alpha2))
      ImageSourceConfigButton.GetComponent<Button>().onClick.Invoke();
    if (Input.GetKeyDown(KeyCode.Alpha3))
      GlobalConfigButton.GetComponent<Button>().onClick.Invoke();
    if (Input.GetKeyDown(KeyCode.Alpha4))
      ConsoleButton.GetComponent<Button>().onClick.Invoke();
  }
}
