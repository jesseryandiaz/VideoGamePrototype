using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    public GameObject levelScript;
    public GameObject player;

    private Vector3 enemyPosition;
    private Vector3 playerPosition;
    private Transform playerTransform;
	

    void Update()
    {
        enemyPosition = gameObject.transform.position;
        playerTransform = player.transform;
        playerPosition = new Vector3(playerTransform.position.x, enemyPosition.y, playerTransform.position.z);
        gameObject.transform.LookAt(playerPosition);
        gameObject.transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, 0.4f * Time.deltaTime);
    }
	

}