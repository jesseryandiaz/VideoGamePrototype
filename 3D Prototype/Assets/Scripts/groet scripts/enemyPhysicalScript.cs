using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPhysicalScript : MonoBehaviour
{
    public GameObject levelScript;
    private bool inEthereal;
    void Start()
    {
        inEthereal = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        inEthereal = levelScript.GetComponent<levelScript>().getInEthereal();
        Vector3 enemyPosition = gameObject.transform.position;
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 playerPosition = playerTransform.position;
        gameObject.transform.LookAt(playerTransform);
        gameObject.transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, 0.4f * Time.deltaTime);
    }
}