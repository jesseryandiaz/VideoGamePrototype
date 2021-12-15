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
        
        if (Input.GetKey(moveFor) || Input.GetKey(moveLeft) || 
            Input.GetKey(moveBack) || Input.GetKey(moveRight))
        {
            if(Input.GetKey(sprint)){
                playerAnimator.SetBool("running", true);
            }
            else
            {
                playerAnimator.SetBool("walking", true);
                playerAnimator.SetBool("running", false);
            }
        }
        if (!Input.GetKey(moveFor) && !Input.GetKey(moveLeft) && !Input.GetKey(moveBack) && !Input.GetKey(moveRight))
        {
            playerAnimator.SetBool("running", false);
            playerAnimator.SetBool("walking", false);
        }
    }
}