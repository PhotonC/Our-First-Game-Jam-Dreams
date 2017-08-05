using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animation_controller;
    private CharacterController char_controller;
    private bool on_ground;


    // Use this for initialization
    void Start ()
    {
        animation_controller = GetComponent<Animator>();
        char_controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CollisionFlags collision_flags;

        /* check if the player is on the ground
        if (CollisionFlags.Below != 0)
        {
            on_ground = true;
        }
        else
        {
            on_ground = false;
        }*/

        if(on_ground == false)
        {
            // do not change animation if the player is falling
        }
        else if (Input.GetButtonDown("Jump")) // play the jump animation if the player is jumping
        {
            animation_controller.Play("Jump");
        }
        else if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            animation_controller.Play("Walk");
        }
        else if(!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            animation_controller.Play("Idle");
        }
	}
}
