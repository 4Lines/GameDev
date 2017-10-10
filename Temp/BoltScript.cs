using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
