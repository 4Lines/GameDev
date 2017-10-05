using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private Rigidbody2D rb;
	private float nextFire;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
		}
	}

	void FixedUpdate () {
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);

		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
		rb.angularVelocity = 0;

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		rb.AddForce(gameObject.transform.up * moveVertical * speed) ;
		rb.AddForce(gameObject.transform.right * moveHorizontal * speed) ;



	}
}
