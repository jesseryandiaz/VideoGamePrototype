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
    private bool sprinting = false;
    void Update()
    {
        bool moving = Input.GetKey(moveFor) || Input.GetKey(moveLeft) || Input.GetKey(moveBack) || Input.GetKey(moveRight);
        if (Input.GetKeyDown(sprint)) //use keyup and keydown with a boolean for sprinting to make it more resilient to spamming the sprint button
        {
            sprinting = true;
        }
        if(Input.GetKeyUp(sprint))
        {
            sprinting = false;
        }
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