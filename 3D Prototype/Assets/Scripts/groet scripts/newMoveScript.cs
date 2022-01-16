using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMoveScript : MonoBehaviour
{
    //inputs
    private float horizontal;
    private float vertical;
    public CharacterController charCon;
    private float currentSpeed;
    public float walkSpeed = 1.0f;
    public float runSpeed = 3.4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 moveVect;
	
	//object references
	public GameObject levelScript;
	
	//camera
	public Transform cam;
    public float xoffset = 0.0f;
    public float yoffset = 4.6f;
    public float zoffset = 7.2f;
	

    private void Start()
    {
        currentSpeed = walkSpeed;
        moveVect = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //important definitions for user input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //handle running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
        }
		
		//switch realms key
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<AudioSource>().Play();
            levelScript.GetComponent<levelScript>().SwitchRealms();
        }
    }

    private void FixedUpdate()
    {
        //MOVEMENT CODE, doesn't play nice with jumping yet, but ramps and steps work decently well
        moveVect = new Vector3(horizontal, 0, vertical).normalized * Time.deltaTime * currentSpeed * 50;
        charCon.SimpleMove(moveVect);

        //ROTATION CODE
        Rigidbody rigidbodycomponent = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        if (Mathf.Abs(charCon.velocity.x) >= 0.1f || Mathf.Abs(charCon.velocity.z) >= 0.1f) // this if is necessary to stop rotation from resetting when player stops moving
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
		
		//CAMERA CODE
		cam.position = new Vector3(transform.position.x, transform.position.y + yoffset, transform.position.z - zoffset);
    }
}
