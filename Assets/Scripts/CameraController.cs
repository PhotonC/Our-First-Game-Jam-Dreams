using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float camera_speed;

    private float camera_horizontal;
    private float camera_vertical;

    private Vector3 offset;

	void Start ()
    {
        offset = transform.position - player.transform.position;
        camera_horizontal = 0.0f;
        camera_vertical = 0.0f;
	}

    void Update()
    {
        camera_horizontal += Input.GetAxis("Camera Horizontal");
        camera_vertical += Input.GetAxis("Camera Vertical");
    }

    // Late Update is only called at the end of the frame
    void LateUpdate ()
    {
        Quaternion rotation = Quaternion.Euler(camera_vertical, camera_horizontal, 0);
        //transform.position = player.transform.position + offset; // old camera movement
        transform.position = player.transform.position + rotation * offset;
        transform.LookAt(player.transform.position);
        // holy crap that works
    }
}
