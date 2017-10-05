using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float speed;
	Camera cam;

	// Use this for initialization
	void Start () 
	{
		cam = GetComponent<Camera> ();
	}

	void FixedUpdate () 
	{
		cam.orthographicSize = (Screen.height / 100f) / 4f;
		if (target) 
		{
			transform.position = Vector3.Lerp (transform.position, target.position, speed) + new Vector3 (0, 100, 0);
		}
	}
}
