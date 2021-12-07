using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject levelScript;
    //private bool switchFlag;
    void Start()
    {
        Debug.Log("Hello World");
        //switchFlag = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //switchFlag = true;
            gameObject.GetComponent<AudioSource>().Play();
            levelScript.GetComponent<levelScript>().SwitchRealms();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //switchFlag= false;
        }
    }
}