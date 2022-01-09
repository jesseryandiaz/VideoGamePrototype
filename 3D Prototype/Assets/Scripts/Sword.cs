using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{


    Animator animator;
    public Collider colliderino;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) animator.SetTrigger("SwingActivated");
    }

    void FixedUpdate()
    {
        if (animator.IsInTransition(0)) colliderino.enabled = true;
        else colliderino.enabled = false;
    }
	
}
