using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject levelScript;
    public Transform cam;
    public float actualSpeed = 1.6f;
    public float walkSpeed = 1.6f;
    public float sprintSpeed = 3.6f;
    public float accelSpeed = 0.4f;
    public float turnSmoothTime = 0.01f;
    float turnSmoothVelocity;
    private Rigidbody rigidbodycomponent;
    private bool sprinting;

    //inputs
    private float horizontal;
    private float vertical;


    void Start()
    {
        Debug.Log("Hello World");
        rigidbodycomponent = GetComponent<Rigidbody>();
        sprinting = false;
    }

    void Update()
    {
        //switch realms key
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<AudioSource>().Play();
            levelScript.GetComponent<levelScript>().SwitchRealms();
        }

        //sprint key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }
    }

    private void FixedUpdate()
    {
        //important definitions for user input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //rotation code
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        if (rigidbodycomponent.velocity.magnitude > 0.05) // this if is necessary to stop rotation from resetting when player stops moving
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //movement code
        rigidbodycomponent.velocity = new Vector3(horizontal, 0, vertical).normalized * actualSpeed;
        if (sprinting && actualSpeed < sprintSpeed) //speeding up curve
        {
            actualSpeed += accelSpeed;
        }
        if (!sprinting && actualSpeed > walkSpeed) //slowing down curve
        {
            actualSpeed -= accelSpeed * 2;
        }

        //camera code
        cam.position = new Vector3(transform.position.x, transform.position.y + 4.6f, transform.position.z - 7.2f);
    }
}