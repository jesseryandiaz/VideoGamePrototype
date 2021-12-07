using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEtherealScript : MonoBehaviour
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
        Transform playerTransform = GameObject.Find("mainCharacter").GetComponent<Transform>();
        Vector3 playerPosition = playerTransform.position;

        //look at player always, move toward player when p[l;ayer is in physical realm
        gameObject.transform.LookAt(playerTransform);
        if (!inEthereal)
        {
            gameObject.transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, 1.0f * Time.deltaTime);
        }
    }
}
