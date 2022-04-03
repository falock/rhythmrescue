using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 10f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    // Place the script in the Camera-Control group in the component menu
    private void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target) return;

		float currentHeight = transform.position.y;

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
	}
}


