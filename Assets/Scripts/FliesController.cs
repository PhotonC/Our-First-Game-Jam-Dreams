using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliesController : MonoBehaviour {

    private float collect_radius = 0.5f;
    public Collider[] hit_colliders;
    public LayerMask mask;
    public bool going_up;
    public float max_bob;
    public float bob_speed;

    private Vector3 initial_position;
    private Vector3 bob_vector;

    // Use this for initialization
    void Start ()
    {
        initial_position = transform.position;
        bob_vector = new Vector3(0.0f, bob_speed, 0.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
       // hit_colliders = Physics.OverlapCapsule(transform.position, transform.position, collect_radius);
        hit_colliders = Physics.OverlapSphere(transform.position, collect_radius, mask);

        if(hit_colliders.Length > 0)
        {
            if(Input.GetButtonDown("Tongue"))
            {
                print("collect");
                // AAAAAAHHHHHH
                // okay get it to tell game manager to increase score by one
                Globals.flies++; // globals because that's so much easier.
                DestroyObject(gameObject); // kills self when collected
            }
        }

        // make the fly bob in place
        if (transform.position.y >= initial_position.y + max_bob)
        {
            going_up = false;
        }
        else if (transform.position.y <= initial_position.y - max_bob)
        {
            going_up = true;
        }

        if(going_up)
        {
            transform.Translate(bob_vector);
        }
        else if(!going_up)
        {
            transform.Translate(bob_vector * -1);
        }

        return;
	}
}
