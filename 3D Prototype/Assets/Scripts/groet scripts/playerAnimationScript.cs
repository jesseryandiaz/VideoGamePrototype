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

    void Update()
    {
        if (Input.GetKeyDown(switchRealm))
        {
            playerAnimator.SetTrigger("switchRealms");
        }
        if (Input.GetKeyDown(moveFor) || Input.GetKeyDown(moveLeft) || 
            Input.GetKeyDown(moveBack) || Input.GetKeyDown(moveRight))
        {
            playerAnimator.SetBool("move", true);
            if (Input.GetKey(sprint)){
                playerAnimator.SetBool("moveFast", true);
            }
        }
        if (Input.GetKeyDown(sprint))
        {
            playerAnimator.SetBool("moveFast", true);
        }
        if (Input.GetKeyUp(sprint))
        {
            playerAnimator.SetBool("moveFast", false);
        }
        if (!Input.GetKey(moveFor) && !Input.GetKey(moveLeft) && !Input.GetKey(moveBack) && !Input.GetKey(moveRight))
        {
            playerAnimator.SetBool("move", false);
            playerAnimator.SetBool("moveFast", false);
        }
    }
}