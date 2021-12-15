using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationScript : MonoBehaviour
{
    public Animator playerAnimator;
    public KeyCode switchRealm;
    public KeyCode moveFor;
    public KeyCode moveLeft;
    public KeyCode moveBack;
    public KeyCode moveRight;
    public KeyCode sprint;

    private bool[] states = new bool[] { true, false, false };
    void Update()
    {
        /*bool sprinting = Input.GetKey(sprint);
        //if any of the movement keys are pressed down, player is moving
        if (Input.GetKey(moveFor) || Input.GetKey(moveLeft) ||
            Input.GetKey(moveBack) || Input.GetKey(moveRight))
        {
            //if sprint button is held while movement keys are pressed, the character is sprinting
            if (Input.GetKey(sprint)){ 
                playerAnimator.SetBool("running", true);
            }
            //if not, the character is walking and not sprinting
            else
            {
                playerAnimator.SetBool("walking", true);
                playerAnimator.SetBool("running", false);
            }
        }
        //if none of the movement buttons are pressed, the character won't be walking or running
        if (!Input.GetKey(moveFor) && !Input.GetKey(moveLeft) && !Input.GetKey(moveBack) && !Input.GetKey(moveRight))
        {
            playerAnimator.SetBool("running", false);
            playerAnimator.SetBool("walking", false);
            if (sprinting)
            {
                playerAnimator.SetBool("idle", true);
            }
        }*/
        Debug.Log("Idle: " + states[0] + "    Walk: " + states[1] + "    Run: " + states[2]);

        //new better state machine control
        bool moving = Input.GetKey(moveFor) || Input.GetKey(moveLeft) || Input.GetKey(moveBack) || Input.GetKey(moveRight);
        bool sprinting = Input.GetKey(sprint);
        if (states[0])
        {
            if (moving)
            {
                if (sprinting)
                {
                    states[0] = false;
                    states[2] = true;
                    playerAnimator.SetTrigger("idleToRun");
                }
                else
                {
                    states[0] = false;
                    states[1] = true;
                    playerAnimator.SetTrigger("idleToWalk");
                }
            }
        }
        if (states[1])
        {
            if (!moving)
            {
                states[1] = false;
                states[0] = true;
                playerAnimator.SetTrigger("walkToIdle");
            }
            else if (sprinting)
            {
                states[1] = false;
                states[2] = true;
                playerAnimator.SetTrigger("walkToRun");
            }
        }
        if (states[2])
        {
            if (!moving)
            {
                states[2] = false;
                states[0] = true;
                playerAnimator.SetTrigger("runToIdle");
            }
            else if (!sprinting)
            {
                states[2] = false;
                states[1] = true;
                playerAnimator.SetTrigger("runToWalk");
            }
        }
    }
}