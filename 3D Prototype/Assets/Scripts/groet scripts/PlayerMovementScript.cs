using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject levelScript;
    public Transform cam;
    public float actualSpeed = 0.0f;
    public float maxSpeed = 3.6f;
    public float walkSpeed = 1.6f;
    public float sprintSpeed = 3.6f;
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
        //Debug.Log("Horizontal Input:  " + horizontal);
        vertical = Input.GetAxis("Vertical");
        //Debug.Log("Vertical Input:  " + vertical);

        //rotation code
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        if (rigidbodycomponent.velocity.magnitude > 0.05) // this if is necessary to stop rotation from resetting when player stops moving
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //movement code
        if ((Mathf.Abs(horizontal) > 0  || Mathf.Abs(vertical) > 0) && actualSpeed<walkSpeed) //player walking
        {
            actualSpeed += 0.8f;
        }
        if(Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0 && actualSpeed>0.0f) //player stopped
        {
            actualSpeed -= 0.4f;
        }
        if (sprinting && actualSpeed < sprintSpeed ) //player sprinting
        {
            actualSpeed += 0.4f;
        }
        if (!sprinting && actualSpeed > walkSpeed) //player not sprinting
        {
            actualSpeed -= 0.4f * 2;
        }
        rigidbodycomponent.velocity = new Vector3(horizontal, rigidbodycomponent.velocity.y, vertical).normalized * actualSpeed;

        //camera code
        cam.position = new Vector3(transform.position.x, transform.position.y + 4.6f, transform.position.z - 7.2f);
    }
}