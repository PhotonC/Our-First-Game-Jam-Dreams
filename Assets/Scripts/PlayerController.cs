using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // these are the player's attributes
    public float speed;
    public float jump_speed;
    public float gravity;
    public float jump_delay;

    // this allows us to access the game camera
    public Transform game_camera; 

    // these are the player's components that will be accessed in this script
    private CharacterController char_controller;

    private bool can_jump; // determines whether or not the player is allowed to jump
    private float vertical_velocity; // used for gravity and jumping

    private IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(jump_delay);
        vertical_velocity += jump_speed;// make player jump
    }

    // start is called at the first frame; it's an initalization function
    void Start()
    {
        char_controller = GetComponent<CharacterController>();
        can_jump = false;
    }

    // update is called at every frame; game code goes here
    void Update()
    {

        // these will grab input from the input manager
        float move_horizontal = Input.GetAxis("Horizontal");
        float move_vertical = Input.GetAxis("Vertical");
        CollisionFlags collision_flags; // used for, guess? Collision? Yeah, exactly.
        Quaternion input_rotation; // weird stuff, but helps with input based on camera angle
        // this creates a Vector 3 to hold the directional forces input from the player
        Vector3 movement = new Vector3(move_horizontal, 0.0f, move_vertical);
        Vector3 future_position = new Vector3(0f, 0f, 0f);

        // this prevents the player from speeding up while moving at a diagonal
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        // this will make it so that the player moves relatively to the camera
        input_rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(game_camera.forward, Vector3.up), Vector3.up);
        movement = input_rotation * movement;

        future_position = movement + transform.position;

        // this will simulate gravity YAY!!!
        vertical_velocity = vertical_velocity - gravity * Time.deltaTime;

        // this makes it work with the Move function better (moving per second instead of per frame)
        movement = movement * speed * Time.deltaTime;

        if(Input.GetButtonDown("Jump"))
        {
            // if the player is able to jump
            if(can_jump)
            {
                StartCoroutine(jumpDelay());
            }

        }

        movement.y = vertical_velocity * Time.deltaTime;

        // need code to rotate the player based on which direction they move
        if (movement.x != 0 || movement.z != 0)
        {
            transform.LookAt(future_position);
        }

        // let's use Unity's built in movement for now; rigid bodies are intimidating
        // by setting collision_flags to this function, we can access... the collision flags when the player moves
        collision_flags = char_controller.Move(movement);

        // check if the player can jump
        if((collision_flags & CollisionFlags.Below) != 0)
        {
            can_jump = true;
            vertical_velocity = -3.0f; // this makes it so that the object will just move up and down slopes
        }
        else
        {
            can_jump = false;
        }
    }

}
