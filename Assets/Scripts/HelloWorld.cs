using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField]
    private string myName;
    private enum actionPropsList { Idle, Run};
    [SerializeField]
    private actionPropsList actionState;
    private string actionStateStr;
    // Start is called before the first frame update

    private void Awake()
    {
        Debug.Log("Awake called");
    }
    void Start()
    {
        Debug.Log("Start called");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, Screen.width, Screen.height),
        "Hello, my name is " + myName);
        if (actionState == actionPropsList.Idle)
        {
            actionStateStr = "I will " + actionState;
        }
        if (actionState == actionPropsList.Run)
        {
            actionStateStr = "I will " + actionState;
        }
        GUI.Label(new Rect(10, 30, Screen.width, Screen.height), actionStateStr);
    }
}
