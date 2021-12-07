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
    void Start()
    {
        //gameObject.transform.position = new Vector3(-3.574f, 0.83f, 1.9249f);
    }

    void Update()
    {
        enemyPosition = gameObject.transform.position;
        playerTransform = player.transform;
        playerPosition = new Vector3(playerTransform.position.x, enemyPosition.y, playerTransform.position.z);
        gameObject.transform.LookAt(playerPosition);
        gameObject.transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, 0.4f * Time.deltaTime);
    }

}