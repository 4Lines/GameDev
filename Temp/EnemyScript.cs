using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float speed;
	public Transform target;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate () {
		float z = Mathf.Atan2 ((target.transform.position.y - transform.position.y), 
			          (target.transform.position.x - transform.position.x)) *
		          Mathf.Rad2Deg - 90;
		transform.eulerAngles = new Vector3 (0, 0, z);
		rb.AddForce (gameObject.transform.up * speed);
	}
}
