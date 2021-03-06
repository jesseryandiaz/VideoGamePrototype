using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject levelScript;
    public Transform cam;
    public float actualSpeed = 0.0f;
    public float accelSpeed = 0.4f;
    public float walkSpeed = 1.6f;
    public float sprintSpeed = 3.6f;
    public float turnSmoothTime = 0.001f;
    float turnSmoothVelocity;
    private Rigidbody rigidbodycomponent;
    private bool sprinting;

    //inputs
    private float horizontal;
    private float vertical;

    //camera offsets
    public float xoffset = 0.0f;
    public float yoffset = 4.6f;
    public float zoffset = 7.2f;

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

        //ROTATION CODE
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        if (rigidbodycomponent.velocity.magnitude > 0.05) // this if is necessary to stop rotation from resetting when player stops moving
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //MOVEMENT CODE
        bool moving = (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0); //True if inputs are received
        //If movement inputs are received, accelerate to walking speed
        if (moving)
        {
            if(actualSpeed < walkSpeed) //always get up to walk speed
            {
                actualSpeed += accelSpeed;
            }
            if(sprinting && actualSpeed < sprintSpeed)//accelerate faster if sprinting, also accelerate up to sprint speed
            {
                actualSpeed += accelSpeed;
            }
            if (!sprinting && actualSpeed>walkSpeed) //if not sprinting, go back down to walk speed
            {
                actualSpeed -= accelSpeed;
            }
        }
        //If movement inputs are not received, deccelerate to stop
        if (!moving && actualSpeed>0.0f)
        {
            actualSpeed -= accelSpeed/4;
            //If character was sprinting, deccelerate faster
            if (actualSpeed > walkSpeed)
            {
                //actualSpeed -= accelSpeed;
            }
        }
        if (actualSpeed < 0.0f)
        {
            actualSpeed = 0.0f;
        }
        //Updates velocity with above changes
        rigidbodycomponent.velocity = new Vector3(horizontal, rigidbodycomponent.velocity.y, vertical).normalized * actualSpeed;

        //CAMERA CODE
        cam.position = new Vector3(transform.position.x, transform.position.y + 4.6f, transform.position.z - 7.2f);
    }
}